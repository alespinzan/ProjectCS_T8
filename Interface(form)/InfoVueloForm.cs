using FlightLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Interface_form_
{
    public partial class InfoVueloForm : Form
    {
        private FlightPlan vueloMostrado;

        private Label labelTituloID;
        private Label lblID;
        private Label labelTituloPos;
        private Label lblPosicion;
        private Label labelTituloDest;
        private Label lblDestino;
        private Label labelTituloVel;
        private Label lblVelocidad;

        public InfoVueloForm(FlightPlan vuelo)
        {
            InitializeComponent();

            this.vueloMostrado = vuelo;
            this.Text = "Información del Vuelo";
            this.ClientSize = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;

            // --- INICIALIZAR CONTROLES ---
            this.labelTituloID = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold), Location = new Point(20, 20), Text = "Identificador:" };
            this.Controls.Add(this.labelTituloID);
            this.lblID = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F), Location = new Point(150, 20) };
            this.Controls.Add(this.lblID);

            this.labelTituloPos = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold), Location = new Point(20, 50), Text = "Posición Actual:" };
            this.Controls.Add(this.labelTituloPos);
            this.lblPosicion = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F), Location = new Point(150, 50) };
            this.Controls.Add(this.lblPosicion);

            this.labelTituloDest = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold), Location = new Point(20, 80), Text = "Destino:" };
            this.Controls.Add(this.labelTituloDest);
            this.lblDestino = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F), Location = new Point(150, 80) };
            this.Controls.Add(this.lblDestino);

            this.labelTituloVel = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold), Location = new Point(20, 110), Text = "Velocidad:" };
            this.Controls.Add(this.labelTituloVel);
            this.lblVelocidad = new Label { AutoSize = true, Font = new Font("Microsoft Sans Serif", 10F), Location = new Point(150, 110) };
            this.Controls.Add(this.lblVelocidad);

            ActualizarDatos();
        }

        // <--- CORREGIDO
        // Esta función ahora usa GetVelocidad(), GetX() y GetY()
        private void ActualizarDatos()
        {
            lblID.Text = vueloMostrado.GetId();
            lblPosicion.Text = string.Format("({0:F2}, {1:F2})", vueloMostrado.GetCurrentPosition().GetX(), vueloMostrado.GetCurrentPosition().GetY());
            lblDestino.Text = string.Format("({0}, {1})", vueloMostrado.GetFinalPosition().GetX(), vueloMostrado.GetFinalPosition().GetY());
            lblVelocidad.Text = string.Format("{0:F2} km/h", vueloMostrado.GetVelocidad());
        }
        // --->
    }
}