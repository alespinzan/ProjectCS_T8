using FlightLib;
using System;
using System.Diagnostics;
using System.Windows.Forms;

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
        }

        private void flightPlansToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Open modal dialog so we can get the created FlightPlan objects
            //using (var form = new FlightPlanForm())
            FlightPlanForm form = new FlightPlanForm(flightPlans);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // No reasignes flightPlans, solo usa la instancia existente
                flightPlans.EscribeConsola();
                MessageBox.Show("Flight plans added to Main.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void securitySettingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SafetySettingsForm form = new SafetySettingsForm();
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Read values exposed by the form
                    securityDistance = form.SecurityDistance;
                    cycleTime = form.CycleTime;

                    // Optional: reflect change in UI or notify user
                    MessageBox.Show($"Security distance set to {securityDistance}\nCycle time set to {cycleTime}", "Safety Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Obtener los dos primeros planes guardados (si existen)
            FlightPlan fp1 = flightPlans.GetFlightPlan(0);
            FlightPlan fp2 = flightPlans.GetFlightPlan(1);
            try
            {
                if (fp1 == null || fp2 == null)
                {
                    MessageBox.Show("Debe añadir al menos dos planes de vuelo antes de iniciar la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir formulario de simulación (modal)
                SimulationForm form = new SimulationForm(flightPlans, cycleTime, securityDistance);
                form.ShowDialog(this);

            }
            catch (Exception)
            {
                MessageBox.Show("Security setup missing");
            }
            

            
           
        }

    }
}
