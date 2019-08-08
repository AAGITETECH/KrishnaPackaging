namespace KrishnaPackaging.View
{
    partial class MixingAdd
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.CmbItem = new System.Windows.Forms.ComboBox();
            this.CmbInward = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LabAQty = new System.Windows.Forms.Label();
            this.txtissueQty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMixingwater = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtfinisheqty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Lab = new System.Windows.Forms.Label();
            this.LabRate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.LabIssueWeight = new System.Windows.Forms.Label();
            this.LabMixingNo = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(102)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(115)))), ((int)(((byte)(77)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(115)))), ((int)(((byte)(77)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(193, 334);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 42);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(172)))), ((int)(((byte)(230)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(179)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(179)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(309, 334);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 42);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 17);
            this.label6.TabIndex = 20;
            this.label6.Text = "Item";
            // 
            // CmbItem
            // 
            this.CmbItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbItem.Location = new System.Drawing.Point(16, 79);
            this.CmbItem.Name = "CmbItem";
            this.CmbItem.Size = new System.Drawing.Size(393, 28);
            this.CmbItem.TabIndex = 0;
            this.CmbItem.SelectedIndexChanged += new System.EventHandler(this.CmbType_SelectedIndexChanged);
            // 
            // CmbInward
            // 
            this.CmbInward.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.CmbInward.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.CmbInward.Location = new System.Drawing.Point(16, 132);
            this.CmbInward.Name = "CmbInward";
            this.CmbInward.Size = new System.Drawing.Size(259, 28);
            this.CmbInward.TabIndex = 1;
            this.CmbInward.SelectedIndexChanged += new System.EventHandler(this.CmbInward_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 49;
            this.label1.Text = "Inward";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(292, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 50;
            this.label2.Text = "Available Qty";
            // 
            // LabAQty
            // 
            this.LabAQty.AutoSize = true;
            this.LabAQty.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabAQty.ForeColor = System.Drawing.Color.DarkGreen;
            this.LabAQty.Location = new System.Drawing.Point(292, 134);
            this.LabAQty.Name = "LabAQty";
            this.LabAQty.Size = new System.Drawing.Size(0, 25);
            this.LabAQty.TabIndex = 51;
            // 
            // txtissueQty
            // 
            this.txtissueQty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtissueQty.Location = new System.Drawing.Point(16, 185);
            this.txtissueQty.Name = "txtissueQty";
            this.txtissueQty.Size = new System.Drawing.Size(259, 27);
            this.txtissueQty.TabIndex = 2;
            this.txtissueQty.Leave += new System.EventHandler(this.txtissueQty_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 17);
            this.label9.TabIndex = 57;
            this.label9.Text = "Issue Qty";
            // 
            // txtMixingwater
            // 
            this.txtMixingwater.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMixingwater.Location = new System.Drawing.Point(16, 238);
            this.txtMixingwater.Name = "txtMixingwater";
            this.txtMixingwater.Size = new System.Drawing.Size(393, 27);
            this.txtMixingwater.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 17);
            this.label4.TabIndex = 59;
            this.label4.Text = "Mixing Water (Liter)";
            // 
            // txtfinisheqty
            // 
            this.txtfinisheqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfinisheqty.Location = new System.Drawing.Point(16, 291);
            this.txtfinisheqty.Name = "txtfinisheqty";
            this.txtfinisheqty.Size = new System.Drawing.Size(393, 27);
            this.txtfinisheqty.TabIndex = 4;
            this.txtfinisheqty.Leave += new System.EventHandler(this.txtfinisheqty_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 270);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 61;
            this.label3.Text = "Finished Qty (Kg)";
            // 
            // Lab
            // 
            this.Lab.AutoSize = true;
            this.Lab.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lab.Location = new System.Drawing.Point(16, 321);
            this.Lab.Name = "Lab";
            this.Lab.Size = new System.Drawing.Size(55, 17);
            this.Lab.TabIndex = 62;
            this.Lab.Text = "Rate/Kg";
            // 
            // LabRate
            // 
            this.LabRate.AutoSize = true;
            this.LabRate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabRate.Location = new System.Drawing.Point(17, 342);
            this.LabRate.Name = "LabRate";
            this.LabRate.Size = new System.Drawing.Size(0, 21);
            this.LabRate.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(292, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 17);
            this.label5.TabIndex = 64;
            this.label5.Text = "Issue Weight (Kg)";
            // 
            // LabIssueWeight
            // 
            this.LabIssueWeight.AutoSize = true;
            this.LabIssueWeight.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabIssueWeight.Location = new System.Drawing.Point(292, 188);
            this.LabIssueWeight.Name = "LabIssueWeight";
            this.LabIssueWeight.Size = new System.Drawing.Size(0, 21);
            this.LabIssueWeight.TabIndex = 65;
            // 
            // LabMixingNo
            // 
            this.LabMixingNo.AutoSize = true;
            this.LabMixingNo.Font = new System.Drawing.Font("Cambria", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabMixingNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.LabMixingNo.Location = new System.Drawing.Point(20, 25);
            this.LabMixingNo.Name = "LabMixingNo";
            this.LabMixingNo.Size = new System.Drawing.Size(29, 32);
            this.LabMixingNo.TabIndex = 66;
            this.LabMixingNo.Text = "a";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 17);
            this.label7.TabIndex = 67;
            this.label7.Text = "Mixing No";
            // 
            // MixingAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 389);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LabMixingNo);
            this.Controls.Add(this.LabIssueWeight);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LabRate);
            this.Controls.Add(this.Lab);
            this.Controls.Add(this.txtfinisheqty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMixingwater);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtissueQty);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.LabAQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CmbInward);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CmbItem);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MixingAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox CmbItem;
        private System.Windows.Forms.ComboBox CmbInward;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LabAQty;
        private System.Windows.Forms.TextBox txtissueQty;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMixingwater;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtfinisheqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Lab;
        private System.Windows.Forms.Label LabRate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label LabIssueWeight;
        private System.Windows.Forms.Label LabMixingNo;
        private System.Windows.Forms.Label label7;
    }
}