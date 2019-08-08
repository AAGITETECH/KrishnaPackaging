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
    public partial class DispatchAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        DispatchMaster _DispatchMaster;
        BindingList<DispatchDetail> _listDispatchDetail;
        BindingList<DispatchDetail> _DeletelistDispatchDetail = new BindingList<DispatchDetail>();
        DispatchDetail _DispatchDetail;
        public DispatchAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, DispatchMaster _dispatchmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            FillComboBox();
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Dispatch";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _DispatchMaster = _dispatchmaster;
                FillProductionDetail();
                FillReceiveNoteNo();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Dispatch";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _DispatchMaster = _dispatchmaster;
                FillRecord(_DispatchMaster);
                FillProductionDetail();
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
            BindingList<PartyMaster> ListOfCategoryMaster = new BindingList<PartyMaster>(_KPDB.PartyMasters.Where(w => w.IsDelete != true && w.Type != "Purchase" && w.CompanyId == Program.CompanyId).ToList());
            CmbParty.DataSource = null;
            CmbParty.DataSource = ListOfCategoryMaster;
            CmbParty.DisplayMember = "PartyName";
            CmbParty.ValueMember = "PartyId";
            CmbParty.SelectedIndex = -1;
        }
        void FillReceiveNoteNo()
        {
            var SM = _KPDB.DispatchMasters.Where(w => w.IsDelete != true).OrderByDescending(o => o.DMId).FirstOrDefault();
            if (SM == null)
            {
                txtDNO.Text = "DIS001";
            }
            else
            {
                int INN = Convert.ToInt32(SM.DNo.Split('S')[1]) + 1;
                if (INN.ToString().Length == 1)
                {
                    txtDNO.Text = "DIS00" + INN.ToString();
                }
                else if (INN.ToString().Length == 2)
                {
                    txtDNO.Text = "DIS0" + INN.ToString();
                }
                else
                {
                    txtDNO.Text = "DIS" + INN.ToString();
                }
            }
        }
        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbMachine.SelectedIndex >= 0)
            {
                CmbLotno.SelectedIndexChanged -= new EventHandler(CmbLotno_SelectedIndexChanged);
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.Machine == CmbMachine.SelectedItem.ToString() && w.CompanyId == Program.CompanyId ).ToList());
                CmbLotno.DataSource = null;
                CmbLotno.DataSource = ListOfCategoryMaster.Where(w => w.Stock > 0).ToList();
                CmbLotno.DisplayMember = "LotNo";
                CmbLotno.ValueMember = "MachinId";
                CmbLotno.SelectedIndex = -1;
                CmbLotno.SelectedIndexChanged += new EventHandler(CmbLotno_SelectedIndexChanged);
            }
            else
            {
                LabAQty.Text = "0";
                txtQty.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
            }

        }
        private void CmbLotno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbLotno.SelectedIndex >= 0)
            {
                MachineMaster M = CmbLotno.SelectedItem as MachineMaster;
                LabAQty.Text = Convert.ToDecimal(M.Stock).ToString();
                decimal ConAmount = Convert.ToDecimal(M.ConsumptionMasters.Where(w => w.IsDelete != true).Sum(s => s.ConsumptionDetails.Where(w => w.IsDelete != true).Sum(c => (c.Rate * c.Qty))));
                decimal ProAmount = Convert.ToDecimal(M.ProductionDetails.Where(w => w.IsDelete != true).Sum(s => s.Production));
                string Unit = M.ProductionDetails.Where(w => w.IsDelete != true).Select(s => s.Unit).FirstOrDefault();
                decimal Rate = M.Costing;// (ConAmount / ProAmount);
                LabUnit.Text = Unit;
                txtRate.Text = Rate.ToString("N2");
                CalAmount();
            }
            else
            {
                LabAQty.Text = "0";
                txtQty.Text = "0";
                txtRate.Text = "0";
                txtAmount.Text = "0";
                LabUnit.Text = "";
            }
        }
        void CalAmount()
        {
            decimal Qty = txtQty.Text != "" ? Decimal.Round(Convert.ToDecimal(txtQty.Text), 0) : 0;
            decimal Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Decimal.Round(Convert.ToDecimal(txtRate.Text), 2);
            decimal Amt = Rate * Qty;
            txtAmount.Text = Amt.ToString("N2");
        }
        void FillRecord(DispatchMaster _Im)
        {
            try
            {
                CmbParty.SelectedValue = _Im.PartyId;
                DtpDate.Value = Convert.ToDateTime(_Im.Date);
                txtDNO.Text = _Im.DNo;
                DTPPoDate.Value = Convert.ToDateTime(_Im.PartyPODate);
                txtPoNo.Text = _Im.PartyPoNo;
                txtinvoiceno.Text = _Im.InvoiceNo;
                txtVehicleNo.Text = _Im.VehicleNo;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void FillProductionDetail()
        {
            if (_DispatchMaster.DMId != 0)
            {
                _listDispatchDetail = new BindingList<DispatchDetail>(_KPDB.DispatchDetails.Where(w => w.DMId == _DispatchMaster.DMId && w.IsDelete != true).ToList());
            }
            else
                _listDispatchDetail = new BindingList<DispatchDetail>();
            GridConsumptiondetail.DataSource = null;
            GridConsumptiondetail.DataSource = _listDispatchDetail;
            GridConsumptiondetail.Columns["DDId"].Visible = false;
            GridConsumptiondetail.Columns["DMId"].Visible = false;
            GridConsumptiondetail.Columns["Weight"].Visible = false;
            GridConsumptiondetail.Columns["MachineId"].Visible = false;
            GridConsumptiondetail.Columns["MachineMaster"].Visible = false;
            GridConsumptiondetail.Columns["DispatchMaster"].Visible = false;
            GridConsumptiondetail.Columns["AddDate"].Visible = false;
            GridConsumptiondetail.Columns["EditDate"].Visible = false;
            GridConsumptiondetail.Columns["IsDelete"].Visible = false;
            GridConsumptiondetail.Columns["DeleteDate"].Visible = false;
            btnDelete.Enabled = false;
        }

        bool CanAddConsumptionDetail()
        {
            if (CmbMachine.SelectedIndex < 0)
            {
                obj.Information("Seletc Machine.");
                CmbLotno.Focus();
                return false;
            }
            else if (CmbLotno.SelectedIndex < 0)
            {
                obj.Information("Seletc Lot No.");
                CmbLotno.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                obj.Information("Enter Qty.");
                txtQty.Focus();
                return false;
            }
            else if (_DispatchDetail == null && _listDispatchDetail.Where(w => w.MachineId == Convert.ToInt64(CmbLotno.SelectedValue)).Any())
            {
                obj.Information("Already exist Lot No.");
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
                if (_DispatchDetail != null)
                {
                    _DispatchDetail.MachineMaster = CmbLotno.SelectedItem as MachineMaster;
                    _DispatchDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _DispatchDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    //_DispatchDetail.Weight = string.IsNullOrEmpty(txtWeight.Text) ? 0 : Convert.ToDecimal(txtWeight.Text);
                    _DispatchDetail.Amount = string.IsNullOrEmpty(txtAmount.Text) ? 0 : Convert.ToDecimal(txtAmount.Text);
                    _DispatchDetail.EditDate = DateTime.Now;
                }
                else
                {
                    _DispatchDetail = new DispatchDetail();
                    _DispatchDetail.MachineMaster = CmbLotno.SelectedItem as MachineMaster;
                    _DispatchDetail.Qty = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _DispatchDetail.Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                    //_DispatchDetail.Weight = string.IsNullOrEmpty(txtWeight.Text) ? 0 : Convert.ToDecimal(txtWeight.Text);
                    _DispatchDetail.Amount = string.IsNullOrEmpty(txtAmount.Text) ? 0 : Convert.ToDecimal(txtAmount.Text);
                    _DispatchDetail.AddDate = DateTime.Now;
                    _DispatchDetail.EditDate = null;
                    _DispatchDetail.IsDelete = false;
                    _DispatchDetail.DeleteDate = null;
                    _listDispatchDetail.Add(_DispatchDetail);
                }
                ClearConsumptionDetail();
            }
        }
        void ClearConsumptionDetail()
        {
            _DispatchDetail = null;
            CmbMachine.SelectedIndex = -1;
            CmbLotno.SelectedIndex = -1;
            txtQty.Text = "";
            txtRate.Text = "";
            LabAQty.Text = "";
            LabUnit.Text = "";
                btnAdd.Text = "&Add";
            btnDelete.Enabled = false;
            CmbMachine.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_DispatchDetail != null)
            {
                if (obj.Question("Are you sure ?") == DialogResult.Yes)
                {
                    if (_DispatchDetail.DDId != 0)
                    {
                        _DeletelistDispatchDetail.Add(_DispatchDetail);
                    }
                    _listDispatchDetail.Remove(_DispatchDetail);
                    ClearConsumptionDetail();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearConsumptionDetail();
        }
        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (CmbLotno.SelectedIndex >= 0)
            {
                //MachineMaster _Im = CmbLotno.SelectedItem as MachineMaster;
                decimal AQty = LabAQty.Text != "" ? Decimal.Round(Convert.ToDecimal(LabAQty.Text), 0) : 0;
                decimal IssueQty = txtQty.Text != "" ? Decimal.Round(Convert.ToDecimal(txtQty.Text), 0) : 0;

                //if (_DispatchDetail.dd != 0)
                //{
                //    AQty = AQty + Convert.ToDecimal(_MixingMaster.IssueQty);
                //}
                if (IssueQty > AQty)
                {
                    txtQty.Text = "";
                    obj.Information("Out Of Stock Qty..");
                }
            }
            CalAmount();
        }
        private void txtQty_Leave(object sender, EventArgs e)
        {
            CalAmount();

        }
        private void GridConsumptiondetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GridConsumptiondetail.Rows.Count)
            {
                _DispatchDetail = _listDispatchDetail[GridConsumptiondetail.CurrentRow.Index];
                FilCD(_DispatchDetail);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
        void FilCD(DispatchDetail _RND)
        {
            if (_RND != null)
            {
                CmbMachine.SelectedItem = _RND.MachineMaster.Machine.ToString();
                CmbLotno.SelectedValue = _RND.MachineId;
                txtQty.Text =Convert.ToDecimal(_RND.Qty).ToString("0.00##");
                txtRate.Text = Convert.ToDecimal(_RND.Rate).ToString("0.00##");
                //txtWeight.Text = _RND.Weight.ToString();
                txtAmount.Text = _RND.Amount.ToString();
            }
        }

        private bool Cansave()
        {
            if (CmbParty.SelectedIndex < 0)
            {
                obj.Information("Select Party");
                CmbMachine.Focus();
                return false;
            }
            else if (_listDispatchDetail.Count == 0)
            {
                obj.Information("Add one DispatchDetail Item.");
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
                    _DispatchMaster.PartyMaster = CmbParty.SelectedItem as PartyMaster;
                    _DispatchMaster.Date = DtpDate.Value;
                    _DispatchMaster.DNo = txtDNO.Text;
                    _DispatchMaster.InvoiceNo = txtinvoiceno.Text;
                    _DispatchMaster.VehicleNo = txtVehicleNo.Text;
                    _DispatchMaster.PartyPoNo = txtPoNo.Text;
                    _DispatchMaster.PartyPODate = DTPPoDate.Value;
                    if (_DeletelistDispatchDetail.Count > 0)
                    {
                        foreach (DispatchDetail item in _DeletelistDispatchDetail)
                        {
                            item.IsDelete = true;
                            item.DeleteDate = DateTime.Now;
                        }
                    }

                    if (_listDispatchDetail.Count > 0)
                    {
                        foreach (DispatchDetail item in _listDispatchDetail)
                        {
                            if (item.DDId == 0)
                            {
                                item.DispatchMaster = _DispatchMaster;
                                item.AddDate = DateTime.Now;
                                item.EditDate = null;
                                item.IsDelete = false;
                                item.DeleteDate = null;
                                _KPDB.DispatchDetails.InsertOnSubmit(item);
                            }
                            else
                            {
                                item.EditDate = DateTime.Now;
                            }
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {
                        _DispatchMaster.CompanyId = Program.CompanyId;
                        _DispatchMaster.AddDate = DateTime.Now;
                        _DispatchMaster.EditDate = null;
                        _DispatchMaster.IsDelete = false;
                        _DispatchMaster.DeleteDate = null;
                        _KPDB.DispatchMasters.InsertOnSubmit(_DispatchMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _DispatchMaster.EditDate = DateTime.Now;
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

        private void ProductionAdd_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
