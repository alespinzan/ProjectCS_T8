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
        public FlightPlanList _flightplans;
        public FlightPlan Flightplan1 {  get; set; }
        public FlightPlan Flightplan2 { get; set; }

        public FlightPlanForm(FlightPlanList flightplans)
        {
            InitializeComponent();
            _flightplans = flightplans;

            UpdateDataSource();
        }

        private void UpdateDataSource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Company", typeof(string));
            dt.Columns.Add("Initial X", typeof(double));
            dt.Columns.Add("Initial Y", typeof(double));
            dt.Columns.Add("Final X", typeof(double));
            dt.Columns.Add("Final Y", typeof(double));
            dt.Columns.Add("Velocity", typeof(double));

            for (int i = 0; i < _flightplans.getnum(); i++)
            {
                FlightPlan fp = _flightplans.GetFlightPlan(i);
                dt.Rows.Add(
                    fp.GetId(),
                    fp.GetcompanyName(),
                    fp.GetInitialPosition().GetX(),
                    fp.GetInitialPosition().GetY(),
                    fp.GetFinalPosition().GetX(),
                    fp.GetFinalPosition().GetY(),
                    fp.GetVelocidad()
                );
            }

            flightPlansDataGridView.DataSource = dt;
            flightPlansDataGridView.Refresh();
        }


        private void acceptButton_Click(object sender, EventArgs e)
        {
            // Return OK to caller and close
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

            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1, null);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2, null);

            // Añade los planes a la lista compartida
            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);
            _flightplans.setname("nonConflict");

            UpdateDataSource();
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
            Flightplan1 = new FlightPlan(id1, o1x, o1y, d1x, d1y, v1, null);
            Flightplan2 = new FlightPlan(id2, o2x, o2y, d2x, d2y, v2, null);

            _flightplans.AddFlightPlan(Flightplan1);
            _flightplans.AddFlightPlan(Flightplan2);
            _flightplans.setname("conflict");

            UpdateDataSource();
        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            _flightplans.Clear();
            UpdateDataSource();
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
                        UpdateDataSource();
                    }
                    else
                    {
                        MessageBox.Show("Error reading or parsing the file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string id = id1box.Text.Trim();
                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Flight ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string companyName = operatorbox.Text.Trim();
                if (companyName.Length == 0)
                {
                    companyName = null;
                }

                string[] originCoords = origin1box.Text.Split(',');
                double originX = Convert.ToDouble(originCoords[0]);
                double originY = Convert.ToDouble(originCoords[1]);

                string[] destCoords = destination1box.Text.Split(',');
                double destX = Convert.ToDouble(destCoords[0]);
                double destY = Convert.ToDouble(destCoords[1]);

                double velocity = Convert.ToDouble(velocity1box.Text);

                var newFlightPlan = new FlightPlan(id, originX, originY, destX, destY, velocity, companyName);
                _flightplans.AddFlightPlan(newFlightPlan);

                UpdateDataSource();

                id1box.Clear();
                operatorbox.Clear();
                origin1box.Clear();
                destination1box.Clear();
                velocity1box.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Format Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void undobtn_Click(object sender, EventArgs e)
        {
            if (_flightplans.getnum() > 0)
            {
                _flightplans.RemoveLast();
                UpdateDataSource();
            }
            else
            {
                MessageBox.Show("No flight plans to remove.", "Undo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (flightPlansDataGridView.SelectedRows.Count > 0)
            {
                // Get the ID from the selected row in the DataTable
                string selectedId = flightPlansDataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
                
                // Remove it from the list using its ID
                _flightplans.RemoveById(selectedId);

                // Refresh the grid
                UpdateDataSource();
            }
            else
            {
                MessageBox.Show("Please select a flight plan to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
