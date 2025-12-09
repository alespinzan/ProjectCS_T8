namespace Interface_form_
{
    partial class FlightPlanForm
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
            this.FlightBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.velocity1box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.destination1box = new System.Windows.Forms.TextBox();
            this.origin1box = new System.Windows.Forms.TextBox();
            this.id1box = new System.Windows.Forms.TextBox();
            this.acceptButton = new System.Windows.Forms.Button();
            this.nonConflictbtn = new System.Windows.Forms.Button();
            this.conflictbtn = new System.Windows.Forms.Button();
            this.Test_Plans = new System.Windows.Forms.Label();
            this.resetbtn = new System.Windows.Forms.Button();
            this.browcebtn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.flightPlansDataGridView = new System.Windows.Forms.DataGridView();
            this.addbtn = new System.Windows.Forms.Button();
            this.undobtn = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            this.FlightBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flightPlansDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // FlightBox1
            // 
            this.FlightBox1.Controls.Add(this.label7);
            this.FlightBox1.Controls.Add(this.velocity1box);
            this.FlightBox1.Controls.Add(this.label3);
            this.FlightBox1.Controls.Add(this.label2);
            this.FlightBox1.Controls.Add(this.label1);
            this.FlightBox1.Controls.Add(this.destination1box);
            this.FlightBox1.Controls.Add(this.origin1box);
            this.FlightBox1.Controls.Add(this.id1box);
            this.FlightBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FlightBox1.Location = new System.Drawing.Point(43, 441);
            this.FlightBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FlightBox1.Name = "FlightBox1";
            this.FlightBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FlightBox1.Size = new System.Drawing.Size(433, 130);
            this.FlightBox1.TabIndex = 0;
            this.FlightBox1.TabStop = false;
            this.FlightBox1.Text = "Insert_FlightPlan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label7.Location = new System.Drawing.Point(325, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Velocity";
            // 
            // velocity1box
            // 
            this.velocity1box.Location = new System.Drawing.Point(327, 62);
            this.velocity1box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.velocity1box.Name = "velocity1box";
            this.velocity1box.Size = new System.Drawing.Size(73, 26);
            this.velocity1box.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label3.Location = new System.Drawing.Point(220, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Destination";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label2.Location = new System.Drawing.Point(128, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Origin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label1.Location = new System.Drawing.Point(25, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID";
            // 
            // destination1box
            // 
            this.destination1box.Location = new System.Drawing.Point(221, 62);
            this.destination1box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.destination1box.Name = "destination1box";
            this.destination1box.Size = new System.Drawing.Size(73, 26);
            this.destination1box.TabIndex = 2;
            // 
            // origin1box
            // 
            this.origin1box.Location = new System.Drawing.Point(123, 62);
            this.origin1box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.origin1box.Name = "origin1box";
            this.origin1box.Size = new System.Drawing.Size(73, 26);
            this.origin1box.TabIndex = 1;
            // 
            // id1box
            // 
            this.id1box.Location = new System.Drawing.Point(21, 62);
            this.id1box.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.id1box.Name = "id1box";
            this.id1box.Size = new System.Drawing.Size(73, 26);
            this.id1box.TabIndex = 0;
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(646, 517);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(93, 33);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // nonConflictbtn
            // 
            this.nonConflictbtn.Location = new System.Drawing.Point(643, 80);
            this.nonConflictbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.nonConflictbtn.Name = "nonConflictbtn";
            this.nonConflictbtn.Size = new System.Drawing.Size(93, 34);
            this.nonConflictbtn.TabIndex = 4;
            this.nonConflictbtn.Text = "NonConflict";
            this.nonConflictbtn.UseVisualStyleBackColor = true;
            this.nonConflictbtn.Click += new System.EventHandler(this.nonConflictbtn_Click);
            // 
            // conflictbtn
            // 
            this.conflictbtn.Location = new System.Drawing.Point(645, 131);
            this.conflictbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.conflictbtn.Name = "conflictbtn";
            this.conflictbtn.Size = new System.Drawing.Size(93, 34);
            this.conflictbtn.TabIndex = 5;
            this.conflictbtn.Text = "Conflict";
            this.conflictbtn.UseVisualStyleBackColor = true;
            this.conflictbtn.Click += new System.EventHandler(this.conflictbtn_Click);
            // 
            // Test_Plans
            // 
            this.Test_Plans.AutoSize = true;
            this.Test_Plans.Location = new System.Drawing.Point(641, 53);
            this.Test_Plans.Name = "Test_Plans";
            this.Test_Plans.Size = new System.Drawing.Size(70, 16);
            this.Test_Plans.TabIndex = 6;
            this.Test_Plans.Text = "Test plans";
            // 
            // resetbtn
            // 
            this.resetbtn.Location = new System.Drawing.Point(43, 15);
            this.resetbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetbtn.Name = "resetbtn";
            this.resetbtn.Size = new System.Drawing.Size(93, 33);
            this.resetbtn.TabIndex = 7;
            this.resetbtn.Text = "Reset";
            this.resetbtn.UseVisualStyleBackColor = true;
            this.resetbtn.Click += new System.EventHandler(this.resetbtn_Click);
            // 
            // browcebtn
            // 
            this.browcebtn.Location = new System.Drawing.Point(646, 205);
            this.browcebtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.browcebtn.Name = "browcebtn";
            this.browcebtn.Size = new System.Drawing.Size(93, 34);
            this.browcebtn.TabIndex = 8;
            this.browcebtn.Text = "Browse";
            this.browcebtn.UseVisualStyleBackColor = true;
            this.browcebtn.Click += new System.EventHandler(this.browcebtn_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(642, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Load from file";
            // 
            // flightPlansDataGridView
            // 
            this.flightPlansDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.flightPlansDataGridView.Location = new System.Drawing.Point(43, 53);
            this.flightPlansDataGridView.Name = "flightPlansDataGridView";
            this.flightPlansDataGridView.RowHeadersWidth = 51;
            this.flightPlansDataGridView.RowTemplate.Height = 24;
            this.flightPlansDataGridView.Size = new System.Drawing.Size(534, 373);
            this.flightPlansDataGridView.TabIndex = 10;
            // 
            // addbtn
            // 
            this.addbtn.Location = new System.Drawing.Point(484, 441);
            this.addbtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(93, 33);
            this.addbtn.TabIndex = 11;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // undobtn
            // 
            this.undobtn.Location = new System.Drawing.Point(484, 480);
            this.undobtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.undobtn.Name = "undobtn";
            this.undobtn.Size = new System.Drawing.Size(93, 33);
            this.undobtn.TabIndex = 12;
            this.undobtn.Text = "Undo";
            this.undobtn.UseVisualStyleBackColor = true;
            this.undobtn.Click += new System.EventHandler(this.undobtn_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Location = new System.Drawing.Point(484, 517);
            this.deletebtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(93, 33);
            this.deletebtn.TabIndex = 13;
            this.deletebtn.Text = "Delete";
            this.deletebtn.UseVisualStyleBackColor = true;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // FlightPlanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 605);
            this.Controls.Add(this.deletebtn);
            this.Controls.Add(this.undobtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.flightPlansDataGridView);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.browcebtn);
            this.Controls.Add(this.resetbtn);
            this.Controls.Add(this.Test_Plans);
            this.Controls.Add(this.conflictbtn);
            this.Controls.Add(this.nonConflictbtn);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.FlightBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FlightPlanForm";
            this.Text = "FlightplanForm";
            this.FlightBox1.ResumeLayout(false);
            this.FlightBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.flightPlansDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox FlightBox1;
        private System.Windows.Forms.TextBox id1box;
        private System.Windows.Forms.TextBox destination1box;
        private System.Windows.Forms.TextBox origin1box;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox velocity1box;
        private System.Windows.Forms.Button nonConflictbtn;
        private System.Windows.Forms.Button conflictbtn;
        private System.Windows.Forms.Label Test_Plans;
        private System.Windows.Forms.Button resetbtn;
        private System.Windows.Forms.Button browcebtn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView flightPlansDataGridView;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.Button undobtn;
        private System.Windows.Forms.Button deletebtn;
    }
}