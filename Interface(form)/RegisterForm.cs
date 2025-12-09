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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void registerbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(usernameTbox.Text) && !string.IsNullOrWhiteSpace(passwordTbox.Text))
                {
                    gestorBBDD gestor = new gestorBBDD();
                    gestor.Open();
                    gestor.RegisterUser(usernameTbox.Text, passwordTbox.Text);
                    gestor.Close();
                    MessageBox.Show("User registered successfully!");
                    usernameTbox.Clear();
                    passwordTbox.Clear();
                }
                else
                {
                    MessageBox.Show("Please enter both username and password.");
                }
            }
            catch (Exception AlredyRegistered)
            {
                MessageBox.Show(AlredyRegistered.Message);
            }
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
            this.Hide();
        }
    }
}
