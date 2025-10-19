using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightInfo : Form
    {
        FlightPlan currentFP;
        public FlightInfo()
        {
            InitializeComponent();
        }

        public void setFlight(FlightPlan f)
        {
            this.currentFP = f;
        }

        private void FlightInfo_Load(object sender, EventArgs e)
        {
            Position pos = currentFP.GetCurrentPosition();
            double x = pos.GetX();
            double y = pos.GetY();
            xbox.Text = x.ToString("F2");
            ybox.Text = y.ToString("F2");
            Idbox.Text = currentFP.GetId();
            speedbox.Text = currentFP.GetVelocidad().ToString("F2");
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
