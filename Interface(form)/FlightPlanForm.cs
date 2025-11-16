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
        public FlightPlanList _flightplans;

        public FlightPlan Flightplan1 {  get; set; }
        public FlightPlan Flightplan2 { get; set; }
        // Default velocity used if no velocity TextBox is present or left empty

        public FlightPlanForm(FlightPlanList flightplans)
        {
            InitializeComponent();
            _flightplans = flightplans;

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

                string[] o2 = origin2box.Text.Split(',');

                o2x = Convert.ToDouble(o2[0]);
                o2y = Convert.ToDouble(o2[1]);

                string[] d2 = destination2box.Text.Split(',');

                d2x = Convert.ToDouble(d2[0]);
                d2y = Convert.ToDouble(d2[1]);

                double velocity2 = Convert.ToDouble(velocity2box.Text);

                Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, velocity1);
                Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, velocity2);

                // Añadir los planes a la lista
                _flightplans.AddFlightPlan(Flightplan1);
                _flightplans.AddFlightPlan(Flightplan2);
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

        private void nonConflictbtn_Click(object sender, EventArgs e)
        {
            string id1 = "FP-A100";
            string id2 = "FP-B200";

            double o1x = 0.0;
            double o1y = 0.0;
            double d1x = 500.0;
            double d1y = 1000.0;
            double v1 = 500.0;

            double o2x = 500.0;
            double o2y = 500;
            double d2x = 0.0;
            double d2y = 0.0;
            double v2 = 600.0;

            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2);

            // Añade los planes a la lista compartida
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void conflictbtn_Click(object sender, EventArgs e)
        {
            // Manually defined, non-conflicting flight plans
            string id1 = "FP-A100";
            string id2 = "FP-B200";

            // Flight 1: travels along Y = 0 from X = 0 to X = 1000
            double o1x = 0.0;
            double o1y = 0.0;
            double d1x = 500.0;
            double d1y = 500;
            double v1 = 500.0; // meters per second (example)

            // Flight 2: travels along Y = 2000 from X = 0 to X = 1000 (well separated in Y)
            double o2x = 500.0;
            double o2y = 0;
            double d2x = 0.0;
            double d2y = 500;
            double v2 = 500; // meters per second (example)

            // Create the FlightPlan objects using the available constructor
            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2);

            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);

            // Return OK to the caller and close the dialog
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            _flightplans.Clear();
            MessageBox.Show("La lista de planes de vuelo ha sido vaciada.", "Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browcebtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select Flight Plan File";
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    int plansAdded = _flightplans.loadFlpFromFile(filePath);

                    if (plansAdded >= 0)
                    {
                        MessageBox.Show($"{plansAdded} flight plan(s) loaded successfully.", "File Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error reading or parsing the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
