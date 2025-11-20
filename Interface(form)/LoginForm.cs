using DATAmanager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface_form_
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Enterbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTbox.Text) || string.IsNullOrWhiteSpace(PasswordTbox.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gestorBBDD gestor = new gestorBBDD();
            try
            {
                gestor.Open();
                string storedPassword = gestor.GetPassword(UsernameTbox.Text);

                if (storedPassword != null && storedPassword == PasswordTbox.Text)
                {
                    Main mainForm = new Main();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTbox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gestor.Close();
            }
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
            this.Hide();
        }
    }
}
