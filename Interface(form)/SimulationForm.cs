using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FlightLib;
using System.IO;
using System.Globalization;
using System.Text;

namespace Interface_form_
{
    public partial class SimulationForm : Form
    {
        private FlightPlanList _flightPlans;
        private double _cycleTime;
        private double _securityDistance;
        private PictureBox[] flights;
        private Label[] flightLabels;
        private Timer simulationTimer;
        private Stack<Position[]> _history = new Stack<Position[]>();
        private string _saveFilePath;

        // --- CAMPOS NUEVOS PARA EL AUTO-ESCALADO ---
        private double minWorldX, maxWorldX, minWorldY, maxWorldY;
        private double worldScale;
        // Padding (margen) para que los vuelos no toquen los bordes
        private const int padding = 30;
        // ------------------------------------------

        // Constructor (MODIFICADO para llamar al cálculo de escala)
        public SimulationForm(FlightPlanList flightPlans, double cycleTime, double securityDistance)
        {
            InitializeComponent();
            _flightPlans = flightPlans;
            _cycleTime = cycleTime;
            _securityDistance = securityDistance;
            panel1.Paint += panel1_Paint;
      
            // 1. Calcular los límites y la escala ANTES de dibujar nada
            CalcularLimitesYEscala();

            // Initialize the simulation timer
            simulationTimer = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // Convert seconds to milliseconds
            simulationTimer.Tick += timer1_Tick;
        }

       
        private void CalcularLimitesYEscala()
        {
            minWorldX = double.MaxValue;
            maxWorldX = double.MinValue;
            minWorldY = double.MaxValue;
            maxWorldY = double.MinValue;

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan fp = _flightPlans.GetFlightPlan(i);
                Position[] positions = { fp.GetInitialPosition(), fp.GetFinalPosition() };

                foreach (Position pos in positions)
                {
                    if (pos.GetX() < minWorldX) minWorldX = pos.GetX();
                    if (pos.GetX() > maxWorldX) maxWorldX = pos.GetX();
                    if (pos.GetY() < minWorldY) minWorldY = pos.GetY();
                    if (pos.GetY() > maxWorldY) maxWorldY = pos.GetY();
                }
            }

            if (maxWorldX == minWorldX) maxWorldX += 100;
            if (maxWorldY == minWorldY) maxWorldY += 100;

            double worldWidth = maxWorldX - minWorldX;
            double worldHeight = maxWorldY - minWorldY;

            double screenWidth = panel1.Width - (2 * padding);
            double screenHeight = panel1.Height - (2 * padding);

            double scaleX = screenWidth / worldWidth;
            double scaleY = screenHeight / worldHeight;
            worldScale = Math.Min(scaleX, scaleY);

            // Añadir esta comprobación para evitar valores infinitos o NaN
            if (double.IsInfinity(worldScale) || double.IsNaN(worldScale))
            {
                worldScale = 1.0;
            }
        }

        private Point MapearCoordenadas(Position pos)
        {
       
            int screenX = (int)((pos.GetX() - minWorldX) * worldScale) + padding;

           
            int screenY = (int)((maxWorldY - pos.GetY()) * worldScale) + padding;

            return new Point(screenX, screenY);
        }

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            flights = new PictureBox[_flightPlans.getnum()];
            flightLabels = new Label[_flightPlans.getnum()];
            int i = 0;
            while (i < _flightPlans.getnum())
            {
                PictureBox p = new PictureBox();
                FlightPlan f = _flightPlans.GetFlightPlan(i);

                // Si la posición actual es nula (p.ej. al cargar un fichero sin estado),
                // la inicializamos a la posición de partida.
                if (f.GetCurrentPosition() == null)
                {
                    f.Restart();
                }

                p.Width = 10;
                p.Height = 10;
                p.ClientSize = new Size(10, 10);

                Point screenPos = MapearCoordenadas(f.GetCurrentPosition());
                int x = screenPos.X - p.Width / 2;
                int y = screenPos.Y - p.Height / 2;

                p.Location = new Point(x, y);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bmp = new Bitmap(p.Width, p.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Black, 0, 0, p.Width, p.Height);
                }

                p.Image = bmp;
                p.Tag = i;
                p.Click += new System.EventHandler(this.flightInfo);
                panel1.Controls.Add(p);
                flights[i] = p;

