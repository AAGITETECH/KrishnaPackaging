using GeneralCodeLibrary;
using KrishnaPackaging.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrishnaPackaging.View
{
    public partial class RewinderAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        RewinderMaster _RewinderMaster;
        RewinderDetail _RewinderDetail;

        BindingList<RewinderDetail> _listofRewinderDetail;
        BindingList<RewinderDetail> _DeleteListOfRewinderDetail = new BindingList<RewinderDetail>();

        public RewinderAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, RewinderMaster _rewindermaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Rewinder";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _RewinderMaster = _rewindermaster;
                RewinderDetail();
                FillComboBox();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Rewinder";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _RewinderMaster = _rewindermaster;
                FillComboBox();
                RewinderDetail();
                FillRecord(_RewinderMaster);
                CalculateSize();
                return base.ShowDialog();
            }
            return DialogResult.Cancel;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (msg.WParam.ToInt32() == (int)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return true;
            }
            if (msg.WParam.ToInt32() == (int)Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        void FillComboBox()
        {
            if (formMode == FormMode.Add)
            {
                CmbInwNo.SelectedIndexChanged -= new EventHandler(CmbType_SelectedIndexChanged);
                BindingList<InwardMaster> ListOfItemMaster = new BindingList<InwardMaster>(_KPDB.InwardMasters.Where(w => w.ItemMaster.ProcessType == "Re-windable" && w.IsDelete != true && w.IsRew == false && w.IsClose != true && w.CompanyId == Program.CompanyId).ToList());
                CmbInwNo.DataSource = null;
                CmbInwNo.DataSource = ListOfItemMaster;
                CmbInwNo.DisplayMember = "InwNo";
                CmbInwNo.ValueMember = "InwId";
                CmbInwNo.SelectedIndex = -1;
                CmbInwNo.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            }
            else
            {
                CmbInwNo.SelectedIndexChanged -= new EventHandler(CmbType_SelectedIndexChanged);
                BindingList<InwardMaster> ListOfItemMaster = new BindingList<InwardMaster>(_KPDB.InwardMasters.Where(w => w.ItemMaster.ProcessType == "Re-windable" && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList());
                CmbInwNo.DataSource = null;
                CmbInwNo.DataSource = ListOfItemMaster;
                CmbInwNo.DisplayMember = "InwNo";
                CmbInwNo.ValueMember = "InwId";
                CmbInwNo.SelectedIndex = -1;
                CmbInwNo.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            }

        }
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbInwNo.SelectedIndex >= 0)
            {
                txtTotalSize.Text = "";
                txtTotalwieght.Text = "";
                InwardMaster _Im = CmbInwNo.SelectedItem as InwardMaster;
                txtTotalSize.Text = Convert.ToDecimal(_Im.Size).ToString("0.00##");
                txtTotalwieght.Text = Convert.ToDecimal(_Im.Weight).ToString("0.00##");
                CalculateSize();
            }
            else
            {
                txtTotalSize.Text = "";
                txtTotalwieght.Text = "";
            }

        }
        void FillRecord(RewinderMaster _Im)
        {
            try
            {
                CmbInwNo.SelectedValue = _Im.InwId;
                CmbInwNo.Enabled = false;
                txtNoofRew.Enabled = false;
                //txtRate.Text = Convert.ToDecimal(_Im.Rate).ToString("F2"); ;
                //txtRate.Enabled = false;
                //CmbType.SelectedItem = _Im.Type.ToString();
                //CmbProcessType.SelectedItem = _Im.ProcessType.ToString();
                //CmbInwardType.SelectedItem = _Im.InwardType.ToString();
                //CmbUnit.SelectedItem = _Im.UOM.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
        }



        private bool Cansave()
        {
            if (CmbInwNo.SelectedIndex < 0)
            {
                obj.Information("Select Inward No.");
                CmbInwNo.Focus();
                return false;
            }
            else
                return true;
        }

        #region  PackingMaterial


        void RewinderDetail()
        {
            if (_RewinderMaster.RewId != 0)
            {
                _listofRewinderDetail = new BindingList<RewinderDetail>(_KPDB.RewinderDetails.Where(w => w.RewId == _RewinderMaster.RewId && w.IsDelete != true).ToList());
            }
            else
                _listofRewinderDetail = new BindingList<RewinderDetail>();
            GridRewinderdetail.DataSource = null;
            GridRewinderdetail.DataSource = _listofRewinderDetail;
            GridRewinderdetail.Columns["RewId"].Visible = false;
            GridRewinderdetail.Columns["IsUse"].Visible = false;
            GridRewinderdetail.Columns["RewDId"].Visible = false;
            GridRewinderdetail.Columns["RewinderMaster"].Visible = false;
            GridRewinderdetail.Columns["AddDate"].Visible = false;
            GridRewinderdetail.Columns["EditDate"].Visible = false;
            GridRewinderdetail.Columns["IsDelete"].Visible = false;
            GridRewinderdetail.Columns["DeleteDate"].Visible = false;
            GridRewinderdetail.Columns["Size"].DefaultCellStyle.Format = "0.00##";
            GridRewinderdetail.Columns["Weight"].DefaultCellStyle.Format = "0.00##";

            btnDelete.Enabled = false;
            CalculateTotalSizeWeight();
        }
        bool CanAddRewinderItem()
        {
            if (string.IsNullOrEmpty(txtRawNo.Text.Trim()))
            {
                obj.Information("Enter Rewinder No.");
                txtRawNo.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtSize.Text.Trim()))
            {
                obj.Information("Enter Size.");
                txtSize.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtweight.Text.Trim()))
            {
                obj.Information("Enter Weight.");
                txtweight.Focus();
                return false;
            }
            else if (_RewinderDetail == null && _listofRewinderDetail.Where(w => w.RewNo == txtRawNo.Text.Trim()).Any())
            {
                obj.Information("Rewinder No already exists..");
                txtRawNo.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }


        private void BtnPMAdd_Click(object sender, EventArgs e)
        {
            if (CanAddRewinderItem())
            {
                if (_RewinderDetail != null)
                {
                    _RewinderDetail.RewNo = txtRawNo.Text;
                    _RewinderDetail.Size = string.IsNullOrEmpty(txtSize.Text) ? 0 : Convert.ToDecimal(txtSize.Text);
                    _RewinderDetail.Weight = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDecimal(txtweight.Text);
                    _RewinderDetail.EditDate = DateTime.Now;
                }
                else
                {
                    _RewinderDetail = new RewinderDetail();
                    _RewinderDetail.RewNo = txtRawNo.Text;
                    _RewinderDetail.Size = string.IsNullOrEmpty(txtSize.Text) ? 0 : Convert.ToDecimal(txtSize.Text);
                    _RewinderDetail.Weight = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDecimal(txtweight.Text);
                    _RewinderDetail.AddDate = DateTime.Now;
                    _RewinderDetail.EditDate = null;
                    _RewinderDetail.IsUse = false;
                    _RewinderDetail.IsDelete = false;
                    _RewinderDetail.DeleteDate = null;
                    _listofRewinderDetail.Add(_RewinderDetail);
                }
                ClearReceiveNoteDetail();
                CalculateTotalSizeWeight();
            }
        }



        private void txtQty_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
            CalculateSize();
        }
        void ClearReceiveNoteDetail()
        {
            _RewinderDetail = null;
            txtRawNo.Text = "";
            txtSize.Text = "";
            txtweight.Text = "";
            btnAdd.Text = "&Add";
            btnDelete.Enabled = false;
            txtRawNo.Focus();
            CalculateSize();
        }
        private void BtnPMDelete_Click(object sender, EventArgs e)
        {
            if (_RewinderDetail != null)
            {
                if (obj.Question("Are you sure ?") == DialogResult.Yes)
                {
                    if (_RewinderDetail.RewDId != 0)
                    {
                        _DeleteListOfRewinderDetail.Add(_RewinderDetail);
                    }
                    _listofRewinderDetail.Remove(_RewinderDetail);
                    ClearReceiveNoteDetail();
                    CalculateTotalSizeWeight();
                }
            }
        }

        void CalculateAmount()
        {
            decimal Size = txtSize.Text != "" ? Decimal.Round(Convert.ToDecimal(txtSize.Text), 0) : 0;
            decimal totalwieght = txtTotalwieght.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTotalwieght.Text), 2) : 0;
            decimal totalsize = txtTotalSize.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTotalSize.Text), 2) : 0;

            decimal WieghtParsize = totalwieght / totalsize;

            decimal Weight = WieghtParsize * Size;
            txtweight.Text = Weight.ToString("0.00##");
        }
        void CalculateSize()
        {
            decimal gridSize = Convert.ToDecimal(_listofRewinderDetail != null ? _listofRewinderDetail.Sum(s => s.Size) : 0);
            decimal TotalSize = Convert.ToDecimal(txtTotalSize.Text);
            decimal Size = txtSize.Text != "" ? Decimal.Round(Convert.ToDecimal(txtSize.Text), 2) : 0;
            if (_RewinderDetail != null)
            {
                gridSize = gridSize - Convert.ToDecimal(_RewinderDetail.Size);
            }
            if ((gridSize + Size) > TotalSize)
            {
                obj.Information("Size add less than " + LabLeftSize.Text.ToString());
                txtSize.Text = "";
            }
            else
            {
                decimal Leftsize = TotalSize - gridSize;
                LabLeftSize.Text = Leftsize.ToString("0.00##");
            }

        }

        void CalculateTotalSizeWeight()
        {
            decimal Size = Convert.ToDecimal(_listofRewinderDetail != null ? _listofRewinderDetail.Sum(s => s.Size) : 0);
            LabSize.Text = Size.ToString("0.00##");
            decimal Weight = Convert.ToDecimal(_listofRewinderDetail != null ? _listofRewinderDetail.Sum(s => s.Weight) : 0);
            LabWeight.Text = Weight.ToString("0.00##");
        }

        private void BtnPMClear_Click(object sender, EventArgs e)
        {
            ClearReceiveNoteDetail();
        }

        private void GridReceiveNoteDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GridRewinderdetail.Rows.Count)
            {
                _RewinderDetail = _listofRewinderDetail[GridRewinderdetail.CurrentRow.Index];
                FillPackingMaterial(_RewinderDetail);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        void FillPackingMaterial(RewinderDetail _RND)
        {
             if (_RND != null)
            {
                txtRawNo.Text =_RND.RewNo.ToString();
                txtSize.Text = Convert.ToDecimal(_RND.Size).ToString("0.00##");
               // LabLeftSize.Text = (Convert.ToDecimal(LabLeftSize.Text) + _RND.Size).ToString();
                CalculateAmount();
                CalculateSize();

            }
        }

        #endregion


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cansave())
                {
                    _RewinderMaster.InwId = Convert.ToInt32(CmbInwNo.SelectedValue);
                    if (_DeleteListOfRewinderDetail.Count > 0)
                    {
                        foreach (RewinderDetail item in _DeleteListOfRewinderDetail)
                        {
                            item.IsDelete = true;
                            item.DeleteDate = DateTime.Now;
                        }
                    }

                    if (_listofRewinderDetail.Count > 0)
                    {
                        foreach (RewinderDetail item in _listofRewinderDetail)
                        {
                            if (item.RewDId == 0)
                            {
                                item.RewinderMaster = _RewinderMaster;
                                _KPDB.RewinderDetails.InsertOnSubmit(item);
                            }
                            else
                            {
                                item.EditDate = DateTime.Now;
                            }
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {
                        //_RewinderMaster.InwardMaster.IsRew = true;
                        InwardMaster _im = _KPDB.InwardMasters.Where(w => w.InwId == Convert.ToInt64(CmbInwNo.SelectedValue)).SingleOrDefault();
                        _im.IsRew = true;
                        _RewinderMaster.CompanyId = Program.CompanyId;
                        _RewinderMaster.AddDate = DateTime.Now;
                        _RewinderMaster.EditDate = null;
                        _RewinderMaster.IsDelete = false;
                        _RewinderMaster.DeleteDate = null;
                        _KPDB.RewinderMasters.InsertOnSubmit(_RewinderMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _RewinderMaster.EditDate = DateTime.Now;
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Information(ex.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtRawNo_Leave(object sender, EventArgs e)
        {
            if (CmbInwNo.SelectedIndex >= 0)
            {
                if (!string.IsNullOrEmpty(txtNoofRew.Text))
                {
                    _listofRewinderDetail = new BindingList<RewinderDetail>();
                    GridRewinderdetail.DataSource = null;
                    GridRewinderdetail.DataSource = _listofRewinderDetail;
                    GridRewinderdetail.Columns["RewId"].Visible = false;
                    GridRewinderdetail.Columns["IsUse"].Visible = false;
                    GridRewinderdetail.Columns["RewDId"].Visible = false;
                    GridRewinderdetail.Columns["RewinderMaster"].Visible = false;
                    GridRewinderdetail.Columns["AddDate"].Visible = false;
                    GridRewinderdetail.Columns["EditDate"].Visible = false;
                    GridRewinderdetail.Columns["IsDelete"].Visible = false;
                    GridRewinderdetail.Columns["DeleteDate"].Visible = false;
                    GridRewinderdetail.Columns["Size"].DefaultCellStyle.Format = "0.00##";
                    GridRewinderdetail.Columns["Weight"].DefaultCellStyle.Format = "0.00##";
                    int No = Convert.ToInt32(txtNoofRew.Text);
                    for (int i = 0; i < No; i++)
                    {
                        _RewinderDetail = new RewinderDetail();
                        _RewinderDetail.RewNo = CmbInwNo.Text + "-" + (i + 1).ToString();
                        decimal TotalSize = Convert.ToDecimal(txtTotalSize.Text);
                        _RewinderDetail.Size = decimal.Round(Convert.ToDecimal(TotalSize / No), 2);
                        decimal totalwieght = txtTotalwieght.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTotalwieght.Text), 2) : 0;
                        decimal totalsize = txtTotalSize.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTotalSize.Text), 2) : 0;
                        decimal WieghtParsize = totalwieght / totalsize;
                        decimal Weight = WieghtParsize * decimal.Round(Convert.ToDecimal(TotalSize / No), 2);
                        _RewinderDetail.Weight = decimal.Round(Weight, 2);
                        _RewinderDetail.AddDate = DateTime.Now;
                        _RewinderDetail.EditDate = null;
                        _RewinderDetail.IsUse = false;
                        _RewinderDetail.IsDelete = false;
                        _RewinderDetail.DeleteDate = null;
                        _listofRewinderDetail.Add(_RewinderDetail);
                    }
                    LabLeftSize.Text = "0.00";
                    CalculateTotalSizeWeight();
                }
            }
            else
            {
                _listofRewinderDetail = new BindingList<RewinderDetail>();
                GridRewinderdetail.DataSource = null;
                GridRewinderdetail.DataSource = _listofRewinderDetail;
                GridRewinderdetail.Columns["RewId"].Visible = false;
                GridRewinderdetail.Columns["IsUse"].Visible = false;
                GridRewinderdetail.Columns["RewDId"].Visible = false;
                GridRewinderdetail.Columns["RewinderMaster"].Visible = false;
                GridRewinderdetail.Columns["AddDate"].Visible = false;
                GridRewinderdetail.Columns["EditDate"].Visible = false;
                GridRewinderdetail.Columns["IsDelete"].Visible = false;
                GridRewinderdetail.Columns["DeleteDate"].Visible = false;
                GridRewinderdetail.Columns["Size"].DefaultCellStyle.Format = "0.00##";
                GridRewinderdetail.Columns["Weight"].DefaultCellStyle.Format = "0.00##";
                CalculateTotalSizeWeight();
                txtNoofRew.Text = "";
                obj.Information("Select InwardNo.");
            }
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.SelectedText = "";
            e.Handled = !obj.IsNumericTwoDecimalPlace(txt.Text, e.KeyChar);
        }
    }
}
