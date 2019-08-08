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
    public partial class ConsumptionAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        ConsumptionMaster _ConsumptionMaster;
        BindingList<ConsumptionDetail> _listConsumptionDetail;
        BindingList<ConsumptionDetail> _DeletelistConsumptionDetail = new BindingList<ConsumptionDetail>();
        ConsumptionDetail _ConsumptionDetail;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");

        public ConsumptionAdd()
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, ConsumptionMaster _consumptionmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            FillComboBox();
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Consumption";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _ConsumptionMaster = _consumptionmaster;
                FillConsumptionDetail();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Consumption";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _ConsumptionMaster = _consumptionmaster;
                FillRecord(_ConsumptionMaster);
                FillConsumptionDetail();
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
            CmbItem.SelectedIndexChanged -= new EventHandler(CmbItem_SelectedIndexChanged);
            BindingList<ItemMaster> ListOfItemMaster = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList());
            CmbItem.DataSource = null;
            CmbItem.DataSource = ListOfItemMaster;
            CmbItem.DisplayMember = "Name";
            CmbItem.ValueMember = "IMId";
            CmbItem.SelectedIndex = -1;
            CmbItem.SelectedIndexChanged += new EventHandler(CmbItem_SelectedIndexChanged);

        }
        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbMachine.SelectedIndex >= 0)
            {
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.IsClose != true && w.Machine == CmbMachine.SelectedItem.ToString() && w.CompanyId == Program.CompanyId).ToList());
                CmbLotno.DataSource = null;
                CmbLotno.DataSource = ListOfCategoryMaster;
                CmbLotno.DisplayMember = "LotNo";
                CmbLotno.ValueMember = "MachinId";
                CmbLotno.SelectedIndex = -1;
            }
        }
        private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                txtRate.Text = "";
                LabAQty.Text = "";
                txtQty.Text = "";
                CmbRewMix.Enabled = true;
                ItemMaster _Im = CmbItem.SelectedItem as ItemMaster;
                if (_Im.ProcessType == "Re-windable")
                {
                    CmbRewMix.SelectedIndexChanged -= new EventHandler(CmbRewMix_SelectedIndexChanged);
                    //BindingList<RewinderDetail> ListOfCategoryMaster = new BindingList<RewinderDetail>(_KPDB.RewinderDetails.Where(w => w.IsDelete != true && w.RewinderMaster.InwardMaster.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.RewinderMaster.CompanyId == Program.CompanyId && w.IsUse != true).ToList());
                    BindingList<InwardMaster> ListOfInwardMaster = new BindingList<InwardMaster>(_KPDB.InwardMasters.Where(w => w.IsDelete != true && w.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.CompanyId == Program.CompanyId && w.RewinderMasters.Where(R => R.RewinderDetails.Where(RD => RD.IsUse == false && RD.IsDelete == false).Any() && R.IsDelete == false).AsEnumerable().Count() > 0).ToList());
                    CmbRewMix.DataSource = null;
                    CmbRewMix.DataSource = ListOfInwardMaster;
                    CmbRewMix.DisplayMember = "InwNo";
                    CmbRewMix.ValueMember = "InwId";
                    CmbRewMix.SelectedIndex = -1;
                    CmbRewMix.SelectedIndexChanged += new EventHandler(CmbRewMix_SelectedIndexChanged);
                }
                else if (_Im.ProcessType == "Mixable")
                {
                    CmbRewMix.SelectedIndexChanged -= new EventHandler(CmbRewMix_SelectedIndexChanged);
                    BindingList<MixingMaster> ListOfCMBData = new BindingList<MixingMaster>(_KPDB.MixingMasters.Where(w => w.IsDelete != true && w.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.CompanyId == Program.CompanyId && w.FinisheQty > w.IssueWeight).ToList());
                    CmbRewMix.DataSource = null;
                    CmbRewMix.DataSource = ListOfCMBData;
                    CmbRewMix.DisplayMember = "MixingNo";
                    CmbRewMix.ValueMember = "MMId";
                    CmbRewMix.SelectedIndex = -1;
                    CmbRewMix.SelectedIndexChanged += new EventHandler(CmbRewMix_SelectedIndexChanged);
                }
                else
                {
                    CmbRewMix.Enabled = false;
                    txtRate.Text = Convert.ToDecimal(_Im.Rate).ToString("0.00##");
                    LabAQty.Text = Convert.ToDecimal(_Im.Qty).ToString("0.00##");
                    txtQty.Text = "";
                    txtQty.Enabled = true;

                }
            }
            else
            {
                CmbRewMix.Enabled = true;
                txtRate.Text = "";
                LabAQty.Text = "";
                txtQty.Text = "";
            }
        }
        private void CmbRewMix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbRewMix.SelectedIndex >= 0)
            {
                txtRate.Text = "";
                LabAQty.Text = "";
                txtQty.Text = "";
                ItemMaster _Im = CmbItem.SelectedItem as ItemMaster;

                if (_Im.ProcessType == "Re-windable")
                {
                    //_listConsumptionDetail = null;

                    //_listConsumptionDetail = new BindingList<ConsumptionDetail>();
                    if (_ConsumptionDetail == null)
                    {

                        InwardMaster _RD = CmbRewMix.SelectedItem as InwardMaster;
                        BindingList<RewinderDetail> ListOfCategoryMaster = new BindingList<RewinderDetail>(_KPDB.RewinderDetails.Where(w => w.IsDelete != true && w.RewinderMaster.InwardMaster.InwId == Convert.ToInt64(_RD.InwId) && w.RewinderMaster.CompanyId == Program.CompanyId && w.IsUse != true).ToList());
                        foreach (RewinderDetail item in ListOfCategoryMaster)
                        {
                            _ConsumptionDetail = new ConsumptionDetail();
                            _ConsumptionDetail.ItemMaster = _RD.ItemMaster;
                            _ConsumptionDetail.RewinderDetail = item;
                            _ConsumptionDetail.RDId = item.RewDId;
                            _ConsumptionDetail.MMId = null;

                            _ConsumptionDetail.Qty = decimal.Round(Convert.ToDecimal(item.Weight), 2);
                            var Rate = item.RewinderMaster.InwardMaster.ReceiveNoteMaster.ReceiveNoteDetails.Where(w => w.IMId == _RD.IMId && w.IsDelete != true).SingleOrDefault();
                            _ConsumptionDetail.Rate = decimal.Round(Convert.ToDecimal(Rate.Rate), 2);
                            _ConsumptionDetail.AddDate = DateTime.Now;
                            _ConsumptionDetail.EditDate = null;
                            _ConsumptionDetail.IsDelete = false;
                            _ConsumptionDetail.DeleteDate = null;
                            _listConsumptionDetail.Remove(_listConsumptionDetail.Where(w => w.RDId == Convert.ToInt64(item.RewDId)).FirstOrDefault());
                            _listConsumptionDetail.Add(_ConsumptionDetail);
                        }
                        txtQty.Enabled = false;
                        ClearConsumptionDetail();
                    }


                }
                else if (_Im.ProcessType == "Mixable")
                {
                    MixingMaster _MM = CmbRewMix.SelectedItem as MixingMaster;
                    txtRate.Text = Convert.ToDecimal(_MM.Rate).ToString("0.00##");
                    decimal Aqty = Convert.ToDecimal(_MM.FinisheQty) - Convert.ToDecimal(_MM.IssueWeight);
                    LabAQty.Text = Aqty.ToString("0.00##");
                    txtQty.Text = Convert.ToDecimal(Aqty).ToString("0.00##");
                    txtQty.Enabled = true;

                }
                else
                {
                    txtRate.Text = "";
                    LabAQty.Text = "";
                    txtQty.Text = "";
                }
            }
            else
            {
                txtRate.Text = "";
                LabAQty.Text = "";
                txtQty.Text = "";
            }
        }
        void FillRecord(ConsumptionMaster _Im)
        {
            try
            {
                CmbMachine.SelectedItem = _Im.MachineMaster.Machine.ToString();
                CmbLotno.SelectedValue = Convert.ToInt64(_Im.MachinId);
                DtpDate.Value = Convert.ToDateTime(_Im.Date);
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
        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                ItemMaster _Im = CmbItem.SelectedItem as ItemMaster;
                if (_Im.Type != "Raw Materlal")
                {
                    decimal AQty = LabAQty.Text != "" ? Decimal.Round(Convert.ToDecimal(LabAQty.Text), 0) : 0;
                    decimal IssueQty = txtQty.Text != "" ? Decimal.Round(Convert.ToDecimal(txtQty.Text), 0) : 0;
                    if (_ConsumptionDetail != null)
                    {
                        AQty = AQty + Convert.ToDecimal(_ConsumptionDetail.Qty);
                    }
                    if (IssueQty > AQty)
                    {
                        obj.Information("Out Of Stock. (Available Qty : " + AQty.ToString() + ")");
                        txtQty.Text = "";
                    }
                }
            }
        }
        void FillConsumptionDetail()
        {
            if (_ConsumptionMaster.ConId != 0)
            {
                _listConsumptionDetail = new BindingList<ConsumptionDetail>(_KPDB.ConsumptionDetails.Where(w => w.ConId == _ConsumptionMaster.ConId && w.IsDelete != true).ToList());
            }
            else
                _listConsumptionDetail = new BindingList<ConsumptionDetail>();
            GridConsumptiondetail.DataSource = null;
            GridConsumptiondetail.DataSource = _listConsumptionDetail;
            GridConsumptiondetail.Columns["ConDId"].Visible = false;
            GridConsumptiondetail.Columns["ConId"].Visible = false;
            GridConsumptiondetail.Columns["IMId"].Visible = false;
            GridConsumptiondetail.Columns["RDId"].Visible = false;
            GridConsumptiondetail.Columns["MMId"].Visible = false;
            GridConsumptiondetail.Columns["ConsumptionMaster"].Visible = false;
            GridConsumptiondetail.Columns["ItemMaster"].Visible = false;
            GridConsumptiondetail.Columns["RewinderDetail"].Visible = false;
            GridConsumptiondetail.Columns["MixingMaster"].Visible = false;
            GridConsumptiondetail.Columns["Total"].Visible = false;
            GridConsumptiondetail.Columns["Qty"].HeaderText = "weight";
            GridConsumptiondetail.Columns["AddDate"].Visible = false;
            GridConsumptiondetail.Columns["EditDate"].Visible = false;
            GridConsumptiondetail.Columns["IsDelete"].Visible = false;
            GridConsumptiondetail.Columns["DeleteDate"].Visible = false;
            GridConsumptiondetail.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            GridConsumptiondetail.Columns["Rate"].DefaultCellStyle.Format = "0.00##";

            btnDelete.Enabled = false;
        }

        bool CanAddConsumptionDetail()
        {
            if (CmbItem.SelectedIndex < 0)
            {
                obj.Information("Seletc Item.");
                CmbItem.Focus();
                return false;
            }
            else if (CmbRewMix.Enabled == true && CmbRewMix.SelectedIndex < 0)
            {
                obj.Information("Select Rewinder / Mixing No.");
                CmbRewMix.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                obj.Information("Enter Qty.");
                txtQty.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtRate.Text.Trim()))
            {
                obj.Information("Enter Rate.");
                txtRate.Focus();
                return false;
            }
            else if (CmbRewMix.Enabled == false)
            {
                if (_ConsumptionDetail == null && _listConsumptionDetail.Where(w => w.IMId == Convert.ToInt64(CmbItem.SelectedValue)).Any())
                {
                    obj.Information("Already exist Item.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (_ConsumptionDetail == null && _listConsumptionDetail.Where(w => w.MMId != null && w.MMId == Convert.ToInt64(CmbRewMix.SelectedValue)).Any())
            {
                obj.Information("Already exist Item.");
                return false;
            }
            else if (_ConsumptionDetail == null && _listConsumptionDetail.Where(w => w.RDId != null && w.RDId == Convert.ToInt64(CmbRewMix.SelectedValue)).Any())
            {
                obj.Information("Already exist Item.");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (CanAddConsumptionDetail())
            {
                if (_ConsumptionDetail != null)
                {
                    _ConsumptionDetail.ItemMaster = CmbItem.SelectedItem as ItemMaster;
                    if (_ConsumptionDetail.ItemMaster.ProcessType == "Re-windable")
                    {
                        //_ConsumptionDetail.RewinderDetail = CmbRewMix.SelectedItem as RewinderDetail;
                        //_ConsumptionDetail.RDId = Convert.ToInt64(CmbRewMix.SelectedValue);
                        //_ConsumptionDetail.MMId = null;
                    }
                    else if (_ConsumptionDetail.ItemMaster.ProcessType == "Mixable")
                    {
                        _ConsumptionDetail.MixingMaster = CmbRewMix.SelectedItem as MixingMaster;
                        _ConsumptionDetail.MMId = Convert.ToInt64(CmbRewMix.SelectedValue);
                        _ConsumptionDetail.RDId = null;
                    }
                    else
                    {
                        _ConsumptionDetail.MMId = null;
                        _ConsumptionDetail.RDId = null;

                    }
                    _ConsumptionDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ConsumptionDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    _ConsumptionDetail.EditDate = DateTime.Now;
                }
                else
                {
                    _ConsumptionDetail = new ConsumptionDetail();
                    _ConsumptionDetail.ItemMaster = CmbItem.SelectedItem as ItemMaster;
                    if (_ConsumptionDetail.ItemMaster.ProcessType == "Re-windable")
                    {
                        _ConsumptionDetail.RewinderDetail = CmbRewMix.SelectedItem as RewinderDetail;
                        _ConsumptionDetail.RDId = Convert.ToInt64(CmbRewMix.SelectedValue);
                        _ConsumptionDetail.MMId = null;
                    }
                    else if (_ConsumptionDetail.ItemMaster.ProcessType == "Mixable")
                    {
                        _ConsumptionDetail.MixingMaster = CmbRewMix.SelectedItem as MixingMaster;
                        _ConsumptionDetail.MMId = Convert.ToInt64(CmbRewMix.SelectedValue);
                        _ConsumptionDetail.RDId = null;
                    }
                    else
                    {
                        _ConsumptionDetail.MMId = null;
                        _ConsumptionDetail.RDId = null;

                    }
                    _ConsumptionDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ConsumptionDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    _ConsumptionDetail.AddDate = DateTime.Now;
                    _ConsumptionDetail.EditDate = null;
                    _ConsumptionDetail.IsDelete = false;
                    _ConsumptionDetail.DeleteDate = null;
                    _listConsumptionDetail.Add(_ConsumptionDetail);
                }
                ClearConsumptionDetail();
            }
        }
        void ClearConsumptionDetail()
        {
            _ConsumptionDetail = null;
            CmbItem.SelectedIndex = -1;
            CmbRewMix.SelectedIndex = -1;
            txtQty.Text = "";
            txtRate.Text = "";
            LabAQty.Text = "";
            btnAdd.Text = "&Add";
            btnDelete.Enabled = false;
            CmbItem.Enabled = true;
            CmbRewMix.Enabled = true;
            CmbItem.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_ConsumptionDetail != null)
            {
                if (obj.Question("Are you sure ?") == DialogResult.Yes)
                {
                    if (_ConsumptionDetail.ConDId != 0)
                    {
                        _DeletelistConsumptionDetail.Add(_ConsumptionDetail);
                    }
                    _listConsumptionDetail.Remove(_ConsumptionDetail);
                    ClearConsumptionDetail();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearConsumptionDetail();
        }

        private void GridConsumptiondetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GridConsumptiondetail.Rows.Count)
            {
                _ConsumptionDetail = _listConsumptionDetail[GridConsumptiondetail.CurrentRow.Index];
                btnAdd.Text = "&Update";
                FilCD(_ConsumptionDetail);
                btnDelete.Enabled = true;

            }
        }
        void FilCD(ConsumptionDetail _RND)
        {
            if (_RND != null)
            {
                CmbItem.SelectedValue = _RND.IMId;
                if (_RND.RDId != null)
                {
                    CmbRewMix.SelectedValue = _RND.RewinderDetail.RewinderMaster.InwId;
                    CmbRewMix.Enabled = false;
                }
                else if (_RND.MMId != null)
                {
                    CmbRewMix.SelectedValue = _RND.MMId;
                }
                else
                {
                    CmbRewMix.Enabled = false;
                }
                if (_RND.ConDId != 0)
                {
                    CmbItem.Enabled = false;
                    CmbRewMix.Enabled = false;
                }
                CmbItem.SelectedValue = _RND.IMId;
                txtQty.Text = _RND.Qty.ToString();
                txtRate.Text = _RND.Rate.ToString();

            }
        }

        private bool Cansave()
        {
            if (CmbMachine.SelectedIndex < 0)
            {
                obj.Information("Select Machine");
                CmbMachine.Focus();
                return false;
            }
            else if (CmbLotno.SelectedIndex < 0)
            {
                obj.Information("Select Lot No.");
                CmbLotno.Focus();
                return false;
            }
            else if (_listConsumptionDetail.Count == 0)
            {
                obj.Information("Add one ConsumptionDetail Item.");
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
                    _ConsumptionMaster.MachinId = Convert.ToInt64(CmbLotno.SelectedValue);
                    _ConsumptionMaster.Date = DtpDate.Value;
                    if (_DeletelistConsumptionDetail.Count > 0)
                    {
                        foreach (ConsumptionDetail item in _DeletelistConsumptionDetail)
                        {
                            ItemMaster Pd = item.ItemMaster;
                            if (item.MMId != null)
                            {
                                item.MixingMaster.IssueWeight = item.MixingMaster.IssueWeight - item.Qty;
                            }
                            else if (item.RDId != null)
                            {
                                item.RewinderDetail.IsUse = false;

                            }
                            else
                            {
                                Pd.Qty = Pd.Qty + item.Qty;
                            }
                            item.IsDelete = true;
                            item.DeleteDate = DateTime.Now;
                        }
                    }

                    if (_listConsumptionDetail.Count > 0)
                    {
                        foreach (ConsumptionDetail item in _listConsumptionDetail)
                        {
                            if (item.ConDId == 0)
                            {
                                item.ConsumptionMaster = _ConsumptionMaster;
                                ItemMaster Pd = item.ItemMaster;
                                if (Pd.ProcessType == "Re-windable")
                                {
                                    RewinderDetail RD = _KPDB.RewinderDetails.Where(w => w.RewDId == item.RDId).SingleOrDefault();
                                    RD.IsUse = true;
                                }
                                else if (Pd.ProcessType == "Mixable")
                                {
                                    MixingMaster MM = _KPDB.MixingMasters.Where(w => w.MMId == item.MMId).SingleOrDefault();
                                    MM.IssueWeight = MM.IssueWeight + item.Qty;
                                }
                                else
                                {
                                    ItemMaster Im = _KPDB.ItemMasters.Where(w => w.IMId == item.IMId).SingleOrDefault();
                                    Im.Qty = Im.Qty - item.Qty;

                                }
                                item.AddDate = DateTime.Now;
                                item.EditDate = null;
                                item.IsDelete = false;
                                item.DeleteDate = null;
                                _KPDB.ConsumptionDetails.InsertOnSubmit(item);
                            }
                            else
                            {
                                KrishnaPackagingDbDataContext _KPDB1 = new KrishnaPackagingDbDataContext();
                                ConsumptionDetail Sd = _KPDB1.ConsumptionDetails.Where(w => w.ConDId == item.ConDId && w.IsDelete != true).SingleOrDefault();
                                ItemMaster Pd = item.ItemMaster;
                                if (Pd.ProcessType == "Re-windable")
                                {

                                }
                                else if (Pd.ProcessType == "Mixable")
                                {
                                    MixingMaster MM = _KPDB.MixingMasters.Where(w => w.MMId == item.MMId).SingleOrDefault();
                                    MM.IssueWeight = (MM.IssueWeight - Sd.Qty) + item.Qty;
                                }
                                else
                                {
                                    ItemMaster Im = _KPDB.ItemMasters.Where(w => w.IMId == item.IMId).SingleOrDefault();
                                    Im.Qty = (Im.Qty + Sd.Qty) - item.Qty;
                                }
                                item.EditDate = DateTime.Now;
                            }
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {
                        _ConsumptionMaster.CompanyId = Program.CompanyId;
                        _ConsumptionMaster.AddDate = DateTime.Now;
                        _ConsumptionMaster.EditDate = null;
                        _ConsumptionMaster.IsDelete = false;
                        _ConsumptionMaster.DeleteDate = null;
                        _KPDB.ConsumptionMasters.InsertOnSubmit(_ConsumptionMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _ConsumptionMaster.EditDate = DateTime.Now;
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


    }
}
