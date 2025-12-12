namespace Interface_form_
{
    partial class OperatorsF
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
            this.operatosDataview = new System.Windows.Forms.DataGridView();
            this.usrersbox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mailbox = new System.Windows.Forms.TextBox();
            this.phonebox = new System.Windows.Forms.TextBox();
            this.namebox = new System.Windows.Forms.TextBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.operatosDataview)).BeginInit();
            this.usrersbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // operatosDataview
            // 
            this.operatosDataview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operatosDataview.Location = new System.Drawing.Point(12, 12);
            this.operatosDataview.Name = "operatosDataview";
            this.operatosDataview.RowHeadersWidth = 51;
            this.operatosDataview.RowTemplate.Height = 24;
            this.operatosDataview.Size = new System.Drawing.Size(491, 419);
            this.operatosDataview.TabIndex = 0;
            // 
            // usrersbox
            // 
            this.usrersbox.Controls.Add(this.label3);
            this.usrersbox.Controls.Add(this.label2);
            this.usrersbox.Controls.Add(this.label1);
            this.usrersbox.Controls.Add(this.mailbox);
            this.usrersbox.Controls.Add(this.phonebox);
            this.usrersbox.Controls.Add(this.namebox);
            this.usrersbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.usrersbox.Location = new System.Drawing.Point(509, 12);
            this.usrersbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usrersbox.Name = "usrersbox";
            this.usrersbox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usrersbox.Size = new System.Drawing.Size(215, 284);
            this.usrersbox.TabIndex = 1;
            this.usrersbox.TabStop = false;
            this.usrersbox.Text = "Insert_Operator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.Location = new System.Drawing.Point(25, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "eMail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(25, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Phone";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(25, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name";
            // 
            // mailbox
            // 
            this.mailbox.Location = new System.Drawing.Point(28, 211);
            this.mailbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.mailbox.Name = "mailbox";
            this.mailbox.Size = new System.Drawing.Size(153, 26);
            this.mailbox.TabIndex = 2;
            // 
            // phonebox
            // 
            this.phonebox.Location = new System.Drawing.Point(28, 134);
            this.phonebox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.phonebox.Name = "phonebox";
            this.phonebox.Size = new System.Drawing.Size(153, 26);
            this.phonebox.TabIndex = 1;
            // 
            // namebox
            // 
            this.namebox.Location = new System.Drawing.Point(28, 62);
            this.namebox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.namebox.Name = "namebox";
            this.namebox.Size = new System.Drawing.Size(153, 26);
            this.namebox.TabIndex = 0;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(571, 398);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(93, 33);
            this.acceptButton.TabIndex = 3;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(571, 351);
            this.deletebtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(93, 33);
            this.deletebtn.TabIndex = 14;
            this.deletebtn.Text = "Delete";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // UsersSetupF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 481);
            this.Controls.Add(this.deletebtn);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.usrersbox);
            this.Controls.Add(this.operatosDataview);
            this.Name = "UsersSetupF";
            this.Text = "UsersSetupF";
            ((System.ComponentModel.ISupportInitialize)(this.operatosDataview)).EndInit();
            this.usrersbox.ResumeLayout(false);
            this.usrersbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView operatosDataview;
        private System.Windows.Forms.GroupBox usrersbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mailbox;
        private System.Windows.Forms.TextBox phonebox;
        private System.Windows.Forms.TextBox namebox;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button deletebtn;
    }
}