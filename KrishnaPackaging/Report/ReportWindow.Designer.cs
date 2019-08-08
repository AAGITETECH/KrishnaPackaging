namespace KrishnaPackaging.Report
{
    partial class ReportWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportWindow));
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPartyName = new System.Windows.Forms.Label();
            this.CmbPM = new System.Windows.Forms.ComboBox();
            this.lblfromdate = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.BtnOk2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbItem = new System.Windows.Forms.ComboBox();
            this.ToItemDTP = new System.Windows.Forms.DateTimePicker();
            this.FromItemDTP = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            this.reportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer.Location = new System.Drawing.Point(0, 93);
            this.reportViewer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1136, 569);
            this.reportViewer.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1136, 93);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.lblPartyName);
            this.panel3.Controls.Add(this.CmbPM);
            this.panel3.Controls.Add(this.lblfromdate);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dtpFromDate);
            this.panel3.Controls.Add(this.dtpToDate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1136, 90);
            this.panel3.TabIndex = 7;
            // 
            // lblPartyName
            // 
            this.lblPartyName.AutoSize = true;
            this.lblPartyName.Location = new System.Drawing.Point(20, 12);
            this.lblPartyName.Name = "lblPartyName";
            this.lblPartyName.Size = new System.Drawing.Size(82, 17);
            this.lblPartyName.TabIndex = 4;
            this.lblPartyName.Text = "Select &Party :";
            // 
            // CmbPM
            // 
            this.CmbPM.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbPM.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbPM.DropDownHeight = 300;
            this.CmbPM.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPM.FormattingEnabled = true;
            this.CmbPM.IntegralHeight = false;
            this.CmbPM.Location = new System.Drawing.Point(20, 34);
            this.CmbPM.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CmbPM.Name = "CmbPM";
            this.CmbPM.Size = new System.Drawing.Size(257, 28);
            this.CmbPM.TabIndex = 1;
            // 
            // lblfromdate
            // 
            this.lblfromdate.AutoSize = true;
            this.lblfromdate.Location = new System.Drawing.Point(301, 12);
            this.lblfromdate.Name = "lblfromdate";
            this.lblfromdate.Size = new System.Drawing.Size(76, 17);
            this.lblfromdate.TabIndex = 5;
            this.lblfromdate.Text = "&From Date :";
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
            this.btnOk.Location = new System.Drawing.Point(650, 23);
            this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(140, 39);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "&Search";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(471, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "&To Date :";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(301, 34);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(164, 25);
            this.dtpFromDate.TabIndex = 2;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(471, 34);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(159, 25);
            this.dtpToDate.TabIndex = 3;
            // 
            // BtnOk2
            // 
            this.BtnOk2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnOk2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnOk2.FlatAppearance.BorderSize = 0;
            this.BtnOk2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnOk2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BtnOk2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOk2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOk2.ForeColor = System.Drawing.Color.White;
            this.BtnOk2.Location = new System.Drawing.Point(616, 20);
            this.BtnOk2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnOk2.Name = "BtnOk2";
            this.BtnOk2.Size = new System.Drawing.Size(140, 39);
            this.BtnOk2.TabIndex = 4;
            this.BtnOk2.Text = "&Search";
            this.BtnOk2.UseVisualStyleBackColor = false;
            this.BtnOk2.Click += new System.EventHandler(this.BtnOk2_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Select &Item";
            // 
            // CmbItem
            // 
            this.CmbItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbItem.DropDownHeight = 300;
            this.CmbItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbItem.FormattingEnabled = true;
            this.CmbItem.IntegralHeight = false;
            this.CmbItem.Location = new System.Drawing.Point(22, 30);
            this.CmbItem.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.CmbItem.Name = "CmbItem";
            this.CmbItem.Size = new System.Drawing.Size(238, 28);
            this.CmbItem.TabIndex = 0;
            // 
            // ToItemDTP
            // 
            this.ToItemDTP.CustomFormat = "dd/MM/yyyy";
            this.ToItemDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToItemDTP.Location = new System.Drawing.Point(439, 30);
            this.ToItemDTP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ToItemDTP.Name = "ToItemDTP";
            this.ToItemDTP.Size = new System.Drawing.Size(159, 25);
            this.ToItemDTP.TabIndex = 10;
            // 
            // FromItemDTP
            // 
            this.FromItemDTP.CustomFormat = "dd/MM/yyyy";
            this.FromItemDTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromItemDTP.Location = new System.Drawing.Point(269, 30);
            this.FromItemDTP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FromItemDTP.Name = "FromItemDTP";
            this.FromItemDTP.Size = new System.Drawing.Size(164, 25);
            this.FromItemDTP.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "&To Date :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "&From Date :";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.FromItemDTP);
            this.panel2.Controls.Add(this.ToItemDTP);
            this.panel2.Controls.Add(this.CmbItem);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.BtnOk2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1136, 89);
            this.panel2.TabIndex = 9;
            // 
            // ReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 662);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblPartyName;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblfromdate;
        private System.Windows.Forms.ComboBox CmbPM;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker FromItemDTP;
        private System.Windows.Forms.DateTimePicker ToItemDTP;
        private System.Windows.Forms.ComboBox CmbItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnOk2;
    }
}