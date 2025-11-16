namespace Interface_form_
{
    partial class SimulationForm
    {

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

       
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cyclebtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.restartbtn = new System.Windows.Forms.Button();
            this.stopbtn = new System.Windows.Forms.Button();
            this.startbtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.infobtn = new System.Windows.Forms.Button();
            this.conflictbtn = new System.Windows.Forms.Button();
            this.editspeedsbtn = new System.Windows.Forms.Button();
            this.returnbtn = new System.Windows.Forms.Button();
            this.saveExitBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(56, 47);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 600);
            this.panel1.TabIndex = 0;
            // 
            // cyclebtn
            // 
            this.cyclebtn.Location = new System.Drawing.Point(985, 47);
            this.cyclebtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cyclebtn.Name = "cyclebtn";
            this.cyclebtn.Size = new System.Drawing.Size(113, 37);
            this.cyclebtn.TabIndex = 1;
            this.cyclebtn.Text = "Cycle";
            this.cyclebtn.UseVisualStyleBackColor = true;
            this.cyclebtn.Click += new System.EventHandler(this.cyclebtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.restartbtn);
            this.groupBox1.Controls.Add(this.stopbtn);
            this.groupBox1.Controls.Add(this.startbtn);
            this.groupBox1.Location = new System.Drawing.Point(985, 111);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(113, 194);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AutoSim";
            // 
            // restartbtn
            // 
            this.restartbtn.Location = new System.Drawing.Point(12, 138);
            this.restartbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restartbtn.Name = "restartbtn";
            this.restartbtn.Size = new System.Drawing.Size(85, 34);
            this.restartbtn.TabIndex = 2;
            this.restartbtn.Text = "Restart";
            this.restartbtn.UseVisualStyleBackColor = true;
            this.restartbtn.Click += new System.EventHandler(this.restartbtn_Click);
            // 
            // stopbtn
            // 
            this.stopbtn.Location = new System.Drawing.Point(12, 81);
            this.stopbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stopbtn.Name = "stopbtn";
            this.stopbtn.Size = new System.Drawing.Size(85, 34);
            this.stopbtn.TabIndex = 1;
            this.stopbtn.Text = "Stop";
            this.stopbtn.UseVisualStyleBackColor = true;
            this.stopbtn.Click += new System.EventHandler(this.stopbtn_Click);
            // 
            // startbtn
            // 
            this.startbtn.Location = new System.Drawing.Point(12, 28);
            this.startbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.startbtn.Name = "startbtn";
            this.startbtn.Size = new System.Drawing.Size(85, 34);
            this.startbtn.TabIndex = 0;
            this.startbtn.Text = "Start";
            this.startbtn.UseVisualStyleBackColor = true;
            this.startbtn.Click += new System.EventHandler(this.startbtn_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // infobtn
            // 
            this.infobtn.Location = new System.Drawing.Point(985, 367);
            this.infobtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.infobtn.Name = "infobtn";
            this.infobtn.Size = new System.Drawing.Size(113, 37);
            this.infobtn.TabIndex = 3;
            this.infobtn.Text = "Space Info";
            this.infobtn.UseVisualStyleBackColor = true;
            this.infobtn.Click += new System.EventHandler(this.infobtn_Click);
            // 
            // conflictbtn
            // 
            this.conflictbtn.Location = new System.Drawing.Point(985, 425);
            this.conflictbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conflictbtn.Name = "conflictbtn";
            this.conflictbtn.Size = new System.Drawing.Size(113, 37);
            this.conflictbtn.TabIndex = 4;
            this.conflictbtn.Text = "Check Conflict";
            this.conflictbtn.UseVisualStyleBackColor = true;
            this.conflictbtn.Click += new System.EventHandler(this.conflictbtn_Click);
            // 
            // editspeedsbtn
            // 
            this.editspeedsbtn.Location = new System.Drawing.Point(985, 548);
            this.editspeedsbtn.Margin = new System.Windows.Forms.Padding(4);
            this.editspeedsbtn.Name = "editspeedsbtn";
            this.editspeedsbtn.Size = new System.Drawing.Size(104, 31);
            this.editspeedsbtn.TabIndex = 5;
            this.editspeedsbtn.Text = "Edit Speeds";
            this.editspeedsbtn.UseVisualStyleBackColor = true;
            this.editspeedsbtn.Click += new System.EventHandler(this.editspeedsbtn_Click);
            // 
            // returnbtn
            // 
            this.returnbtn.Location = new System.Drawing.Point(985, 490);
            this.returnbtn.Margin = new System.Windows.Forms.Padding(4);
            this.returnbtn.Name = "returnbtn";
            this.returnbtn.Size = new System.Drawing.Size(105, 33);
            this.returnbtn.TabIndex = 6;
            this.returnbtn.Text = "Return";
            this.returnbtn.UseVisualStyleBackColor = true;
            this.returnbtn.Click += new System.EventHandler(this.returnbtn_Click);
            // 
            // saveExitBtn
            // 
            this.saveExitBtn.Location = new System.Drawing.Point(985, 594);
            this.saveExitBtn.Margin = new System.Windows.Forms.Padding(4);
            this.saveExitBtn.Name = "saveExitBtn";
            this.saveExitBtn.Size = new System.Drawing.Size(113, 37);
            this.saveExitBtn.TabIndex = 7;
            this.saveExitBtn.Text = "Guardar y salir";
            this.saveExitBtn.UseVisualStyleBackColor = true;
            this.saveExitBtn.Click += new System.EventHandler(this.saveExitBtn_Click);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1271, 710);
            this.Controls.Add(this.saveExitBtn);
            this.Controls.Add(this.returnbtn);
            this.Controls.Add(this.editspeedsbtn);
            this.Controls.Add(this.conflictbtn);
            this.Controls.Add(this.infobtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cyclebtn);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cyclebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button stopbtn;
        private System.Windows.Forms.Button startbtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button infobtn;
        private System.Windows.Forms.Button conflictbtn;
        private System.Windows.Forms.Button restartbtn;
        private System.Windows.Forms.Button editspeedsbtn;
        private System.Windows.Forms.Button returnbtn;
        private System.Windows.Forms.Button saveExitBtn;
    }
}