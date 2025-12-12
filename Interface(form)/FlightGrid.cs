using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using FlightLib;
using DATAmanager;

namespace Interface_form_
{
    public partial class FlightGrid : Form
    {
        private readonly FlightPlanList flightplans;
        private readonly gestorBBDD _db = new gestorBBDD();

        public FlightGrid(FlightPlanList _flightPlans)
        {
            InitializeComponent();
            flightplans = _flightPlans;

            Finfo.CellClick += Finfo_CellClick;
            this.Load += FlightGrid_Load;
            this.FormClosed += FlightGrid_FormClosed;
        }

        private void FlightGrid_Load(object sender, EventArgs e)
        {
            try
            {
                _db.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible conectar con la base de datos de compañías.\n" + ex.Message,
                                "Error BBDD",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            Finfo.Columns.Clear();
            Finfo.Rows.Clear();
            Finfo.ColumnCount = 6;
            Finfo.Columns[0].Name = "ID";
            Finfo.Columns[1].Name = "Posición Actual";
            Finfo.Columns[2].Name = "Velocidad";
            Finfo.Columns[3].Name = "Compañía";
            Finfo.Columns[4].Name = "Teléfono";
            Finfo.Columns[5].Name = "Email";

            int numFlights = flightplans.getnum();
            for (int i = 0; i < numFlights; i++)
            {
                FlightPlan plan = flightplans.GetFlightPlan(i);
                if (plan == null) continue;

                string id = plan.GetId();
                Position pos = plan.GetCurrentPosition();
                string posStr = "(" + pos.GetX().ToString("F2") + ", " + pos.GetY().ToString("F2") + ")";
                double velocidad = plan.GetVelocidad();

                string companyName = plan.GetcompanyName();
                string phone = string.Empty;
                string email = string.Empty;

                if (!string.IsNullOrWhiteSpace(companyName))
                {
                    DataRow companyRow = _db.GetAirline(companyName);
                    if (companyRow != null)
                    {
                        companyName = companyRow["name"].ToString();
                        phone = companyRow["phone"].ToString();
                        email = companyRow["email"].ToString();
                    }
                }

                Finfo.Rows.Add(id, posStr, velocidad, companyName, phone, email);
            }

            Finfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Finfo.ColumnHeadersVisible = true;
            Finfo.RowHeadersVisible = false;
            Finfo.MultiSelect = false;
            Finfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            distancebox.Multiline = true;
            distancebox.ScrollBars = ScrollBars.Vertical;
            distancebox.Text = "Clica un vuelo para ver distancias.";
        }

        private void FlightGrid_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.Close();
        }

        private void Finfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            MostrarDistanciasDesde(e.RowIndex);
        }

        private void MostrarDistanciasDesde(int indice)
        {
            FlightPlan basePlan = flightplans.GetFlightPlan(indice);
            if (basePlan == null)
            {
                distancebox.Text = "Índice inválido.";
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Distancias desde vuelo " + basePlan.GetId() + ":");

            int total = flightplans.getnum();
            for (int i = 0; i < total; i++)
            {
                if (i == indice) continue;

                FlightPlan otro = flightplans.GetFlightPlan(i);
                if (otro == null) continue;

                double d = basePlan.Distance(otro);
                sb.Append("- ").Append(otro.GetId()).Append(": ").Append(d.ToString("F2")).AppendLine();
            }

            distancebox.Text = sb.ToString();
        }
    }
}

