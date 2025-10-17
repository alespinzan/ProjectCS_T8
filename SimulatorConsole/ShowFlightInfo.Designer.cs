namespace SimulatorConsole
{
    partial class ShowFlightInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Xbox = new System.Windows.Forms.TextBox();
            this.YBox = new System.Windows.Forms.TextBox();
            this.SpeedBox = new System.Windows.Forms.TextBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(178, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Position";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Speed";
            // 
            // Xbox
            // 
            this.Xbox.Location = new System.Drawing.Point(274, 133);
            this.Xbox.Name = "Xbox";
            this.Xbox.Size = new System.Drawing.Size(67, 20);
            this.Xbox.TabIndex = 2;
            // 
            // YBox
            // 
            this.YBox.Location = new System.Drawing.Point(356, 133);
            this.YBox.Name = "YBox";
            this.YBox.Size = new System.Drawing.Size(68, 20);
            this.YBox.TabIndex = 3;
            // 
            // SpeedBox
            // 
            this.SpeedBox.Location = new System.Drawing.Point(274, 165);
            this.SpeedBox.Name = "SpeedBox";
            this.SpeedBox.Size = new System.Drawing.Size(100, 20);
            this.SpeedBox.TabIndex = 4;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(274, 222);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(130, 36);
            this.CloseButton.TabIndex = 5;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 360);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SpeedBox);
            this.Controls.Add(this.YBox);
            this.Controls.Add(this.Xbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Xbox;
        private System.Windows.Forms.TextBox YBox;
        private System.Windows.Forms.TextBox SpeedBox;
        private System.Windows.Forms.Button CloseButton;
    }
}