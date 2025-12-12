using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;
using DATAmanager;

namespace Interface_form_
{
    public partial class FlightInfo : Form
    {
        private readonly gestorBBDD _db = new gestorBBDD();
        FlightPlan currentFP;
        public FlightInfo()
        {
            InitializeComponent();
        }

        public void setFlight(FlightPlan f)
        {
            this.currentFP = f;
        }

        private void FlightInfo_Load(object sender, EventArgs e)
        {
            if (currentFP == null) return;

            Position pos = currentFP.GetCurrentPosition();
            double x = pos.GetX();
            double y = pos.GetY();
            xbox.Text = x.ToString("F2");
            ybox.Text = y.ToString("F2");
            Idbox.Text = currentFP.GetId();
            speedbox.Text = currentFP.GetVelocidad().ToString("F2");

            operatorBox.Clear();
            phoneBox.Clear();
            mailbox.Clear();

            string companyName = currentFP.GetcompanyName();
            if (string.IsNullOrWhiteSpace(companyName))
            {
                operatorBox.Text = "Sin operador";
                return;
            }

            try
            {
                _db.Open();
                DataRow company = _db.GetAirline(companyName);
                if (company != null)
                {
                    operatorBox.Text = company["name"].ToString();
                    phoneBox.Text = company["phone"].ToString();
                    mailbox.Text = company["email"].ToString();
                }
                else
                {
                    operatorBox.Text = companyName;
                    phoneBox.Text = "N/D";
                    mailbox.Text = "N/D";
                }
            }
            catch
            {
                operatorBox.Text = companyName;
                phoneBox.Text = "Error";
                mailbox.Text = "Error";
            }
            finally
            {
                _db.Close();
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
