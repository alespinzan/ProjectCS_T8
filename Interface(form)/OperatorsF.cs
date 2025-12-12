using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DATAmanager;

namespace Interface_form_
{
    public partial class OperatorsF : Form
    {
        private readonly gestorBBDD _db = new gestorBBDD();
        private DataTable _airlinesTable;

        public OperatorsF()
        {
            InitializeComponent();
            operatosDataview.SelectionChanged += operatosDataview_SelectionChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                _db.Open();
                ConfigureGrid();
                RefreshAirlines();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to load airline data. {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _db.Close();
            base.OnFormClosed(e);
        }

        private void ConfigureGrid()
        {
            operatosDataview.AutoGenerateColumns = true;
            operatosDataview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            operatosDataview.MultiSelect = false;
            operatosDataview.ReadOnly = true;
            operatosDataview.AllowUserToAddRows = false;
        }

        private void RefreshAirlines()
        {
            _airlinesTable = _db.GetAllAirlines();
            operatosDataview.DataSource = _airlinesTable;

            if (operatosDataview.Columns.Contains("name"))
            {
                operatosDataview.Columns["name"].HeaderText = "Name";
            }
            if (operatosDataview.Columns.Contains("phone"))
            {
                operatosDataview.Columns["phone"].HeaderText = "Phone";
            }
            if (operatosDataview.Columns.Contains("email"))
            {
                operatosDataview.Columns["email"].HeaderText = "Email";
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string name = namebox.Text.Trim();
            string phone = phonebox.Text.Trim();
            string email = mailbox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Company name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                namebox.Focus();
                return;
            }

            try
            {
                bool updated = _db.InsertOrUpdateAirline(name, phone, email);
                if (updated)
                {
                    RefreshAirlines();
                    operatosDataview.ClearSelection();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save company. {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DataRow row = GetSelectedRow();
            if (row == null)
            {
                MessageBox.Show("Select a company to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = row["name"].ToString();
            DialogResult confirm = MessageBox.Show($"Delete airline \"{name}\"?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (_db.DeleteAirline(name))
                {
                    RefreshAirlines();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Company could not be deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to delete company. {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void operatosDataview_SelectionChanged(object sender, EventArgs e)
        {
            DataRow row = GetSelectedRow();
            if (row == null)
            {
                return;
            }

            namebox.Text = row["name"].ToString();
            phonebox.Text = row["phone"].ToString();
            mailbox.Text = row["email"].ToString();
        }

        private DataRow GetSelectedRow()
        {
            if (operatosDataview.SelectedRows.Count == 0)
            {
                return null;
            }

            DataRowView view = operatosDataview.SelectedRows[0].DataBoundItem as DataRowView;
            return view?.Row;
        }

        private void SelectRowByName(string name)
        {
            foreach (DataGridViewRow row in operatosDataview.Rows)
            {
                if (row.Cells["name"].Value?.ToString().Equals(name, StringComparison.OrdinalIgnoreCase) == true)
                {
                    row.Selected = true;
                    operatosDataview.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        private void ClearInputs()
        {
            namebox.Clear();
            phonebox.Clear();
            mailbox.Clear();
        }
    }
}
