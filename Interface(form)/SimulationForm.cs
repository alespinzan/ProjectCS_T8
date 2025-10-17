using System;
using System.Drawing;
using System.Drawing.Drawing2D; // Necesario para el DashStyle
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

        private double minX, minY, maxX, maxY;
        private bool worldInitialized = false;

        private PointF _screenPt1;
        private PointF _screenPt2;

        public SimulationForm(FlightPlanList flightPlans)
        {
            InitializeComponent();

            _flightPlans = flightPlans;
            this.Text = "Simulación - Espacio aéreo";
            this.ClientSize = new Size(600, 450);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterParent;

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

            this.MouseClick += new MouseEventHandler(SimulationForm_MouseClick);
        }

        private void BtnMove_Click(object sender, EventArgs e)
        {
            MoveAircraftCycle();
        }

        private void MoveAircraftCycle()
        {
            _flightPlans.Mover(1.0);
            this.Invalidate();
        }

        private void InitializeWorldBounds()
        {
            FlightPlan fp1 = _flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = _flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null) return;

            // Usamos las posiciones INICIALES y FINALES para definir el mapa
            double x1_i = fp1.GetInitialPosition().GetX();
            double y1_i = fp1.GetInitialPosition().GetY();
            double x1_f = fp1.GetFinalPosition().GetX();
            double y1_f = fp1.GetFinalPosition().GetY();

            double x2_i = fp2.GetInitialPosition().GetX();
            double y2_i = fp2.GetInitialPosition().GetY();
            double x2_f = fp2.GetFinalPosition().GetX();
            double y2_f = fp2.GetFinalPosition().GetY();

            minX = Math.Min(Math.Min(x1_i, x1_f), Math.Min(x2_i, x2_f));
            minY = Math.Min(Math.Min(y1_i, y1_f), Math.Min(y2_i, y2_f));
            maxX = Math.Max(Math.Max(x1_i, x1_f), Math.Max(x2_i, x2_f));
            maxY = Math.Max(Math.Max(y1_i, y1_f), Math.Max(y2_i, y2_f));

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
                    g.DrawString("No hay suficientes planes de vuelo...", f, Brushes.Black, new PointF(10, 10));
                }
                return;
            }

            // Posiciones actuales (para dibujar el avión)
            double x1 = fp1.GetCurrentPosition().GetX();
            double y1 = fp1.GetCurrentPosition().GetY();
            double x2 = fp2.GetCurrentPosition().GetX();
            double y2 = fp2.GetCurrentPosition().GetY();

            var clientRect = this.ClientRectangle;
            var drawRect = new Rectangle(MarginPixels, MarginPixels, Math.Max(0, clientRect.Width - 2 * MarginPixels), Math.Max(0, clientRect.Height - 60));

            Func<double, double, PointF> map = (wx, wy) =>
            {
                float px = (float)(drawRect.Left + (wx - minX) / (maxX - minX) * drawRect.Width);
                float py = (float)(drawRect.Bottom - (wy - minY) / (maxY - minY) * drawRect.Height); // Y invertida
                return new PointF(px, py);
            };

            _screenPt1 = map(x1, y1);
            _screenPt2 = map(x2, y2);

            // Fondo y contorno
            using (var bgBrush = new SolidBrush(Color.LightSkyBlue))
                g.FillRectangle(bgBrush, drawRect);
            using (var pen = new Pen(Color.DarkBlue, 2))
                g.DrawRectangle(pen, drawRect);


            // <--- AÑADIDO FASE 6: Dibujar Trayectorias ---

            // 1. Definir los "lápices" (Pens) para las líneas
            //    Usamos los mismos colores que los aviones (Rojo, Verde) pero con línea discontinua
            using (Pen penTrazo1 = new Pen(Color.Red, 2))
            using (Pen penTrazo2 = new Pen(Color.Green, 2))
            {
                penTrazo1.DashStyle = DashStyle.Dash; // Estilo discontinuo
                penTrazo2.DashStyle = DashStyle.Dash; // Estilo discontinuo

                // 2. Obtener coordenadas INICIALES y FINALES (del mundo real)
                Position inicio1 = fp1.GetInitialPosition();
                Position fin1 = fp1.GetFinalPosition();
                Position inicio2 = fp2.GetInitialPosition();
                Position fin2 = fp2.GetFinalPosition();

                // 3. Mapear esas coordenadas a PÍXELES de la pantalla
                PointF ptInicio1_screen = map(inicio1.GetX(), inicio1.GetY());
                PointF ptFin1_screen = map(fin1.GetX(), fin1.GetY());

                PointF ptInicio2_screen = map(inicio2.GetX(), inicio2.GetY());
                PointF ptFin2_screen = map(fin2.GetX(), fin2.GetY());

                // 4. Dibujar las líneas (antes de dibujar los aviones, para que queden por debajo)
                g.DrawLine(penTrazo1, ptInicio1_screen, ptFin1_screen);
                g.DrawLine(penTrazo2, ptInicio2_screen, ptFin2_screen);
            }
            // ---> FIN FASE 6


            // Dibujar los aviones (esto es de la Fase 4/5)
            DrawAircraft(g, _screenPt1, Brushes.Red, $"A1: {fp1.GetId()}");
            DrawAircraft(g, _screenPt2, Brushes.Green, $"A2: {fp2.GetId()}");

            // Leyenda
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

        private void SimulationForm_MouseClick(object sender, MouseEventArgs e)
        {
            FlightPlan fp1 = _flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = _flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null) return;

            int r = AircraftRadius;
            RectangleF rect1 = new RectangleF(_screenPt1.X - r, _screenPt1.Y - r, r * 2, r * 2);
            RectangleF rect2 = new RectangleF(_screenPt2.X - r, _screenPt2.Y - r, r * 2, r * 2);

            if (rect1.Contains(e.Location))
            {
                InfoVueloForm formularioInfo = new InfoVueloForm(fp1);
                formularioInfo.Show();
            }
            else if (rect2.Contains(e.Location))
            {
                InfoVueloForm formularioInfo = new InfoVueloForm(fp2);
                formularioInfo.Show();
            }
        }
    }
}