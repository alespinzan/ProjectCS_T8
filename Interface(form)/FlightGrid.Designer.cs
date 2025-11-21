namespace Interface_form_
{
    partial class FlightGrid
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
            this.Finfo = new System.Windows.Forms.DataGridView();
            this.distancebox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Finfo)).BeginInit();
            this.SuspendLayout();
            // 
            // Finfo
            // 
            this.Finfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Finfo.Location = new System.Drawing.Point(26, 23);
            this.Finfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Finfo.Name = "Finfo";
            this.Finfo.RowHeadersWidth = 72;
            this.Finfo.RowTemplate.Height = 31;
            this.Finfo.Size = new System.Drawing.Size(418, 299);
            this.Finfo.TabIndex = 0;
            // 
            // distancebox
            // 
            this.distancebox.Location = new System.Drawing.Point(475, 56);
            this.distancebox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.distancebox.Multiline = true;
            this.distancebox.Name = "distancebox";
            this.distancebox.Size = new System.Drawing.Size(145, 197);
            this.distancebox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Distance selected";
            // 
            // FlightGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.distancebox);
            this.Controls.Add(this.Finfo);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FlightGrid";
            this.Text = "FlightGrid";
            this.Load += new System.EventHandler(this.FlightGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Finfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Finfo;
        private System.Windows.Forms.TextBox distancebox;
        private System.Windows.Forms.Label label1;
    }
}