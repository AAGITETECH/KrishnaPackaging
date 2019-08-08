namespace KrishnaPackaging.View
{
    partial class PartyView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartyView));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageEditDelete = new System.Windows.Forms.TabPage();
            this.GrinEditDeleteDetailView = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSAdd = new System.Windows.Forms.ToolStripButton();
            this.TSEdit = new System.Windows.Forms.ToolStripButton();
            this.TSDelete = new System.Windows.Forms.ToolStripButton();
            this.TSCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.TSTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl.SuspendLayout();
            this.tabPageEditDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrinEditDeleteDetailView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageEditDelete);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 26);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(905, 479);
            this.tabControl.TabIndex = 1;
            // 
            // tabPageEditDelete
            // 
            this.tabPageEditDelete.Controls.Add(this.GrinEditDeleteDetailView);
            this.tabPageEditDelete.Location = new System.Drawing.Point(4, 26);
            this.tabPageEditDelete.Name = "tabPageEditDelete";
            this.tabPageEditDelete.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEditDelete.Size = new System.Drawing.Size(897, 449);
            this.tabPageEditDelete.TabIndex = 1;
            this.tabPageEditDelete.Text = "Party Details";
            this.tabPageEditDelete.UseVisualStyleBackColor = true;
            // 
            // GrinEditDeleteDetailView
            // 
            this.GrinEditDeleteDetailView.AllowUserToAddRows = false;
            this.GrinEditDeleteDetailView.AllowUserToDeleteRows = false;
            this.GrinEditDeleteDetailView.AllowUserToResizeColumns = false;
            this.GrinEditDeleteDetailView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.GrinEditDeleteDetailView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.GrinEditDeleteDetailView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GrinEditDeleteDetailView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GrinEditDeleteDetailView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GrinEditDeleteDetailView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrinEditDeleteDetailView.Location = new System.Drawing.Point(3, 3);
            this.GrinEditDeleteDetailView.MultiSelect = false;
            this.GrinEditDeleteDetailView.Name = "GrinEditDeleteDetailView";
            this.GrinEditDeleteDetailView.ReadOnly = true;
            this.GrinEditDeleteDetailView.RowHeadersVisible = false;
            this.GrinEditDeleteDetailView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GrinEditDeleteDetailView.RowTemplate.Height = 25;
            this.GrinEditDeleteDetailView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GrinEditDeleteDetailView.Size = new System.Drawing.Size(891, 443);
            this.GrinEditDeleteDetailView.TabIndex = 0;
            this.GrinEditDeleteDetailView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrinEditDeleteDetailView_CellClick);
            this.GrinEditDeleteDetailView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GrinEditDeleteDetailView_CellDoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSAdd,
            this.TSEdit,
            this.TSDelete,
            this.TSCancel,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.TSTextBoxSearch,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(905, 26);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // TSAdd
            // 
            this.TSAdd.Image = ((System.Drawing.Image)(resources.GetObject("TSAdd.Image")));
            this.TSAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSAdd.Name = "TSAdd";
            this.TSAdd.Size = new System.Drawing.Size(54, 23);
            this.TSAdd.Text = "&Add";
            this.TSAdd.Click += new System.EventHandler(this.TSAdd_Click);
            // 
            // TSEdit
            // 
            this.TSEdit.Image = ((System.Drawing.Image)(resources.GetObject("TSEdit.Image")));
            this.TSEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSEdit.Name = "TSEdit";
            this.TSEdit.Size = new System.Drawing.Size(52, 23);
            this.TSEdit.Text = "&Edit";
            this.TSEdit.Click += new System.EventHandler(this.TSEdit_Click);
            // 
            // TSDelete
            // 
            this.TSDelete.Image = ((System.Drawing.Image)(resources.GetObject("TSDelete.Image")));
            this.TSDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSDelete.Name = "TSDelete";
            this.TSDelete.Size = new System.Drawing.Size(68, 23);
            this.TSDelete.Text = "&Delete";
            this.TSDelete.Click += new System.EventHandler(this.TSDelete_Click);
            // 
            // TSCancel
            // 
            this.TSCancel.Image = ((System.Drawing.Image)(resources.GetObject("TSCancel.Image")));
            this.TSCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSCancel.Name = "TSCancel";
            this.TSCancel.Size = new System.Drawing.Size(69, 23);
            this.TSCancel.Text = "&Cancel";
            this.TSCancel.Click += new System.EventHandler(this.TSCancel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(56, 23);
            this.toolStripLabel1.Text = "&Search :";
            // 
            // TSTextBoxSearch
            // 
            this.TSTextBoxSearch.Name = "TSTextBoxSearch";
            this.TSTextBoxSearch.Size = new System.Drawing.Size(291, 26);
            this.TSTextBoxSearch.ToolTipText = "Search by PartyName and BillingName";
            this.TSTextBoxSearch.TextChanged += new System.EventHandler(this.TSTextBoxSearch_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // PartyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 505);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PartyView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PartyView";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PartyView_KeyDown);
            this.tabControl.ResumeLayout(false);
            this.tabPageEditDelete.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GrinEditDeleteDetailView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageEditDelete;
        private System.Windows.Forms.DataGridView GrinEditDeleteDetailView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSAdd;
        private System.Windows.Forms.ToolStripButton TSEdit;
        private System.Windows.Forms.ToolStripButton TSDelete;
        private System.Windows.Forms.ToolStripButton TSCancel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox TSTextBoxSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}