namespace Interface_form_
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flightPlansToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.securitySettingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationAirspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OperatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.slbl = new System.Windows.Forms.Label();
            this.clbl = new System.Windows.Forms.Label();
            this.sclbl = new System.Windows.Forms.Label();
            this.simlbl = new System.Windows.Forms.Label();
            this.cyclelbl = new System.Windows.Forms.Label();
            this.securitylbl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.simulationAirspaceToolStripMenuItem,
            this.OperatorsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(540, 26);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flightPlansToolStripMenuItem1,
            this.securitySettingsToolStripMenuItem1});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(102, 24);
            this.optionsToolStripMenuItem.Text = "Sim options";
            // 
            // flightPlansToolStripMenuItem1
            // 
            this.flightPlansToolStripMenuItem1.Name = "flightPlansToolStripMenuItem1";
            this.flightPlansToolStripMenuItem1.Size = new System.Drawing.Size(201, 26);
            this.flightPlansToolStripMenuItem1.Text = "FlightPlans";
            this.flightPlansToolStripMenuItem1.Click += new System.EventHandler(this.flightPlansToolStripMenuItem1_Click);
            // 
            // securitySettingsToolStripMenuItem1
            // 
            this.securitySettingsToolStripMenuItem1.Name = "securitySettingsToolStripMenuItem1";
            this.securitySettingsToolStripMenuItem1.Size = new System.Drawing.Size(201, 26);
            this.securitySettingsToolStripMenuItem1.Text = "Security Settings";
            this.securitySettingsToolStripMenuItem1.Click += new System.EventHandler(this.securitySettingsToolStripMenuItem1_Click);
            // 
            // simulationAirspaceToolStripMenuItem
            // 
            this.simulationAirspaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem});
            this.simulationAirspaceToolStripMenuItem.Name = "simulationAirspaceToolStripMenuItem";
            this.simulationAirspaceToolStripMenuItem.Size = new System.Drawing.Size(155, 24);
            this.simulationAirspaceToolStripMenuItem.Text = "Simulation Airspace";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.showToolStripMenuItem.Text = "Open current";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // OperatorsToolStripMenuItem
            // 
            this.OperatorsToolStripMenuItem.Name = "OperatorsToolStripMenuItem";
            this.OperatorsToolStripMenuItem.Size = new System.Drawing.Size(89, 24);
            this.OperatorsToolStripMenuItem.Text = "Operators";
            this.OperatorsToolStripMenuItem.Click += new System.EventHandler(this.OperatorsToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // slbl
            // 
            this.slbl.AutoSize = true;
            this.slbl.Location = new System.Drawing.Point(6, 26);
            this.slbl.Name = "slbl";
            this.slbl.Size = new System.Drawing.Size(112, 16);
            this.slbl.TabIndex = 1;
            this.slbl.Text = "Simulation Name:";
            // 
            // clbl
            // 
            this.clbl.AutoSize = true;
            this.clbl.Location = new System.Drawing.Point(6, 53);
            this.clbl.Name = "clbl";
            this.clbl.Size = new System.Drawing.Size(72, 16);
            this.clbl.TabIndex = 2;
            this.clbl.Text = "Cycle time:";
            // 
            // sclbl
            // 
            this.sclbl.AutoSize = true;
            this.sclbl.Location = new System.Drawing.Point(6, 83);
            this.sclbl.Name = "sclbl";
            this.sclbl.Size = new System.Drawing.Size(112, 16);
            this.sclbl.TabIndex = 3;
            this.sclbl.Text = "Security distance:";
            // 
            // simlbl
            // 
            this.simlbl.AutoSize = true;
            this.simlbl.Location = new System.Drawing.Point(124, 26);
            this.simlbl.Name = "simlbl";
            this.simlbl.Size = new System.Drawing.Size(11, 16);
            this.simlbl.TabIndex = 5;
            this.simlbl.Text = "-";
            // 
            // cyclelbl
            // 
            this.cyclelbl.AutoSize = true;
            this.cyclelbl.Location = new System.Drawing.Point(84, 53);
            this.cyclelbl.Name = "cyclelbl";
            this.cyclelbl.Size = new System.Drawing.Size(11, 16);
            this.cyclelbl.TabIndex = 6;
            this.cyclelbl.Text = "-";
            // 
            // securitylbl
            // 
            this.securitylbl.AutoSize = true;
            this.securitylbl.Location = new System.Drawing.Point(124, 83);
            this.securitylbl.Name = "securitylbl";
            this.securitylbl.Size = new System.Drawing.Size(11, 16);
            this.securitylbl.TabIndex = 7;
            this.securitylbl.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.slbl);
            this.groupBox1.Controls.Add(this.securitylbl);
            this.groupBox1.Controls.Add(this.clbl);
            this.groupBox1.Controls.Add(this.simlbl);
            this.groupBox1.Controls.Add(this.cyclelbl);
            this.groupBox1.Controls.Add(this.sclbl);
            this.groupBox1.Location = new System.Drawing.Point(121, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 120);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(540, 313);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.Text = "Main";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flightPlansToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem securitySettingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem simulationAirspaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label slbl;
        private System.Windows.Forms.Label clbl;
        private System.Windows.Forms.Label sclbl;
        private System.Windows.Forms.ToolStripMenuItem OperatorsToolStripMenuItem;
        private System.Windows.Forms.Label simlbl;
        private System.Windows.Forms.Label cyclelbl;
        private System.Windows.Forms.Label securitylbl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

