using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using FlightLib;

namespace Interface_form_
{
    public partial class FlightGrid : Form
    {
        private readonly FlightPlanList flightplans;

        public FlightGrid(FlightPlanList _flightPlans)
        {
            InitializeComponent();
            flightplans = _flightPlans;

         

            // Nuevo: manejar clic en una fila
            Finfo.CellClick += Finfo_CellClick;
        }

        private void FlightGrid_Load(object sender, EventArgs e)
        {
            Finfo.Columns.Clear();
            Finfo.Rows.Clear();
            Finfo.ColumnCount = 3;
            Finfo.Columns[0].Name = "ID";
            Finfo.Columns[1].Name = "Posición Actual";
            Finfo.Columns[2].Name = "Velocidad";

            int numFlights = flightplans.getnum();
            int i = 0;
            while (i < numFlights)
            {
                FlightPlan plan = flightplans.GetFlightPlan(i);
                string id = plan.GetId();
                Position pos = plan.GetCurrentPosition();
                string posStr = "(" + pos.GetX().ToString("F2") + ", " + pos.GetY().ToString("F2") + ")";
                double velocidad = plan.GetVelocidad();
                Finfo.Rows.Add(id, posStr, velocidad);
                i++;
            }

            Finfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Finfo.ColumnHeadersVisible = true;
            Finfo.RowHeadersVisible = false;
            Finfo.MultiSelect = false;
            Finfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Preparar el cuadro de distancias para mostrar varias líneas
            distancebox.Multiline = true;
            distancebox.ScrollBars = ScrollBars.Vertical;
            distancebox.Text = "Clica un vuelo para ver distancias.";
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
            int i = 0;
            while (i < total)
            {
                if (i != indice)
                {
                    FlightPlan otro = flightplans.GetFlightPlan(i);
                    if (otro != null)
                    {
                        double d = basePlan.Distance(otro);
                        sb.Append("- ").Append(otro.GetId()).Append(": ").Append(d.ToString("F2")).AppendLine();
                    }
                }
                i++;
            }

            distancebox.Text = sb.ToString();
        }
    }
}

