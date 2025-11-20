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
        private readonly FlightPlanList _flightPlans;
        private readonly double _cycleTime;
        private readonly double _securityDistance;
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

            // Ruta del fichero de guardado (en AppData del usuario)
            _saveFilePath = Path.Combine(Application.UserAppDataPath, "simstate_v1.txt");

            // Ofrecer reanudación si existe fichero guardado
            if (File.Exists(_saveFilePath))
            {
                var result = MessageBox.Show(
                    "Se ha encontrado un estado guardado. ¿Deseas reanudar la simulación desde ese punto?",
                    "Reanudar simulación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (!TryLoadSimulationState())
                    {
                        MessageBox.Show("No se pudo cargar el estado guardado. Se iniciará una nueva simulación.",
                            "Carga fallida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            // 1. Calcular los límites y la escala ANTES de dibujar nada
            CalcularLimitesYEscala();

            // Initialize the simulation timer
            simulationTimer = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // Convert seconds to milliseconds
            simulationTimer.Tick += timer1_Tick;
        }

        // --- NUEVO: Guardar estado ---
        private void SaveSimulationState()
        {
            // Formato:
            // SIMSTATE;v1
            // numFlights
            // id;speed;ix;iy;fx;fy;cx;cy
            var ic = CultureInfo.InvariantCulture;

            Directory.CreateDirectory(Path.GetDirectoryName(_saveFilePath) ?? Application.UserAppDataPath);

            using (var sw = new StreamWriter(_saveFilePath, false, Encoding.UTF8))
            {
                sw.WriteLine("SIMSTATE;v1");
                sw.WriteLine(_flightPlans.getnum().ToString(ic));

                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    var f = _flightPlans.GetFlightPlan(i);
                    // Evitar ';' en IDs
                    string id = (f.GetId() ?? "").Replace(";", "_");

                    var ini = f.GetInitialPosition();
                    var fin = f.GetFinalPosition();
                    var cur = f.GetCurrentPosition();

                    string line = string.Join(";",
                        id,
                        f.GetVelocidad().ToString("R", ic),
                        ini.GetX().ToString("R", ic),
                        ini.GetY().ToString("R", ic),
                        fin.GetX().ToString("R", ic),
                        fin.GetY().ToString("R", ic),
                        cur.GetX().ToString("R", ic),
                        cur.GetY().ToString("R", ic)
                    );

                    sw.WriteLine(line);
                }
            }
        }

        // --- NUEVO: Cargar estado ---
        private bool TryLoadSimulationState()
        {
            if (!File.Exists(_saveFilePath)) return false;

            var ic = CultureInfo.InvariantCulture;
            try
            {
                string[] lines = File.ReadAllLines(_saveFilePath, Encoding.UTF8);
                if (lines.Length < 2) return false;
                if (!lines[0].StartsWith("SIMSTATE;v1", StringComparison.Ordinal)) return false;

                int count;
                if (!int.TryParse(lines[1], NumberStyles.Integer, ic, out count)) return false;
                if (lines.Length < 2 + count) return false;

                // Reconstruir la lista
                _flightPlans.Clear();
                for (int i = 0; i < count; i++)
                {
                    string line = lines[2 + i];
                    string[] parts = line.Split(';');
                    if (parts.Length != 8) return false;

                    string id = parts[0];
                    double speed = double.Parse(parts[1], ic);
                    double ix = double.Parse(parts[2], ic);
                    double iy = double.Parse(parts[3], ic);
                    double fx = double.Parse(parts[4], ic);
                    double fy = double.Parse(parts[5], ic);
                    double cx = double.Parse(parts[6], ic);
                    double cy = double.Parse(parts[7], ic);

                    var fp = new FlightPlan(id, ix, iy, fx, fy, speed);
                    fp.SetCurrentPosition(new Position(cx, cy));
                    _flightPlans.AddFlightPlan(fp);
                }

                // Limpiar historial de deshacer y UI se regenerará en Load
                _history.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // --- NUEVO MÉTODO: Calcula los límites y la escala ---
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
        }

        private Point MapearCoordenadas(Position worldPos)
        {
            double scaledX = (worldPos.GetX() - minWorldX) * worldScale;
            int screenX = (int)(scaledX + padding);

            double scaledY = (worldPos.GetY() - minWorldY) * worldScale;
            int screenY = (int)((panel1.Height - padding) - scaledY);

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

                p.Width = 10;
                p.Height = 10;
                p.ClientSize = new Size(10, 10);

                Point screenPos = MapearCoordenadas(f.GetInitialPosition());
                int x = screenPos.X - p.Width / 2;
                int y = screenPos.Y - p.Height / 2;

                p.Location = new Point(x, y);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bmp = new Bitmap(p.Width, p.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Red, 0, 0, p.Width, p.Height);
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

            if (_flightPlans.DetectConflict(_securityDistance) == true)
            {
                MessageBox.Show("Conflicto");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Black, 2))
            using (Pen circlePen = new Pen(Color.Blue, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    FlightPlan flight = _flightPlans.GetFlightPlan(i);
                    Position origin = flight.GetInitialPosition();
                    Position dest = flight.GetFinalPosition();

                    Point p1 = MapearCoordenadas(origin);
                    Point p2 = MapearCoordenadas(dest);
                    e.Graphics.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);

                    Position current = flight.GetCurrentPosition();
                    Point centerPoint = MapearCoordenadas(current);

                    float radius = (float)(_securityDistance / 2 * worldScale);

                    float ellipseX = centerPoint.X - radius;
                    float ellipseY = centerPoint.Y - radius;

                    e.Graphics.DrawEllipse(circlePen, ellipseX, ellipseY, radius * 2, radius * 2);
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
                MessageBox.Show("Conflicto");
                simulationTimer.Stop();
            }
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            int numFlights = _flightPlans.getnum();
            bool conflictPredicted = false;
            int conflictA = -1, conflictB = -1;

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);

                    if (_flightPlans.predictConflict(a, b, _securityDistance))
                    {
                        conflictPredicted = true;
                        conflictA = i;
                        conflictB = j;
                        break;
                    }
                }
                if (conflictPredicted) break;
            }

            if (conflictPredicted)
            {
                var result = MessageBox.Show(
                    $"Se predice conflicto entre los vuelos {_flightPlans.GetFlightPlan(conflictA).GetId()} y {_flightPlans.GetFlightPlan(conflictB).GetId()}.\n" +
                    "¿Desea resolver el conflicto automáticamente ajustando la velocidad de uno de los vuelos?",
                    "Conflicto futuro detectado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    (bool resolved, double cspeed) = _flightPlans.ResolveConflictBySpeed(_flightPlans.GetFlightPlan(conflictA), _flightPlans.GetFlightPlan(conflictB), _securityDistance);
                    if (resolved)
                    {
                        MessageBox.Show(
                            $"La velocidad del vuelo {_flightPlans.GetFlightPlan(conflictB).GetId()} ha sido ajustada a {cspeed} para evitar el conflicto.",
                            "Conflicto resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"No se pudo resolver el conflicto ajustando la velocidad.",
                            "Conflicto no resuelto",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

            simulationTimer.Interval = (int)(_cycleTime * 1000);
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

        private void editspeedsbtn_Click(object sender, EventArgs e)
        {
            EditSpeedsForm editForm = new EditSpeedsForm(_flightPlans);
            editForm.Owner = this;
            editForm.ShowDialog();
        }

        // --- NUEVO: Guardar y salir ---
        private void saveExitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                simulationTimer.Stop();
                SaveSimulationState();
                MessageBox.Show("Simulación guardada correctamente.", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la simulación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}