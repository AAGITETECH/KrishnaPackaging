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
    public partial class ReceiveNoteAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        ReceiveNoteMaster _ReceiveNoteMaster;
        ReceiveNoteDetail _ReceiveNoteDetail;
        BindingList<ReceiveNoteDetail> _ListOfReceiveNoteDetail;
        BindingList<ReceiveNoteDetail> _DeleteListOfReceiveNoteDetail = new BindingList<ReceiveNoteDetail>();
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");

        public ReceiveNoteAdd()
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;

        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, ReceiveNoteMaster _receivenotemaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            FillComboBox();

            if (formMode == FormMode.Add)
            {
                this.Text = "Add Receive Note";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _ReceiveNoteMaster = _receivenotemaster;
                ReceiveNoteDetail();

                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Receive Note";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _ReceiveNoteMaster = _receivenotemaster;
                FillRecord(_ReceiveNoteMaster);
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
            BindingList<PartyMaster> ListOfCategoryMaster = new BindingList<PartyMaster>(_KPDB.PartyMasters.Where(w => w.IsDelete != true && w.Type != "Sales" && w.CompanyId == Program.CompanyId).ToList());
            CmbParty.DataSource = null;
            CmbParty.DataSource = ListOfCategoryMaster;
            CmbParty.DisplayMember = "PartyName";
            CmbParty.ValueMember = "PartyId";
            CmbParty.SelectedIndex = -1;

            CmbItem.SelectedIndexChanged -= new EventHandler(CmbItem_SelectedIndexChanged);
            BindingList<ItemMaster> ListOfItemMaster = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList());
            CmbItem.DataSource = null;
            CmbItem.DataSource = ListOfItemMaster;
            CmbItem.DisplayMember = "Name";
            CmbItem.ValueMember = "IMId";
            CmbItem.SelectedIndex = -1;
            CmbItem.SelectedIndexChanged += new EventHandler(CmbItem_SelectedIndexChanged);

        }
        private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                txtRate.Text = "";
                ItemMaster _Im = CmbItem.SelectedItem as ItemMaster;
                txtRate.Text = Convert.ToDecimal(_Im.Rate).ToString("N2");
            }
            else
            {
                txtRate.Text = "";
            }
        }
        void FillRecord(ReceiveNoteMaster _RNm)
        {
            try
            {
                CmbParty.SelectedValue = _RNm.PartyId;
                txtRNNO.Text = _RNm.RNNo;
                DtpDate.Value = Convert.ToDateTime(_RNm.Date);
                ReceiveNoteDetail();

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #region  PackingMaterial

        void ReceiveNoteDetail()
        {
            if (_ReceiveNoteMaster.RNMId != 0)
            {
                _ListOfReceiveNoteDetail = new BindingList<ReceiveNoteDetail>(_KPDB.ReceiveNoteDetails.Where(w => w.RNMId == _ReceiveNoteMaster.RNMId && w.IsDelete != true).ToList());
                CalculateTotalAmount();
            }
            else
                _ListOfReceiveNoteDetail = new BindingList<ReceiveNoteDetail>();
            GridReceiveNotedetail.DataSource = null;
            GridReceiveNotedetail.DataSource = _ListOfReceiveNoteDetail;
            GridReceiveNotedetail.Columns["RNDId"].Visible = false;
            GridReceiveNotedetail.Columns["RNMId"].Visible = false;
            GridReceiveNotedetail.Columns["IMId"].Visible = false;
            GridReceiveNotedetail.Columns["ReceiveNoteMaster"].Visible = false;
            GridReceiveNotedetail.Columns["ItemMaster"].Visible = false;
            GridReceiveNotedetail.Columns["CompanyMaster"].Visible = false;
            GridReceiveNotedetail.Columns["CompanyId"].Visible = false;
            GridReceiveNotedetail.Columns["AddDate"].Visible = false;
            GridReceiveNotedetail.Columns["EditDate"].Visible = false;
            GridReceiveNotedetail.Columns["IsDelete"].Visible = false;
            GridReceiveNotedetail.Columns["DeleteDate"].Visible = false;
            GridReceiveNotedetail.Columns["InwardQty"].Visible = false;
            GridReceiveNotedetail.Columns["IsInward"].Visible = false;
            btnDelete.Enabled = false;
        }
        bool CanAddReceiveNoteItem()
        {
            if (CmbItem.SelectedIndex < 0)
            {
                obj.Information("Seletc Item.");
                CmbItem.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                obj.Information("Enter Qty.");
                txtQty.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtWeghtPackage.Text.Trim()))
            {
                obj.Information("Enter Weight/Package.");
                txtQty.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtRate.Text.Trim()))
            {
                obj.Information("Enter Rate.");
                txtRate.Focus();
                return false;
            }
            else if (_ReceiveNoteDetail == null && _ListOfReceiveNoteDetail.Where(w => w.IMId == Convert.ToInt64(CmbItem.SelectedValue)).Any())
            {
                obj.Information("Already exist Item.");
                return false;
            }
            else
            {
                return true;
            }
        }


        private void BtnPMAdd_Click(object sender, EventArgs e)
        {
            if (CanAddReceiveNoteItem())
            {
                if (_ReceiveNoteDetail != null)
                {
                    _ReceiveNoteDetail.ItemMaster = CmbItem.SelectedItem as ItemMaster;
                    _ReceiveNoteDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ReceiveNoteDetail.Weight = string.IsNullOrEmpty(txtWeghtPackage.Text) ? 0 : Convert.ToDecimal(txtWeghtPackage.Text);
                    _ReceiveNoteDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    _ReceiveNoteDetail.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
                    _ReceiveNoteDetail.NetRate = string.IsNullOrEmpty(txtNetRate.Text) ? 0 : Convert.ToDecimal(txtNetRate.Text);
                    _ReceiveNoteDetail.CGST = string.IsNullOrEmpty(txtICgst.Text) ? 0 : Convert.ToDecimal(txtICgst.Text);
                    _ReceiveNoteDetail.SGST = string.IsNullOrEmpty(txtISgst.Text) ? 0 : Convert.ToDecimal(txtISgst.Text);
                    _ReceiveNoteDetail.IGST = string.IsNullOrEmpty(txtIIgst.Text) ? 0 : Convert.ToDecimal(txtIIgst.Text);
                    _ReceiveNoteDetail.OtherCharges = string.IsNullOrEmpty(txtOtherCharges.Text) ? 0 : Convert.ToDecimal(txtOtherCharges.Text);
                    _ReceiveNoteDetail.Amount = Convert.ToDecimal(txtITotalAmount.Text);
                    _ReceiveNoteDetail.EditDate = DateTime.Now;
                }
                else
                {
                    _ReceiveNoteDetail = new ReceiveNoteDetail();
                    _ReceiveNoteDetail.ItemMaster = CmbItem.SelectedItem as ItemMaster;
                    _ReceiveNoteDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ReceiveNoteDetail.Weight = string.IsNullOrEmpty(txtWeghtPackage.Text) ? 0 : Convert.ToDecimal(txtWeghtPackage.Text);
                    _ReceiveNoteDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    _ReceiveNoteDetail.Discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);
                    _ReceiveNoteDetail.NetRate = string.IsNullOrEmpty(txtNetRate.Text) ? 0 : Convert.ToDecimal(txtNetRate.Text);
                    _ReceiveNoteDetail.CGST = string.IsNullOrEmpty(txtICgst.Text) ? 0 : Convert.ToDecimal(txtICgst.Text);
                    _ReceiveNoteDetail.SGST = string.IsNullOrEmpty(txtISgst.Text) ? 0 : Convert.ToDecimal(txtISgst.Text);
                    _ReceiveNoteDetail.IGST = string.IsNullOrEmpty(txtIIgst.Text) ? 0 : Convert.ToDecimal(txtIIgst.Text);
                    _ReceiveNoteDetail.OtherCharges = string.IsNullOrEmpty(txtOtherCharges.Text) ? 0 : Convert.ToDecimal(txtOtherCharges.Text);
                    _ReceiveNoteDetail.Amount = Convert.ToDecimal(txtITotalAmount.Text);
                    _ReceiveNoteDetail.CompanyId = Program.CompanyId;
                    _ReceiveNoteDetail.AddDate = DateTime.Now;
                    _ReceiveNoteDetail.EditDate = null;
                    _ReceiveNoteDetail.InwardQty = 0;
                    _ReceiveNoteDetail.IsInward = false;
                    _ReceiveNoteDetail.IsDelete = false;
                    _ReceiveNoteDetail.DeleteDate = null;
                    _ListOfReceiveNoteDetail.Add(_ReceiveNoteDetail);
                }
                ClearReceiveNoteDetail();
                CalculateTotalAmount();
            }
        }

        void CalculateAmount()
        {
            decimal Qty = txtWeghtPackage.Text != "" ? Decimal.Round(Convert.ToDecimal(txtWeghtPackage.Text), 0) : 0;
            decimal Rate = txtRate.Text != "" ? Decimal.Round(Convert.ToDecimal(txtRate.Text), 2) : 0;
            decimal Discount = txtDiscount.Text != "" ? Decimal.Round(Convert.ToDecimal(txtDiscount.Text), 2) : 0;
            decimal CGST = txtICgst.Text != "" ? Decimal.Round(Convert.ToDecimal(txtICgst.Text), 2) : 0;
            decimal SGST = txtISgst.Text != "" ? Decimal.Round(Convert.ToDecimal(txtISgst.Text), 2) : 0;
            decimal IGST = txtIIgst.Text != "" ? Decimal.Round(Convert.ToDecimal(txtIIgst.Text), 2) : 0;
            decimal OTerCharge = txtOtherCharges.Text != "" ? Decimal.Round(Convert.ToDecimal(txtOtherCharges.Text), 2) : 0;
            decimal DisAmt = Rate * (Discount / 100);
            decimal NetRate = Rate - DisAmt;
            txtNetRate.Text = NetRate.ToString("N2");
            decimal SubTAmt = NetRate * Qty;
            txtISubTotal.Text = SubTAmt.ToString("N2");
            decimal CGSTAmt = SubTAmt * (CGST / 100);
            decimal SGSTAmt = SubTAmt * (SGST / 100);
            decimal IGSTAmt = SubTAmt * (IGST / 100);
            decimal Amount = SubTAmt + CGSTAmt + SGSTAmt + IGSTAmt + OTerCharge;
            txtITotalAmount.Text = Amount.ToString("N2");
        }

        void CalculateTotalAmount()
        {
            decimal TotalAmount = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => (s.NetRate * s.Weight)) : 0);
            txtSubTotal.Text = TotalAmount.ToString("0.00##");
            decimal TotalDiscount = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => s.Rate * (s.Discount / 100)) : 0);
            //txtTotalDis.Text = TotalDiscount.ToString("N2");
            decimal Amt = TotalAmount;
            decimal CGST = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => (s.NetRate * s.Weight) * (s.CGST / 100)) : 0);
            decimal SGST = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => (s.NetRate * s.Weight) * (s.SGST / 100)) : 0);
            decimal IGST = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => (s.NetRate * s.Weight) * (s.IGST / 100)) : 0);
            decimal Oth = Convert.ToDecimal(_ListOfReceiveNoteDetail != null ? _ListOfReceiveNoteDetail.Sum(s => s.OtherCharges) : 0);
            decimal FinalAmount = Amt + CGST + SGST + IGST + Oth;
            txtCGST.Text = CGST.ToString("0.00##");
            txtSGST.Text = SGST.ToString("0.00##");
            txtIGST.Text = IGST.ToString("0.00##");
            txtTotalAmount.Text = FinalAmount.ToString("0.00##");
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        void ClearReceiveNoteDetail()
        {
            _ReceiveNoteDetail = null;
            CmbItem.SelectedIndex = -1;
            txtQty.Text = "";
            txtRate.Text = "";
            txtWeghtPackage.Text = "";
            txtDiscount.Text = "";
            txtNetRate.Text = "";
            txtISubTotal.Text = "";
            txtICgst.Text = "";
            txtISgst.Text = "";
            txtIIgst.Text = "";
            txtOtherCharges.Text = "";
            txtITotalAmount.Text = "";
            btnAdd.Text = "&Add";
            btnDelete.Enabled = false;
            CmbItem.Focus();
        }
        private void BtnPMDelete_Click(object sender, EventArgs e)
        {
            if (_ReceiveNoteDetail != null)
            {
                if (obj.Question("Are you sure ?") == DialogResult.Yes)
                {
                    if (_ReceiveNoteDetail.RNDId != 0)
                    {
                        _DeleteListOfReceiveNoteDetail.Add(_ReceiveNoteDetail);
                    }
                    _ListOfReceiveNoteDetail.Remove(_ReceiveNoteDetail);
                    ClearReceiveNoteDetail();
                }
            }
        }

        private void BtnPMClear_Click(object sender, EventArgs e)
        {
            ClearReceiveNoteDetail();
        }

        private void GridReceiveNoteDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GridReceiveNotedetail.Rows.Count)
            {
                _ReceiveNoteDetail = _ListOfReceiveNoteDetail[GridReceiveNotedetail.CurrentRow.Index];
                FillPackingMaterial(_ReceiveNoteDetail);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
        }

        void FillPackingMaterial(ReceiveNoteDetail _RND)
        {
            if (_RND != null)
            {
                CmbItem.SelectedValue = _RND.IMId;
                txtQty.Text = Convert.ToDecimal(_RND.Qty).ToString("N2");
                txtRate.Text = Convert.ToDecimal(_RND.Rate).ToString("N2");
                txtWeghtPackage.Text = Convert.ToDecimal(_RND.Weight).ToString("N2");
                txtDiscount.Text = _RND.Discount.ToString();
                txtNetRate.Text = _RND.NetRate.ToString();
                txtICgst.Text = _RND.CGST.ToString();
                txtISgst.Text = _RND.SGST.ToString();
                txtIIgst.Text = _RND.IGST.ToString();
                txtOtherCharges.Text = _RND.OtherCharges.ToString();
                txtITotalAmount.Text = _RND.Amount.ToString();
                CalculateAmount();
            }
        }

        #endregion

        private bool Cansave()
        {
            if (CmbParty.SelectedIndex < 0)
            {
                obj.Information("Select Party");
                CmbParty.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtRNNO.Text))
            {
                obj.Information("Select ReceiveNote No.");
                txtRNNO.Focus();
                return false;
            }
            else if (_ListOfReceiveNoteDetail.Count == 0)
            {
                obj.Information("Add one Receive Note Item.");
                return false;
            }
            else
                return true;
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.SelectedText = "";
            e.Handled = !obj.IsNumericTwoDecimalPlace(txt.Text, e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cansave())
                {
                    _ReceiveNoteMaster.PartyId = Convert.ToInt32(CmbParty.SelectedValue);
                    _ReceiveNoteMaster.RNNo = txtRNNO.Text;
                    _ReceiveNoteMaster.Date = DtpDate.Value;
                    _ReceiveNoteMaster.Amount = Convert.ToDecimal(txtTotalAmount.Text);

                    if (_DeleteListOfReceiveNoteDetail.Count > 0)
                    {
                        foreach (ReceiveNoteDetail item in _DeleteListOfReceiveNoteDetail)
                        {
                            ItemMaster Pd = item.ItemMaster;
                            Pd.Qty = Pd.Qty - item.Qty;
                            item.IsDelete = true;
                            item.DeleteDate = DateTime.Now;
                        }
                    }

                    if (_ListOfReceiveNoteDetail.Count > 0)
                    {
                        foreach (ReceiveNoteDetail item in _ListOfReceiveNoteDetail)
                        {
                            if (item.RNDId == 0)
                            {
                                ItemMaster Pd = item.ItemMaster;
                                decimal oldrateTotal = Convert.ToDecimal(Pd.Rate) * Convert.ToDecimal(Pd.Qty);
                                decimal NewrateTotal = Convert.ToDecimal(item.Rate) * Convert.ToDecimal(item.Qty);
                                decimal Total = oldrateTotal + NewrateTotal;
                                Pd.Qty = Pd.Qty + item.Qty;
                                Pd.Rate = Total / Convert.ToDecimal(Pd.Qty);
                                item.ReceiveNoteMaster = _ReceiveNoteMaster;
                                item.AddDate = DateTime.Now;
                                item.EditDate = null;
                                item.IsDelete = false;
                                item.DeleteDate = null;
                                _KPDB.ReceiveNoteDetails.InsertOnSubmit(item);
                            }
                            else
                            {
                                KrishnaPackagingDbDataContext _KPDB1 = new KrishnaPackagingDbDataContext();
                                ReceiveNoteDetail Sd = _KPDB1.ReceiveNoteDetails.Where(w => w.RNDId == item.RNDId && w.IsDelete != true).SingleOrDefault();
                                ItemMaster Pd = item.ItemMaster;
                                decimal oldrateTotal = Convert.ToDecimal(Pd.Rate) * Convert.ToDecimal(Pd.Qty - Sd.Qty);
                                decimal NewrateTotal = Convert.ToDecimal(item.Rate) * Convert.ToDecimal(item.Qty);
                                decimal Total = oldrateTotal + NewrateTotal;
                                Pd.Qty = Pd.Qty - Sd.Qty + item.Qty;
                                if (Pd.Qty > 0) 
                                {
                                    Pd.Rate = Total / Convert.ToDecimal(Pd.Qty);
                                }
                                item.EditDate = DateTime.Now;
                            }
                        }
                    }

                    if (this.formMode == FormMode.Add)
                    {

                        _ReceiveNoteMaster.CompanyId = Program.CompanyId;
                        _ReceiveNoteMaster.AddDate = DateTime.Now;
                        _ReceiveNoteMaster.EditDate = null;
                        _ReceiveNoteMaster.IsDelete = false;
                        _ReceiveNoteMaster.DeleteDate = null;
                        _KPDB.ReceiveNoteMasters.InsertOnSubmit(_ReceiveNoteMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _ReceiveNoteMaster.EditDate = DateTime.Now;
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

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtICgst_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtISgst_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtIIgst_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNetRate_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
