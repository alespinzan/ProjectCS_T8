using FlightLib;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO; // <-- Importante para leer archivos

namespace Interface_form_
{
    public partial class Main : Form
    {
        private FlightPlanList flightPlans = new FlightPlanList();
        private double securityDistance;
        private double cycleTime;

        public Main()
        {
            InitializeComponent();
            UpdateParameterLabels();
            // This event handler will be called when the form is closed
            this.FormClosed += new FormClosedEventHandler(Main_FormClosed);
        }

        private void UpdateParameterLabels()
        {
            simlbl.Text = !string.IsNullOrEmpty(flightPlans.getname()) ? flightPlans.getname() : "-";
            cyclelbl.Text = cycleTime > 0 ? cycleTime.ToString() : "-";
            securitylbl.Text = securityDistance > 0 ? securityDistance.ToString() : "-";
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            // This ensures the entire application exits
            Application.Exit();
        }

        // --- MÉTODO 'securitySettings' (CORREGIDO) ---
        private void securitySettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafetySettingsForm form = new SafetySettingsForm();
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Leer solo los 2 valores
                    securityDistance = form.SecurityDistance;
                    cycleTime = form.CycleTime;
                    // (Hemos quitado 'maxPasajeros')
                    UpdateParameterLabels();
                }
            }
        }

        // --- MÉTODO 'showToolStripMenuItem_Click' (CORREGIDO) ---
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlightPlan fp1 = flightPlans.GetFlightPlan(0);

            try
            {
                if (fp1 == null)
                {
                    MessageBox.Show("Debe añadir al menos un plan de vuelo antes de iniciar la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                SimulationForm form = new SimulationForm(flightPlans, cycleTime, securityDistance);
                form.ShowDialog(this);
            }
            catch (Exception)
            {
                MessageBox.Show("Security setup missing");
            }
        }

        private void flightPlansToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FlightPlanForm form = new FlightPlanForm(flightPlans);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                flightPlans.EscribeConsola();
                MessageBox.Show("Flight plans added to Main.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateParameterLabels();
            }

            UpdateParameterLabels();
        }

        private void OperatorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new OperatorsF())
            {
                form.ShowDialog(this);
            }
        }

    }
}