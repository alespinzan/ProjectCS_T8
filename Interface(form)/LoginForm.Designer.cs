namespace Interface_form_
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.usernamelbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.UsernameTbox = new System.Windows.Forms.TextBox();
            this.PasswordTbox = new System.Windows.Forms.TextBox();
            this.Enterbtn = new System.Windows.Forms.Button();
            this.backbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // usernamelbl
            // 
            this.usernamelbl.AutoSize = true;
            this.usernamelbl.Location = new System.Drawing.Point(212, 161);
            this.usernamelbl.Name = "usernamelbl";
            this.usernamelbl.Size = new System.Drawing.Size(102, 25);
            this.usernamelbl.TabIndex = 2;
            this.usernamelbl.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(401, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // UsernameTbox
            // 
            this.UsernameTbox.Location = new System.Drawing.Point(217, 218);
            this.UsernameTbox.Name = "UsernameTbox";
            this.UsernameTbox.Size = new System.Drawing.Size(119, 29);
            this.UsernameTbox.TabIndex = 4;
            // 
            // PasswordTbox
            // 
            this.PasswordTbox.Location = new System.Drawing.Point(406, 218);
            this.PasswordTbox.Name = "PasswordTbox";
            this.PasswordTbox.Size = new System.Drawing.Size(119, 29);
            this.PasswordTbox.TabIndex = 5;
            // 
            // Enterbtn
            // 
            this.Enterbtn.Location = new System.Drawing.Point(319, 298);
            this.Enterbtn.Name = "Enterbtn";
            this.Enterbtn.Size = new System.Drawing.Size(107, 53);
            this.Enterbtn.TabIndex = 6;
            this.Enterbtn.Text = "Enter";
            this.Enterbtn.UseVisualStyleBackColor = true;
            this.Enterbtn.Click += new System.EventHandler(this.Enterbtn_Click);
            // 
            // backbtn
            // 
            this.backbtn.Location = new System.Drawing.Point(33, 492);
            this.backbtn.Name = "backbtn";
            this.backbtn.Size = new System.Drawing.Size(107, 53);
            this.backbtn.TabIndex = 7;
            this.backbtn.Text = "Back";
            this.backbtn.UseVisualStyleBackColor = true;
            this.backbtn.Click += new System.EventHandler(this.backbtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 568);
            this.Controls.Add(this.backbtn);
            this.Controls.Add(this.Enterbtn);
            this.Controls.Add(this.PasswordTbox);
            this.Controls.Add(this.UsernameTbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usernamelbl);
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernamelbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox UsernameTbox;
        private System.Windows.Forms.TextBox PasswordTbox;
        private System.Windows.Forms.Button Enterbtn;
        private System.Windows.Forms.Button backbtn;
    }
}