using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightPlanForm : Form
    {
        // Expose the created plans so Main can read them after ShowDialog()
        public FlightPlan FlightPlan1 { get; private set; }
        public FlightPlan FlightPlan2 { get; private set; }

        // Default velocity used if no velocity TextBox is present or left empty

        public FlightPlanForm()
        {
            InitializeComponent();

            // Ensure Cancel button closes the dialog (designer may or may not have wired it)
            if (cancelButton != null)
                cancelButton.Click += CancelButton_Click;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Read and validate IDs
                string id1 = id1box.Text.Trim();
                string id2 = id2box.Text.Trim();

                double o1x, o1y, d1x, d1y;
                double o2x, o2y, d2x, d2y;

                // Origin 1
                string[] o1 = origin1box.Text.Split(',');

                o1x = Convert.ToDouble(o1[0]);
                o1y = Convert.ToDouble(o1[1]);

                string[] d1 = destination1box.Text.Split(',');

                d1x = Convert.ToDouble(d1[0]);
                d1y = Convert.ToDouble(d1[1]);

                double velocity1 = Convert.ToDouble(velocity1box.Text);

                string[] o2 = origin1box.Text.Split(',');

                o2x = Convert.ToDouble(o2[0]);
                o2y = Convert.ToDouble(o2[1]);

                string[] d2 = destination1box.Text.Split(',');

                d2x = Convert.ToDouble(d2[0]);
                d2y = Convert.ToDouble(d2[1]);

                double velocity2 = Convert.ToDouble(velocity1box.Text);

                FlightPlan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, velocity1);
                FlightPlan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, velocity2);

            }
            catch
            {
                MessageBox.Show("Format Error");
                return;
            }

            // 5) Return OK to caller and close
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
