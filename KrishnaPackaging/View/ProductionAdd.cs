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
    public partial class ProductionAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        ProductionMaster _ProductionMaster;
        BindingList<ProductionDetail> _listProductionDetail;
        BindingList<ProductionDetail> _DeletelistProductionDetail = new BindingList<ProductionDetail>();
        ProductionDetail _ProductionDetail;
        public ProductionAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, ProductionMaster _productionmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Production";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _ProductionMaster = _productionmaster;
                FillProductionDetail();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Production";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _ProductionMaster = _productionmaster;
                FillRecord(_ProductionMaster);
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
        private void CmbMachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbMachine.SelectedIndex >= 0)
            {
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.IsClose != true && w.Machine == CmbMachine.SelectedItem.ToString() && w.CompanyId == Program.CompanyId ).ToList());
                CmbLotno.DataSource = null;
                CmbLotno.DataSource = ListOfCategoryMaster;
                CmbLotno.DisplayMember = "LotNo";
                CmbLotno.ValueMember = "MachinId";
                CmbLotno.SelectedIndex = -1;
            }
        }
        void FillRecord(ProductionMaster _Im)
        {
            try
            {
                CmbMachine.SelectedItem = _Im.Machine.ToString();
                DtpDate.Value = Convert.ToDateTime(_Im.Date);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void FillProductionDetail()
        {
            if (_ProductionMaster.ProId != 0)
            {
                _listProductionDetail = new BindingList<ProductionDetail>(_KPDB.ProductionDetails.Where(w => w.ProId == _ProductionMaster.ProId && w.IsDelete != true).ToList());
            }
            else
                _listProductionDetail = new BindingList<ProductionDetail>();
            GridConsumptiondetail.DataSource = null;
            GridConsumptiondetail.DataSource = _listProductionDetail;
            GridConsumptiondetail.Columns["ProDId"].Visible = false;
            GridConsumptiondetail.Columns["ProId"].Visible = false;
            GridConsumptiondetail.Columns["MachineId"].Visible = false;
            GridConsumptiondetail.Columns["MachineMaster"].Visible = false;
            GridConsumptiondetail.Columns["ProductionMaster"].Visible = false;
            GridConsumptiondetail.Columns["AddDate"].Visible = false;
            GridConsumptiondetail.Columns["EditDate"].Visible = false;
            GridConsumptiondetail.Columns["IsDelete"].Visible = false;
            GridConsumptiondetail.Columns["DeleteDate"].Visible = false;
            GridConsumptiondetail.Columns["Production"].DefaultCellStyle.Format = "0.00##";
            GridConsumptiondetail.Columns["Weight"].DefaultCellStyle.Format = "0.00##";

            btnDelete.Enabled = false;
        }

        bool CanAddConsumptionDetail()
        {
            if (CmbLotno.SelectedIndex < 0)
            {
                obj.Information("Seletc Lot No.");
                CmbLotno.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtProduction.Text.Trim()))
            {
                obj.Information("Enter Production.");
                txtProduction.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                obj.Information("Enter Weight.");
                txtQty.Focus();
                return false;
            }
            else if (_ProductionDetail == null && _listProductionDetail.Where(w => w.MachineId == Convert.ToInt64(CmbLotno.SelectedValue)).Any())
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
                if (_ProductionDetail != null)
                {
                    _ProductionDetail.MachineMaster = CmbLotno.SelectedItem as MachineMaster;
                    _ProductionDetail.Production = string.IsNullOrEmpty(txtProduction.Text) ? 0 : Convert.ToDecimal(txtProduction.Text);
                    _ProductionDetail.Weight = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ProductionDetail.Unit = CmbUnit.SelectedItem.ToString();
                    _ProductionDetail.EditDate = DateTime.Now;
                }
                else
                {
                    _ProductionDetail = new ProductionDetail();
                    _ProductionDetail.MachineMaster = CmbLotno.SelectedItem as MachineMaster;
                    _ProductionDetail.Production = string.IsNullOrEmpty(txtProduction.Text) ? 0 : Convert.ToDecimal(txtProduction.Text);
                    _ProductionDetail.Weight = string.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
                    _ProductionDetail.Unit = CmbUnit.SelectedItem.ToString();
                    _ProductionDetail.AddDate = DateTime.Now;
                    _ProductionDetail.EditDate = null;
                    _ProductionDetail.IsDelete = false;
                    _ProductionDetail.DeleteDate = null;
                    _listProductionDetail.Add(_ProductionDetail);
                }
                ClearConsumptionDetail();
            }
        }
        void ClearConsumptionDetail()
        {
            _ProductionDetail = null;
            CmbLotno.SelectedIndex = -1;
            txtQty.Text = "";
            txtProduction.Text = "";
            LabAQty.Text = "";
            btnAdd.Text = "&Add";
            btnDelete.Enabled = false;
            CmbLotno.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_ProductionDetail != null)
            {
                if (obj.Question("Are you sure ?") == DialogResult.Yes)
                {
                    if (_ProductionDetail.ProDId != 0)
                    {
                        _DeletelistProductionDetail.Add(_ProductionDetail);
                    }
                    _listProductionDetail.Remove(_ProductionDetail);
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
                _ProductionDetail = _listProductionDetail[GridConsumptiondetail.CurrentRow.Index];
                FilCD(_ProductionDetail);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
        }
        void FilCD(ProductionDetail _RND)
        {
            if (_RND != null)
            {
                CmbLotno.SelectedValue = _RND.MachineId;
                txtQty.Text =Convert.ToDecimal(_RND.Weight).ToString("0.00##");
                txtProduction.Text = Convert.ToDecimal(_RND.Production).ToString("0.00##");
                CmbUnit.SelectedItem = _RND.Unit.ToString();
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
            else if (_listProductionDetail.Count == 0)
            {
                obj.Information("Add one ProductionDetail Item.");
                return false;
            }
            else
                return true;
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.SelectedText = "";
            e.Handled = !obj.IsNumericThreeDecimalPlace(txt.Text, e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cansave())
                {
                    _ProductionMaster.Machine = CmbMachine.SelectedItem.ToString();
                    _ProductionMaster.Date = DtpDate.Value;
                    if (_DeletelistProductionDetail.Count > 0)
                    {
                        foreach (ProductionDetail item in _DeletelistProductionDetail)
                        {
                            item.IsDelete = true;
                            item.DeleteDate = DateTime.Now;
                        }
                    }

                    if (_listProductionDetail.Count > 0)
                    {
                        foreach (ProductionDetail item in _listProductionDetail)
                        {
                            if (item.ProDId == 0)
                            {
                                item.ProductionMaster = _ProductionMaster;
                                item.AddDate = DateTime.Now;
                                item.EditDate = null;
                                item.IsDelete = false;
                                item.DeleteDate = null;
                                _KPDB.ProductionDetails.InsertOnSubmit(item);
                            }
                            else
                            {
                                item.EditDate = DateTime.Now;
                            }
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {
                        _ProductionMaster.CompanyId = Program.CompanyId;
                        _ProductionMaster.AddDate = DateTime.Now;
                        _ProductionMaster.EditDate = null;
                        _ProductionMaster.IsDelete = false;
                        _ProductionMaster.DeleteDate = null;
                        _KPDB.ProductionMasters.InsertOnSubmit(_ProductionMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _ProductionMaster.EditDate = DateTime.Now;
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

        private void ProductionAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
