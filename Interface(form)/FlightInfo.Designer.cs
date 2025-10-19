namespace Interface_form_
{
    partial class FlightInfo
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
            this.xbox = new System.Windows.Forms.TextBox();
            this.ybox = new System.Windows.Forms.TextBox();
            this.speedbox = new System.Windows.Forms.TextBox();
            this.Idbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.speedlbl = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // xbox
            // 
            this.xbox.Location = new System.Drawing.Point(43, 134);
            this.xbox.Name = "xbox";
            this.xbox.Size = new System.Drawing.Size(90, 29);
            this.xbox.TabIndex = 0;
            // 
            // ybox
            // 
            this.ybox.Location = new System.Drawing.Point(154, 134);
            this.ybox.Name = "ybox";
            this.ybox.Size = new System.Drawing.Size(90, 29);
            this.ybox.TabIndex = 1;
            // 
            // speedbox
            // 
            this.speedbox.Location = new System.Drawing.Point(43, 218);
            this.speedbox.Name = "speedbox";
            this.speedbox.Size = new System.Drawing.Size(90, 29);
            this.speedbox.TabIndex = 2;
            // 
            // Idbox
            // 
            this.Idbox.Location = new System.Drawing.Point(43, 53);
            this.Idbox.Name = "Idbox";
            this.Idbox.Size = new System.Drawing.Size(90, 29);
            this.Idbox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Position";
            // 
            // speedlbl
            // 
            this.speedlbl.AutoSize = true;
            this.speedlbl.Location = new System.Drawing.Point(50, 190);
            this.speedlbl.Name = "speedlbl";
            this.speedlbl.Size = new System.Drawing.Size(70, 25);
            this.speedlbl.TabIndex = 6;
            this.speedlbl.Text = "Speed";
            // 
            // closebtn
            // 
            this.closebtn.Location = new System.Drawing.Point(169, 218);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(104, 39);
            this.closebtn.TabIndex = 7;
            this.closebtn.Text = "Close";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // FlightInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 312);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.speedlbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Idbox);
            this.Controls.Add(this.speedbox);
            this.Controls.Add(this.ybox);
            this.Controls.Add(this.xbox);
            this.Name = "FlightInfo";
            this.Text = "FlightInfo";
            this.Load += new System.EventHandler(this.FlightInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xbox;
        private System.Windows.Forms.TextBox ybox;
        private System.Windows.Forms.TextBox speedbox;
        private System.Windows.Forms.TextBox Idbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label speedlbl;
        private System.Windows.Forms.Button closebtn;
    }
}