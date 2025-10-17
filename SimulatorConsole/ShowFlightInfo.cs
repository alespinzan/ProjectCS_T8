using FlightLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorConsole
{
    public partial class ShowFlightInfo : Form
    {
        FlightPlan myFlight;
        public ShowFlightInfo()
        {
            InitializeComponent();
        }
        public void SetFlightPlan(FlightPlan f)
        {
            this.myFlight = f;
        }

        // <--- CORREGIDO
        // Se ha modificado esta función para usar GetX() y GetY()
        private void ShowFlightInfo_Load(object sender, EventArgs e)
        {
            // Usamos GetX() y GetY() en lugar de .X e .Y
            Xbox.Text = Convert.ToString(myFlight.GetCurrentPosition().GetX());
            YBox.Text = Convert.ToString(myFlight.GetCurrentPosition().GetY());

            // Esta línea ya estaba correcta, ¡bien hecho!
            SpeedBox.Text = Convert.ToString(myFlight.GetVelocidad());
        }
        // --->

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}