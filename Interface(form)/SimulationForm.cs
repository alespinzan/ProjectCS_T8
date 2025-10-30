using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FlightLib;

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

        public SimulationForm(FlightPlanList flightPlans, double cycleTime, double securityDistance)
        {
            InitializeComponent();
            _flightPlans = flightPlans;
            _cycleTime = cycleTime;
            _securityDistance = securityDistance;
            panel1.Paint += panel1_Paint;

            // Initialize the simulation timer
            simulationTimer = new Timer();
            simulationTimer.Interval = (int)(_cycleTime * 1000); // Convert seconds to milliseconds
            simulationTimer.Tick += timer1_Tick;
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

                p.Width = 20;
                p.Height = 20;
                p.ClientSize = new Size(20, 20);

                Position initialPosition = f.GetInitialPosition();
                int x = (int)initialPosition.GetX() - p.Width / 2;
                int y = panel1.Height - (int)initialPosition.GetY() - p.Height / 2;

                p.Location = new Point(x, y);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                // Crear un bitmap del tamaño del PictureBox
                Bitmap bmp = new Bitmap(p.Width, p.Height);

                // Dibujar un círculo rojo dentro del PictureBox
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(Brushes.Red, 0, 0, p.Width, p.Height);
                }

                // Asignar el bitmap al PictureBox
                p.Image = bmp;

                p.Tag = i;
                p.Click += new System.EventHandler(this.flightInfo);

                panel1.Controls.Add(p);
                flights[i] = p;

                // Create and configure the label for the flight ID
                Label lbl = new Label();
                lbl.Text = f.GetId();
                lbl.AutoSize = true;
                lbl.Location = new Point(x + p.Width, y); // Position it next to the PictureBox
                panel1.Controls.Add(lbl);
                flightLabels[i] = lbl;

                i++;
            }
            // Envía los controles al fondo para que el dibujo quede encima
            foreach (Control c in panel1.Controls)
            {
                c.SendToBack();
            }
        }

        private void flightInfo(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            int i = (int)p.Tag;
            FlightInfo f = new FlightInfo();
            f.setFlight(_flightPlans.GetFlightPlan(i));
            f.ShowDialog();
        }

        private void cyclebtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);

                Position currentPosition = flight.GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
                flightLabels[i].Location = new Point(x + flights[i].Width, y);
            }
            panel1.Invalidate();

            // Comprobar conflictos después de mover los vuelos
            if (_flightPlans.detectConflict(_securityDistance) == true)
            {
                MessageBox.Show("Conflicto");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.Black, 2))
            using (Pen circlePen = new Pen(Color.Blue, 2))
            {
                pen.DashStyle = DashStyle.Dash;
                for (int i = 0; i < _flightPlans.getnum(); i++)
                {
                    FlightPlan flight = _flightPlans.GetFlightPlan(i);
                    Position origin = flight.GetInitialPosition();
                    Position dest = flight.GetFinalPosition();

                    int x1 = (int)origin.GetX();
                    int y1 = panel1.Height - (int)origin.GetY();
                    int x2 = (int)dest.GetX();
                    int y2 = panel1.Height - (int)dest.GetY();

                    e.Graphics.DrawLine(pen, x1, y1, x2, y2);

                    Position current = flight.GetCurrentPosition();
                    float centerX = (float)current.GetX();
                    float centerY = panel1.Height - (float)current.GetY();

                    float radius = (float)_securityDistance / 2;
                    float ellipseX = centerX - radius;
                    float ellipseY = centerY - radius;

                    // Dibuja el círculo de seguridad
                    e.Graphics.DrawEllipse(circlePen, ellipseX, ellipseY, radius * 2, radius * 2);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan flight = _flightPlans.GetFlightPlan(i);
                flight.Mover(_cycleTime);

                Position currentPosition = flight.GetCurrentPosition();
                int x = (int)currentPosition.GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)currentPosition.GetY() - flights[i].Height / 2;

                flights[i].Location = new Point(x, y);
                flightLabels[i].Location = new Point(x + flights[i].Width, y);
            }
            panel1.Invalidate();

            // Comprobar conflictos después de mover los vuelos
            if (_flightPlans.detectConflict(_securityDistance) == true)
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

        // ===========================
        // MÉTODO NUEVO: Reiniciar simulación
        // ===========================
        public void RestartSimulation()
        {
            simulationTimer.Stop();

            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan f = _flightPlans.GetFlightPlan(i);
                f.Restart();

                int x = (int)f.GetCurrentPosition().GetX() - flights[i].Width / 2;
                int y = panel1.Height - (int)f.GetCurrentPosition().GetY() - flights[i].Height / 2;
                flights[i].Location = new Point(x, y);
                flightLabels[i].Location = new Point(x + flights[i].Width, y);
            }

            panel1.Invalidate();
        }

        private void editspeedsbtn_Click(object sender, EventArgs e)
        {
            EditSpeedsForm editForm = new EditSpeedsForm(_flightPlans);
            editForm.Owner = this; // Esto permite que EditSpeedsForm llame a RestartSimulation()
            editForm.ShowDialog();
        }
    }
}