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
    public partial class SafetySettingsForm : Form
    {
        // Backing fields (optional since we expose properties)
        // Expose parsed values to callers (read-only)
        public double CycleTime { get; private set; }
        public double SecurityDistance { get; private set; }

        public SafetySettingsForm()
        {
            InitializeComponent();

            // Ensure Cancel button closes the dialog (designer may or may not have wired it)
            if (cancelButton != null)
                cancelButton.Click += cancelButton_Click;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                double cycleTime = Convert.ToDouble(cyclebox.Text);
                double securityDistance = Convert.ToDouble(securitybox.Text);

                CycleTime = cycleTime;
                SecurityDistance = securityDistance;
            }
            catch
            {
                MessageBox.Show("Format Error");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

       
    }
}
