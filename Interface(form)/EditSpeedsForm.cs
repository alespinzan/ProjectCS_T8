using System;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class EditSpeedsForm : Form
    {
        private FlightPlanList _flightPlans;

        public EditSpeedsForm(FlightPlanList flightPlans)
        {
            InitializeComponent();

            _flightPlans = flightPlans;

            // Configurar DataGridView
            dgvFlights.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFlights.Columns.Clear();

            // Crear columnas
            DataGridViewTextBoxColumn colID = new DataGridViewTextBoxColumn();
            colID.HeaderText = "ID";
            colID.Name = "colID";
            colID.ReadOnly = true;

            DataGridViewTextBoxColumn colSpeed = new DataGridViewTextBoxColumn();
            colSpeed.HeaderText = "Speed";
            colSpeed.Name = "colSpeed";
            colSpeed.ValueType = typeof(double);

            dgvFlights.Columns.Add(colID);
            dgvFlights.Columns.Add(colSpeed);

            // Llenar filas con los vuelos y asignar Tag
            for (int i = 0; i < _flightPlans.getnum(); i++)
            {
                FlightPlan f = _flightPlans.GetFlightPlan(i);
                int rowIndex = dgvFlights.Rows.Add(f.GetId(), f.GetVelocidad());
                dgvFlights.Rows[rowIndex].Tag = f; // Guardar FlightPlan en la fila
            }

            // Eventos
            dgvFlights.CellValueChanged += DgvFlights_CellValueChanged;
            dgvFlights.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvFlights.IsCurrentCellDirty)
                    dgvFlights.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            // Asignar evento Click del botón del diseñador
            savebtn.Click += Savebtn_Click;
        }

        // Cambiar velocidad al editar celda
        private void DgvFlights_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1) return; // Solo columna Speed

            FlightPlan flight = dgvFlights.Rows[e.RowIndex].Tag as FlightPlan;
            if (flight == null) return;

            double newSpeed;
            if (double.TryParse(dgvFlights.Rows[e.RowIndex].Cells[1].Value.ToString(), out newSpeed))
            {
                flight.SetVelocidad(newSpeed);

                // Reiniciar simulación en SimulationForm
                if (this.Owner is SimulationForm parentForm)
                    parentForm.RestartSimulation();
            }
            else
            {
                MessageBox.Show("Introduce un número válido para la velocidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Botón Guardar
        private void Savebtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvFlights.Rows)
            {
                if (row.IsNewRow) continue;
                FlightPlan flight = row.Tag as FlightPlan;
                if (flight != null)
                {
                    double newSpeed;
                    if (double.TryParse(row.Cells["colSpeed"].Value.ToString(), out newSpeed))
                        flight.SetVelocidad(newSpeed);
                }
            }

            // Reiniciar simulación
            if (this.Owner is SimulationForm parentForm)
                parentForm.RestartSimulation();

            this.Close();
        }
    }
}
