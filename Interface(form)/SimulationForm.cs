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

        public SimulationForm(FlightPlanList flightPlans)
        {
            _flightPlans = flightPlans;
            this.Text = "Simulación - Espacio aéreo";
            this.ClientSize = new Size(600, 400);
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Obtener los dos planes (si existen)
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

            // Obtener posiciones iniciales
            var p1 = fp1.GetInitialPosition();
            var p2 = fp2.GetInitialPosition();

            // Intento de obtener coordenadas X/Y desde Position (se usa reflexión si no hay acceso directo)
            double x1 = GetPosX(p1);
            double y1 = GetPosY(p1);
            double x2 = GetPosX(p2);
            double y2 = GetPosY(p2);

            // Calcular bounding box del "mundo" (dos puntos iniciales + margen)
            double minX = Math.Min(x1, x2);
            double minY = Math.Min(y1, y2);
            double maxX = Math.Max(x1, x2);
            double maxY = Math.Max(y1, y2);

            // Si los dos puntos coinciden, expandir un poco para evitar división por cero
            if (Math.Abs(maxX - minX) < 1e-6)
            {
                maxX += 1;
                minX -= 1;
            }
            if (Math.Abs(maxY - minY) < 1e-6)
            {
                maxY += 1;
                minY -= 1;
            }

            // Área cliente disponible para dibujar (restar márgenes)
            var clientRect = this.ClientRectangle;
            var drawRect = new Rectangle(MarginPixels, MarginPixels, Math.Max(0, clientRect.Width - 2 * MarginPixels), Math.Max(0, clientRect.Height - 2 * MarginPixels));

            // Dibujar contorno del espacio aéreo
            using (var bgBrush = new SolidBrush(Color.LightSkyBlue))
            {
                g.FillRectangle(bgBrush, drawRect);
            }
            using (var pen = new Pen(Color.DarkBlue, 2))
            {
                g.DrawRectangle(pen, drawRect);
            }

            // Función de mapeo de coordenadas de mundo a píxeles
            Func<double, double, PointF> map = (wx, wy) =>
            {
                float px = (float)(drawRect.Left + (wx - minX) / (maxX - minX) * drawRect.Width);
                // invertir Y para que valores más altos queden arriba en pantalla
                float py = (float)(drawRect.Bottom - (wy - minY) / (maxY - minY) * drawRect.Height);
                return new PointF(px, py);
            };

            // Dibujar los dos vuelos como círculos con etiquetas
            var pt1 = map(x1, y1);
            var pt2 = map(x2, y2);

            DrawAircraft(g, pt1, Brushes.Red, $"A1: {fp1.GetId()}");
            DrawAircraft(g, pt2, Brushes.Green, $"A2: {fp2.GetId()}");

            // Leyenda simple en esquina
            using (var f = new Font("Segoe UI", 9))
            {
                g.DrawString($"Rango X: {minX:0.##} - {maxX:0.##}", f, Brushes.Black, new PointF(10, clientRect.Height - 40));
                g.DrawString($"Rango Y: {minY:0.##} - {maxY:0.##}", f, Brushes.Black, new PointF(10, clientRect.Height - 22));
            }
        }

        private static void DrawAircraft(Graphics g, PointF center, Brush brush, string label)
        {
            var r = AircraftRadius;
            var rect = new RectangleF(center.X - r, center.Y - r, r * 2, r * 2);
            g.FillEllipse(brush, rect);
            using (var pen = new Pen(Color.Black, 1))
            {
                g.DrawEllipse(pen, rect);
            }
            using (var f = new Font("Segoe UI", 8))
            {
                g.DrawString(label, f, Brushes.Black, center.X + r + 3, center.Y - r);
            }
        }

        // Helpers para acceder a Position: se asume que Position tiene GetX() y GetY() o propiedades X/Y.
        private double GetPosX(object position)
        {
            if (position == null) return 0;
            var type = position.GetType();
            var mx = type.GetMethod("GetX");
            if (mx != null) return Convert.ToDouble(mx.Invoke(position, null));
            var px = type.GetProperty("X");
            if (px != null) return Convert.ToDouble(px.GetValue(position));
            return 0;
        }

        private double GetPosY(object position)
        {
            if (position == null) return 0;
            var type = position.GetType();
            var my = type.GetMethod("GetY");
            if (my != null) return Convert.ToDouble(my.Invoke(position, null));
            var py = type.GetProperty("Y");
            if (py != null) return Convert.ToDouble(py.GetValue(position));
            return 0;
        }
    }
}
