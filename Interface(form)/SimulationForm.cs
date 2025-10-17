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

        // <--- AÑADIDO FASE 5
        // Necesitamos guardar las coordenadas de pantalla de los aviones
        // para que el evento de clic pueda "verlas".
        private PointF _screenPt1;
        private PointF _screenPt2;

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

            // <--- AÑADIDO FASE 5
            // Suscribimos el formulario al evento MouseClick.
            // Usamos MouseClick en lugar de Click para obtener las coordenadas (e.Location)
            this.MouseClick += new MouseEventHandler(SimulationForm_MouseClick);
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

            // --- NOTA: He cambiado tus GetInitialPosition() por GetCurrentPosition() ---
            // Si usas Initial, el mapa se fija en el inicio y si los aviones
            // se mueven mucho, se salen del mapa. Usando Current se ajusta,
            // aunque puede ser mejor usar los 4 puntos (2 iniciales, 2 finales).
            // Si prefieres dejarlo como estaba, vuelve a poner GetInitialPosition() aquí.
            double x1 = fp1.GetCurrentPosition().GetX();
            double y1 = fp1.GetCurrentPosition().GetY();
            double x2 = fp2.GetCurrentPosition().GetX();
            double y2 = fp2.GetCurrentPosition().GetY();

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
            // Descomenta la siguiente línea si quieres que el mapa se reajuste en cada frame
            // InitializeWorldBounds(); 

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
                float py = (float)(drawRect.Bottom - (wy - minY) / (maxY - minY) * drawRect.Height); // Y invertida
                return new PointF(px, py);
            };

            // <--- MODIFICADO FASE 5
            // Guardamos los puntos en las variables de la clase
            _screenPt1 = map(x1, y1);
            _screenPt2 = map(x2, y2);

            // Fondo y contorno
            using (var bgBrush = new SolidBrush(Color.LightSkyBlue))
                g.FillRectangle(bgBrush, drawRect);
            using (var pen = new Pen(Color.DarkBlue, 2))
                g.DrawRectangle(pen, drawRect);

            // <--- MODIFICADO FASE 5
            // Usamos las variables de la clase para dibujar
            DrawAircraft(g, _screenPt1, Brushes.Red, $"A1: {fp1.GetId()}");
            DrawAircraft(g, _screenPt2, Brushes.Green, $"A2: {fp2.GetId()}");

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

        // <--- AÑADIDO FASE 5
        // Este es el manejador de clics para TODO el formulario.
        // Aquí comprobamos si el clic fue sobre uno de los aviones.
        private void SimulationForm_MouseClick(object sender, MouseEventArgs e)
        {
            // Obtener los planes de vuelo (¡revisar si son null!)
            FlightPlan fp1 = _flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = _flightPlans.GetFlightPlan(1);

            if (fp1 == null || fp2 == null) return; // No hacer nada si no hay aviones

            // Usamos el mismo radio que al dibujar
            int r = AircraftRadius;

            // Crear los "rectángulos de colisión" para cada avión
            // Estos son los cuadrados que rodean el círculo del avión
            RectangleF rect1 = new RectangleF(_screenPt1.X - r, _screenPt1.Y - r, r * 2, r * 2);
            RectangleF rect2 = new RectangleF(_screenPt2.X - r, _screenPt2.Y - r, r * 2, r * 2);

            // e.Location nos da el punto (en píxeles) donde el usuario hizo clic
            if (rect1.Contains(e.Location))
            {
                // Se hizo clic en el avión 1
                InfoVueloForm formularioInfo = new InfoVueloForm(fp1);
                formularioInfo.Show();
            }
            else if (rect2.Contains(e.Location))
            {
                // Se hizo clic en el avión 2
                InfoVueloForm formularioInfo = new InfoVueloForm(fp2);
                formularioInfo.Show();
            }
        }
    }
}