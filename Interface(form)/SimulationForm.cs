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
        private const int MarginPixels = 20;
        private const int AircraftRadius = 6;

        private Button btnMove;

        // Límites del “mundo” fijos
        private double minX, minY, maxX, maxY;
        private bool worldInitialized = false;

        public SimulationForm(FlightPlanList flightPlans)
        {
            InitializeComponent();

            _flightPlans = flightPlans;
            this.Text = "Simulación - Espacio aéreo";
            this.ClientSize = new Size(600, 450); // espacio para botón
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterParent;

            // Crear botón “Mover aviones”
            btnMove = new Button
            {
                Text = "Mover aviones",
                Width = 120,
                Height = 30,
                Top = this.ClientSize.Height - 40,
                Left = 10
            };
            btnMove.Click += BtnMove_Click;
            this.Controls.Add(btnMove);
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            MoveAircraftCycle();
        }

        private void MoveAircraftCycle()
        {
            _flightPlans.Mover(1.0); // 1 minuto de simulación
            this.Invalidate();       // redibujar
        }

        // Inicializa límites del mundo usando posiciones iniciales
        private void InitializeWorldBounds()
        {
            FlightPlan fp1 = _flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = _flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null) return;

            double x1 = fp1.GetInitialPosition().GetX();
            double y1 = fp1.GetInitialPosition().GetY();
            double x2 = fp2.GetInitialPosition().GetX();
            double y2 = fp2.GetInitialPosition().GetY();

            minX = Math.Min(x1, x2);
            minY = Math.Min(y1, y2);
            maxX = Math.Max(x1, x2);
            maxY = Math.Max(y1, y2);

            // Evitar división por cero
            if (Math.Abs(maxX - minX) < 1e-6) { maxX += 1; minX -= 1; }
            if (Math.Abs(maxY - minY) < 1e-6) { maxY += 1; minY -= 1; }

            worldInitialized = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (!worldInitialized)
                InitializeWorldBounds();

            FlightPlan fp1 = _flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = _flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null)
            {
                using (var f = new Font("Segoe UI", 10))
                {
                    g.DrawString("No hay suficientes planes de vuelo para mostrar la simulación.", f, Brushes.Black, new PointF(10, 10));
                }
                return;
            }

            // Posiciones actuales
            double x1 = fp1.GetCurrentPosition().GetX();
            double y1 = fp1.GetCurrentPosition().GetY();
            double x2 = fp2.GetCurrentPosition().GetX();
            double y2 = fp2.GetCurrentPosition().GetY();

            var clientRect = this.ClientRectangle;
            var drawRect = new Rectangle(MarginPixels, MarginPixels, Math.Max(0, clientRect.Width - 2 * MarginPixels), Math.Max(0, clientRect.Height - 60));

            // mapeo mundo -> píxeles usando límites fijos
            Func<double, double, PointF> map = (wx, wy) =>
            {
                float px = (float)(drawRect.Left + (wx - minX) / (maxX - minX) * drawRect.Width);
                float py = (float)(drawRect.Bottom - (wy - minY) / (maxY - minY) * drawRect.Height);
                return new PointF(px, py);
            };

            var pt1 = map(x1, y1);
            var pt2 = map(x2, y2);

            // Fondo y contorno
            using (var bgBrush = new SolidBrush(Color.LightSkyBlue))
                g.FillRectangle(bgBrush, drawRect);
            using (var pen = new Pen(Color.DarkBlue, 2))
                g.DrawRectangle(pen, drawRect);

            DrawAircraft(g, pt1, Brushes.Red, $"A1: {fp1.GetId()}");
            DrawAircraft(g, pt2, Brushes.Green, $"A2: {fp2.GetId()}");

            // Leyenda de rangos fijos
            using (var f = new Font("Segoe UI", 9))
            {
                g.DrawString($"Rango X: {minX:0.##} - {maxX:0.##}", f, Brushes.Black, new PointF(10, clientRect.Height - 50));
                g.DrawString($"Rango Y: {minY:0.##} - {maxY:0.##}", f, Brushes.Black, new PointF(10, clientRect.Height - 32));
            }
        }

        private static void DrawAircraft(Graphics g, PointF center, Brush brush, string label)
        {
            int r = AircraftRadius;
            var rect = new RectangleF(center.X - r, center.Y - r, r * 2, r * 2);
            g.FillEllipse(brush, rect);
            using (var pen = new Pen(Color.Black, 1))
                g.DrawEllipse(pen, rect);
            using (var f = new Font("Segoe UI", 8))
                g.DrawString(label, f, Brushes.Black, center.X + r + 3, center.Y - r);
        }
    }
}