                Label lbl = new Label();
                lbl.Text = f.GetId();
                lbl.AutoSize = true;
                lbl.Location = new Point(x + p.Width, y);
                panel1.Controls.Add(lbl);
                flightLabels[i] = lbl;

                i++;
            }
            foreach (Control c in panel1.Controls)
            {
                c.SendToBack();
            }

            // Colocar los aviones en su posición actual (por si hemos cargado estado guardado)
            UpdateFlightsUI();
        }

        private void flightInfo(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            int i = (int)p.Tag;
            FlightInfo f = new FlightInfo();
            f.setFlight(_flightPlans.GetFlightPlan(i));
            f.ShowDialog();
        }

        private void SaveCurrentState()
        {
            Position[] currentState = new Position[_flightPlans.getnum()];
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                Position currentPos = _flightPlans.GetFlightPlan(i).GetCurrentPosition();
                currentState[i] = new Position(currentPos.GetX(), currentPos.GetY());
            }
            _history.Push(currentState);
        }

        private void UpdateFlightsUI()
        {
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                Point screenPos = MapearCoordenadas(flight.GetCurrentPosition());
                int x = screenPos.X - flights[i].Width / 2;
                int y = screenPos.Y - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
                flightLabels[i].Location = new Point(x + flights[i].Width, y);
            }
            panel1.Invalidate();
        }

        private void cyclebtn_Click(object sender, EventArgs e)
        {
            SaveCurrentState();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }
            UpdateFlightsUI();
        }

        private bool VueloEnConflicto(int indice)
        {
            FlightPlan vuelo = _flightPlans.GetFlightPlan(indice);
            if (vuelo == null) return false;
            int total = _flightPlans.getnum();
            int i = 0;
            while (i < total)
            {
                if (i != indice)
                {
                    FlightPlan otro = _flightPlans.GetFlightPlan(i);
                    if (otro != null && vuelo.Conflicto(otro, _securityDistance))
                    {
                        return true;
                    }
                }
                i++;
            }
            return false;
        }

        // Sustituir todo el método panel1_Paint por este
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Helper function to check for valid float values
            bool IsFinite(float f) => !float.IsNaN(f) && !float.IsInfinity(f);

            using (Pen penTrayecto = new Pen(Color.Black, 2))
            {
                penTrayecto.DashStyle = DashStyle.Dash;

                int i = 0;
                while (i < _flightPlans.getnum())
                {
                    FlightPlan flight = _flightPlans.GetFlightPlan(i);
                    Position origin = flight.GetInitialPosition();
                    Position dest = flight.GetFinalPosition();

                    Point p1 = MapearCoordenadas(origin);
                    Point p2 = MapearCoordenadas(dest);
                    e.Graphics.DrawLine(penTrayecto, p1.X, p1.Y, p2.X, p2.Y);

                    Position current = flight.GetCurrentPosition();
                    Point centerPoint = MapearCoordenadas(current);

                    double radiusDouble = (_securityDistance / 2.0) * worldScale;

                    if (double.IsNaN(radiusDouble) || double.IsInfinity(radiusDouble) || radiusDouble > (Math.Max(panel1.Width, panel1.Height) * 2))
                    {
                        i++;
                        continue; 
                    }

                    float radius = (float)radiusDouble;
                    float ellipseX = centerPoint.X - radius;
                    float ellipseY = centerPoint.Y - radius;
                    float diameter = radius * 2.0f;

                    // --- VALIDACIÓN MEJORADA ---
                    // Ensure all calculated values are finite before drawing.
                    if (!IsFinite(ellipseX) || !IsFinite(ellipseY) || !IsFinite(diameter) || diameter <= 0)
                    {
                        i++;
                        continue; // Skip drawing if values are invalid.
                    }
                    // --- FIN DE LA VALIDACIÓN ---

                    bool conflicto = VueloEnConflicto(i);

                    Color colorBorde = conflicto ? Color.Red : Color.Blue;
                    Color colorRelleno = conflicto ? Color.FromArgb(80, 255, 0, 0) : Color.FromArgb(50, 0, 0, 255);

                    using (Pen penCirc = new Pen(colorBorde, 2))
                    using (SolidBrush brushCirc = new SolidBrush(colorRelleno))
                    {
                        e.Graphics.FillEllipse(brushCirc, ellipseX, ellipseY, diameter, diameter);
                        e.Graphics.DrawEllipse(penCirc, ellipseX, ellipseY, diameter, diameter);
                    }

                    i++;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SaveCurrentState();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);
            }
            UpdateFlightsUI();

            if (_flightPlans.DetectConflict(_securityDistance) == true)
            {

                simulationTimer.Stop();
            }
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            // Configuración del algoritmo
            int maxIteraciones = 200;
            double minSpeed = 0.5; // Velocidad mínima razonable para no “parar” vuelos
            double reduccionAmbosFactor = 0.85; // Si falla todo, reducir ambos al 85%
            int iteracion = 0;

            // Diccionario para registrar cambios
            Dictionary<string, double> cambios = new Dictionary<string, double>();

            // Respaldar velocidades originales (por si luego quieres restaurar en Restart)
            int total = _flightPlans.getnum();
            double[] respaldo = new double[total];
            int r = 0;
            while (r < total)
            {
                FlightPlan respaldoFp = _flightPlans.GetFlightPlan(r);
                respaldo[r] = respaldoFp.GetVelocidad();
                r++;
            }

            // Construir listado inicial de conflictos para mostrar al usuario
            StringBuilder listadoInicial = new StringBuilder();
            bool hayInicial = false;
            int i = 0;
            while (i < total)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                int j = i + 1;
                while (j < total)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (_flightPlans.predictConflict(a, b, _securityDistance))
                    {
                        listadoInicial.Append("- ").Append(a.GetId()).Append(" con ").Append(b.GetId()).AppendLine();
                        hayInicial = true;
                    }
                    j++;
                }
                i++;
            }

            if (!hayInicial)
            {
                MessageBox.Show("No se predicen conflictos futuros entre los vuelos.", "Sin conflictos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                simulationTimer.Interval = (int)(_cycleTime * 1000.0);
                simulationTimer.Start();
                return;
            }

            listadoInicial.AppendLine();
            listadoInicial.Append("¿Desea resolverlos iterativamente (uno a la vez) ajustando velocidades?");
            DialogResult respuesta = MessageBox.Show(listadoInicial.ToString(), "Conflictos futuros detectados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (respuesta != DialogResult.Yes)
            {
                MessageBox.Show("Se inicia la simulación sin resolver los conflictos.", "Continuar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                simulationTimer.Interval = (int)(_cycleTime * 1000.0);
                simulationTimer.Start();
                return;
            }

            // Algoritmo iterativo por conflictos
            while (iteracion < maxIteraciones)
            {
                // Encontrar el primer conflicto
                FlightPlan cA = null;
                FlightPlan cB = null;
                int aIndex = -1;
                int bIndex = -1;

                int x = 0;
                while (x < total && cA == null)
                {
                    FlightPlan fa = _flightPlans.GetFlightPlan(x);
                    int y = x + 1;
                    while (y < total)
                    {
                        FlightPlan fb = _flightPlans.GetFlightPlan(y);
                        if (_flightPlans.predictConflict(fa, fb, _securityDistance))
                        {
                            cA = fa;
                            cB = fb;
                            aIndex = x;
                            bIndex = y;
                            break;
                        }
                        y++;
                    }
                    x++;
                }

                if (cA == null)
                {
                    // No quedan conflictos
                    break;
                }

                // Intentar resolver ajustando velocidad de B respecto a A
                (bool okB, double nuevaVelB) = _flightPlans.ResolveConflictBySpeed(cA, cB, _securityDistance);
                if (okB)
                {
                    cambios[cB.GetId()] = nuevaVelB;
                    iteracion++;
                    continue;
                }

                // Si no, intentar ajustando velocidad de A respecto a B
                (bool okA, double nuevaVelA) = _flightPlans.ResolveConflictBySpeed(cB, cA, _securityDistance);
                if (okA)
                {
                    cambios[cA.GetId()] = nuevaVelA;
                    iteracion++;
                    continue;
                }

                // Fallback: Ajustar ambos (si tienen recorrido)
                double distA = cA.GetInitialPosition().Distancia(cA.GetFinalPosition());
                double distB = cB.GetInitialPosition().Distancia(cB.GetFinalPosition());
                if (distA > 1e-6 && distB > 1e-6)
                {
                    double vA = cA.GetVelocidad();
                    double vB = cB.GetVelocidad();
                    double nuevaA = vA * reduccionAmbosFactor;
                    double nuevaB = vB * reduccionAmbosFactor;
                    if (nuevaA < minSpeed) nuevaA = minSpeed;
                    if (nuevaB < minSpeed) nuevaB = minSpeed;
                    cA.SetVelocidad(nuevaA);
                    cB.SetVelocidad(nuevaB);
                    cambios[cA.GetId()] = nuevaA;
                    cambios[cB.GetId()] = nuevaB;
                }
                else
                {
                    // Al menos uno no tiene recorrido (origen = destino): no se puede resolver por velocidad.
                    // Se marca y se continúa buscando otros conflictos.
                    cambios[cA.GetId()] = cA.GetVelocidad();
                    cambios[cB.GetId()] = cB.GetVelocidad();
                }

                iteracion++;
            }

            // Verificación final
            bool quedan = false;
            int ii = 0;
            while (ii < total && !quedan)
            {
                FlightPlan fa2 = _flightPlans.GetFlightPlan(ii);
                int jj = ii + 1;
                while (jj < total)
                {
                    FlightPlan fb2 = _flightPlans.GetFlightPlan(jj);
                    if (_flightPlans.predictConflict(fa2, fb2, _securityDistance))
                    {
                        quedan = true;
                        break;
                    }
                    jj++;
                }
                ii++;
            }

            StringBuilder resultado = new StringBuilder();
            if (cambios.Count > 0)
            {
                resultado.AppendLine("Ajustes de velocidad realizados:");
                foreach (KeyValuePair<string, double> par in cambios)
                {
                    resultado.Append("- ").Append(par.Key).Append(": ").Append(par.Value.ToString("F2")).AppendLine();
                }
            }
            else
            {
                resultado.AppendLine("No se han realizado ajustes de velocidad.");
            }

            if (!quedan)
            {
                resultado.AppendLine();
                resultado.AppendLine("Todos los conflictos han sido resueltos.");
                MessageBox.Show(resultado.ToString(), "Resolución completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                resultado.AppendLine();
                resultado.AppendLine("Persisten conflictos (posibles vuelos sin recorrido o imposible de resolver sólo con velocidad).");
                MessageBox.Show(resultado.ToString(), "Resolución incompleta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            simulationTimer.Interval = (int)(_cycleTime * 1000.0);
            simulationTimer.Start();
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            simulationTimer.Stop();
            panel1.Invalidate();
        }

        private void infobtn_Click(object sender, EventArgs e)
        {
            FlightGrid form = new FlightGrid(_flightPlans);
            form.ShowDialog(this);
        }

        private void restartbtn_Click(object sender, EventArgs e)
        {
            RestartSimulation();
        }

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            int numFlights = _flightPlans.getnum();
            bool conflictPredicted = false;
            string message = "";

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);

                    if (_flightPlans.predictConflict(a, b, _securityDistance))
                    {
                        message += $"Se predice conflicto entre los vuelos {a.GetId()} y {b.GetId()}.\n";
                        conflictPredicted = true;
                    }
                }
            }

            if (conflictPredicted)
            {
                MessageBox.Show(message, "Conflicto futuro detectado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No se predicen conflictos futuros entre los vuelos.", "Sin conflicto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RestartSimulation()
        {
            simulationTimer.Stop();
            _history.Clear();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan f = _flightPlans.GetFlightPlan(i);
                f.Restart();
            }

            UpdateFlightsUI();
        }

        private void returnbtn_Click(object sender, EventArgs e)
        {
            if (_history.Count > 0)
            {
                simulationTimer.Stop();

                Position[] lastState = _history.Pop();
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    _flightPlans.GetFlightPlan(i).SetCurrentPosition(lastState[i]);
                }

                UpdateFlightsUI();
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Flight Plan files (*.flp)|*.flp|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Simulation State";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        int result = _flightPlans.saveToFile(filePath);
                        if (result == 0)
                        {
                            MessageBox.Show("Simulation state saved successfully.", "Save Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to save simulation state.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void editspeedsbtn_Click(object sender, EventArgs e)
        {
            EditSpeedsForm editForm = new EditSpeedsForm(_flightPlans);
            editForm.Owner = this;
            editForm.ShowDialog();
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ReiniciarAIniciales()
        {
            int n = _flightPlans.getnum();
            int i = 0;
            while (i < n)
            {
                FlightPlan f = _flightPlans.GetFlightPlan(i);
                if (f != null)
                {
                    Position ini = f.GetInitialPosition();
                    f.SetCurrentPosition(new Position(ini.GetX(), ini.GetY()));
                }
                i++;
            }
        }
    }
}