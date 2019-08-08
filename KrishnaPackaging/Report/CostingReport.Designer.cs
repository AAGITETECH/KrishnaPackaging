namespace KrishnaPackaging.Report
{
    partial class CostingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CostingReport));
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lblPartyName = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.CmbPM = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbReport = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.PanleCL = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.FromItemDTP = new System.Windows.Forms.DateTimePicker();
            this.ToItemDTP = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.PanleCL.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(0, 73);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1136, 589);
            this.reportViewer.TabIndex = 3;
            // 
            // lblPartyName
            // 
            this.lblPartyName.AutoSize = true;
            this.lblPartyName.Location = new System.Drawing.Point(279, 8);
            this.lblPartyName.Name = "lblPartyName";
            this.lblPartyName.Size = new System.Drawing.Size(85, 17);
            this.lblPartyName.TabIndex = 4;
            this.lblPartyName.Text = "Select &LotNo.";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(555, 20);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(140, 39);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&Search";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // CmbPM
            // 
            this.CmbPM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbPM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbPM.DropDownHeight = 300;
            this.CmbPM.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPM.FormattingEnabled = true;
            this.CmbPM.IntegralHeight = false;
            this.CmbPM.Location = new System.Drawing.Point(279, 30);
            this.CmbPM.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CmbPM.Name = "CmbPM";
            this.CmbPM.Size = new System.Drawing.Size(257, 28);
            this.CmbPM.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select &Machine";
            // 
            // CmbReport
            // 
            this.CmbReport.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbReport.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbReport.DropDownHeight = 300;
            this.CmbReport.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbReport.FormattingEnabled = true;
            this.CmbReport.IntegralHeight = false;
            this.CmbReport.Items.AddRange(new object[] {
            "Cone Macking",
            "Core Pipe",
            "Edge Protector"});
            this.CmbReport.Location = new System.Drawing.Point(22, 30);
            this.CmbReport.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CmbReport.Name = "CmbReport";
            this.CmbReport.Size = new System.Drawing.Size(238, 28);
            this.CmbReport.TabIndex = 0;
            this.CmbReport.SelectedIndexChanged += new System.EventHandler(this.CmbReport_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PanleCL);
            this.panel1.Controls.Add(this.CmbReport);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CmbPM);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.lblPartyName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 73);
            this.panel1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(279, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Select &LotNo.";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(893, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Search";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.DropDownHeight = 300;
            this.comboBox2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.IntegralHeight = false;
            this.comboBox2.Location = new System.Drawing.Point(279, 30);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(257, 28);
            this.comboBox2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Select &Machine";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.DropDownHeight = 300;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "Cone Macking",
            "Core Pipe",
            "Edge Protector"});
            this.comboBox1.Location = new System.Drawing.Point(22, 30);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(238, 28);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // PanleCL
            // 
            this.PanleCL.Controls.Add(this.label4);
            this.PanleCL.Controls.Add(this.label5);
            this.PanleCL.Controls.Add(this.FromItemDTP);
            this.PanleCL.Controls.Add(this.ToItemDTP);
            this.PanleCL.Controls.Add(this.comboBox1);
            this.PanleCL.Controls.Add(this.label2);
            this.PanleCL.Controls.Add(this.comboBox2);
            this.PanleCL.Controls.Add(this.button1);
            this.PanleCL.Controls.Add(this.label3);
            this.PanleCL.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanleCL.Location = new System.Drawing.Point(0, 0);
            this.PanleCL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PanleCL.Name = "PanleCL";
            this.PanleCL.Size = new System.Drawing.Size(1136, 73);
            this.PanleCL.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(549, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "&From Date :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(719, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "&To Date :";
            // 
            // FromItemDTP
            // 
            this.FromItemDTP.CustomFormat = "dd/MM/yyyy";
            this.FromItemDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromItemDTP.Location = new System.Drawing.Point(549, 30);
            this.FromItemDTP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FromItemDTP.Name = "FromItemDTP";
            this.FromItemDTP.Size = new System.Drawing.Size(164, 25);
            this.FromItemDTP.TabIndex = 13;
            // 
            // ToItemDTP
            // 
            this.ToItemDTP.CustomFormat = "dd/MM/yyyy";
            this.ToItemDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToItemDTP.Location = new System.Drawing.Point(719, 30);
            this.ToItemDTP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ToItemDTP.Name = "ToItemDTP";
            this.ToItemDTP.Size = new System.Drawing.Size(159, 25);
            this.ToItemDTP.TabIndex = 14;
            // 
            // CostingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 662);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CostingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanleCL.ResumeLayout(false);
            this.PanleCL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Label lblPartyName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox CmbPM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanleCL;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker FromItemDTP;
        private System.Windows.Forms.DateTimePicker ToItemDTP;
    }
}