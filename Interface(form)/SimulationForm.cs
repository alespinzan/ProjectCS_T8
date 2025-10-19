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
        PictureBox[] flights;
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
                Bitmap image = new Bitmap("Punto.png");
                p.Image = (Image)image;

                p.Tag = i;
                p.Click += new System.EventHandler(this.flightInfo);

                panel1.Controls.Add(p);
                flights[i] = p;
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
            }
            panel1.Invalidate();

            // Comprobar conflictos después de mover los vuelos
            CheckConflicts();
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

                    float radius = (float)_securityDistance;
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
            }
            panel1.Invalidate();

            // Comprobar conflictos después de mover los vuelos
            CheckConflicts();
        }

        private void startbtn_Click(object sender, EventArgs e)
        {
            // Check for predicted conflicts before starting
            int numFlights = _flightPlans.getnum();
            bool conflictPredicted = false;
            int conflictA = -1, conflictB = -1;

            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);

                    if (WillFlightsConflict(a, b, _securityDistance))
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
                    // Try to resolve by adjusting the speed of the second flight
                    bool resolved = ResolveConflictBySpeed(_flightPlans.GetFlightPlan(conflictA), _flightPlans.GetFlightPlan(conflictB), _securityDistance);
                    if (resolved)
                    {
                        MessageBox.Show(
                            $"La velocidad del vuelo {_flightPlans.GetFlightPlan(conflictB).GetId()} ha sido ajustada para evitar el conflicto.",
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
                // If No, continue as usual
            }

            simulationTimer.Interval = (int)(_cycleTime * 1000); // Update interval in case _cycleTime changed
            simulationTimer.Start();
        }

        // Helper: Try to resolve conflict by reducing the speed of flight b
        private bool ResolveConflictBySpeed(FlightPlan a, FlightPlan b, double securityDistance)
        {
            double originalSpeed = b.GetVelocidad();
            double minSpeed = 0.1; // Minimum allowed speed (avoid zero)
            double step = originalSpeed / 20.0; // Try 20 steps

            for (double newSpeed = originalSpeed - step; newSpeed >= minSpeed; newSpeed -= step)
            {
                b.SetVelocidad(newSpeed);
                if (!WillFlightsConflict(a, b, securityDistance))
                {
                    return true; // Conflict resolved
                }
            }
            b.SetVelocidad(originalSpeed); // Restore if not resolved
            return false;
        }

        private void stopbtn_Click(object sender, EventArgs e)
        {
            simulationTimer.Stop();
        }

        private void infobtn_Click(object sender, EventArgs e)
        {
            FlightGrid form = new FlightGrid(_flightPlans);
            form.ShowDialog(this);
        }

        // Método para comprobar conflictos entre todos los pares de vuelos
        private void CheckConflicts()
        {
            int numFlights = _flightPlans.getnum();
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan a = _flightPlans.GetFlightPlan(i);
                for (int j = i + 1; j < numFlights; j++)
                {
                    FlightPlan b = _flightPlans.GetFlightPlan(j);
                    if (a.Conflicto(b, _securityDistance))
                    {
                        MessageBox.Show(
                            $"¡Conflicto detectado entre los vuelos {a.GetId()} y {b.GetId()}!\n" +
                            $"Distancia menor a la de seguridad ({_securityDistance}).",
                            "Conflicto de vuelos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return; // Solo muestra el primer conflicto encontrado en este ciclo
                    }
                }
            }
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

                    // Predict if their paths will ever be closer than _securityDistance
                    if (WillFlightsConflict(a, b, _securityDistance))
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

        private bool WillFlightsConflict(FlightPlan a, FlightPlan b, double securityDistance)
        {
            Position aStart = a.GetInitialPosition();
            Position aEnd = a.GetFinalPosition();
            Position bStart = b.GetInitialPosition();
            Position bEnd = b.GetFinalPosition();

            // Vector direction
            double ax = aEnd.GetX() - aStart.GetX();
            double ay = aEnd.GetY() - aStart.GetY();
            double bx = bEnd.GetX() - bStart.GetX();
            double by = bEnd.GetY() - bStart.GetY();

            // Relative velocity
            double vax = ax * a.GetVelocidad();
            double vay = ay * a.GetVelocidad();
            double vbx = bx * b.GetVelocidad();
            double vby = by * b.GetVelocidad();

            // Relative position and velocity
            double rx = aStart.GetX() - bStart.GetX();
            double ry = aStart.GetY() - bStart.GetY();
            double vx = vax - vbx;
            double vy = vay - vby;

            // Find time t where distance is minimized
            double tMin = 0;
            double denom = vx * vx + vy * vy;
            if (denom != 0)
            {
                tMin = -(rx * vx + ry * vy) / denom;
                tMin = Math.Max(0, tMin); // Only future times
            }

            // Positions at tMin
            double aX = aStart.GetX() + ax * a.GetVelocidad() * tMin;
            double aY = aStart.GetY() + ay * a.GetVelocidad() * tMin;
            double bX = bStart.GetX() + bx * b.GetVelocidad() * tMin;
            double bY = bStart.GetY() + by * b.GetVelocidad() * tMin;

            double dist = Math.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
            return dist < securityDistance;
        }
    }
}