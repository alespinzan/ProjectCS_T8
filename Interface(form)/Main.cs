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
            FlightPlanForm form = new FlightPlanForm();
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    // Read the public properties that FlightPlanForm exposes
                    if (form.FlightPlan1 != null)
                        flightPlans.AddFlightPlan(form.FlightPlan1);

                    if (form.FlightPlan2 != null)
                        flightPlans.AddFlightPlan(form.FlightPlan2);

                    // Optional: reflect change in UI, write to console, or notify user
                    flightPlans.EscribeConsola();

                    // Added: print details to Debug output and Console
                    PrintFlightPlans();

                    MessageBox.Show("Flight plans added to Main.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

            if (fp1 == null || fp2 == null)
            {
                MessageBox.Show("Debe añadir al menos dos planes de vuelo antes de iniciar la simulación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Abrir formulario de simulación (modal)
            using (var form = new SimulationForm(flightPlans))
            {
                form.ShowDialog(this);
            }
        }

        // Helper: imprime todos los flight plans almacenados en flightPlans.
        private void PrintFlightPlans()
        {
            int i = 0;
            while (true)
            {
                var fp = flightPlans.GetFlightPlan(i);
                if (fp == null) break;

                var id = fp.GetId() ?? "<null id>";
                var ip = fp.GetInitialPosition();
                var fpPos = fp.GetFinalPosition();
                string line = string.Format("FlightPlan[{0}] ID={1} Initial=({2:F2},{3:F2}) Final=({4:F2},{5:F2}) Vel={6:F2}",
                    i,
                    id,
                    ip != null ? ip.GetX() : 0.0,
                    ip != null ? ip.GetY() : 0.0,
                    fpPos != null ? fpPos.GetX() : 0.0,
                    fpPos != null ? fpPos.GetY() : 0.0,
                    fp.GetVelocidad()
                );

                Debug.WriteLine(line);    // Ver en Output -> Debug
                Console.WriteLine(line);  // Ver únicamente si la app tiene consola
                i++;
            }
        }
    }
}
