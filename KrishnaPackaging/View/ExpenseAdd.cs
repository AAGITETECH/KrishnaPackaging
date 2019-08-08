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
    public partial class ExpenseAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        ExpenseMaster _ExpenseMaster;
        BindingList<MachineMaster> _ListOfMachine;
        public ExpenseAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, ExpenseMaster _expenseMaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            FillLotNo();
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Expense";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _ExpenseMaster = _expenseMaster;
                FillExpenseNo();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Expense";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _ExpenseMaster = _expenseMaster;
                FillRecord(_ExpenseMaster);
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


        void FillExpenseNo()
        {
            var SM = _KPDB.ExpenseMasters.Where(w => w.IsDelete != true).OrderByDescending(o => o.EMId).FirstOrDefault();
            if (SM == null)
            {
                txtENo.Text = "1";
            }
            else
            {
                int INN = Convert.ToInt32(SM.ExpenseNo) + 1;
                txtENo.Text = INN.ToString();

            }
        }

        void FillLotNo()
        {
            _ListOfMachine = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.IsClose != true).ToList());
            GridLotNo.DataSource = null;
            GridLotNo.DataSource = _ListOfMachine;
            GridLotNo.Columns["MachinId"].Visible = false;
            GridLotNo.Columns["IsClose"].Visible = false;
            GridLotNo.Columns["CompanyId"].Visible = false;
            GridLotNo.Columns["AddDate"].Visible = false;
            GridLotNo.Columns["EditDate"].Visible = false;
            GridLotNo.Columns["Stock"].Visible = false;
            GridLotNo.Columns["Costing"].Visible = false;
            GridLotNo.Columns["IsDelete"].Visible = false;
            GridLotNo.Columns["DeleteDate"].Visible = false;
            GridLotNo.Columns["CompanyMaster"].Visible = false;
            GridLotNo.Columns["Machine"].Visible = false;
            GridLotNo.Columns["Date"].Visible = false;
            GridLotNo.Columns["IsExpense"].DisplayIndex = 0;
            GridLotNo.Columns["LotNo"].DisplayIndex = 1;
            GridLotNo.Columns["DegreeInnerDiameter"].Visible = false;
            GridLotNo.Columns["Size"].Visible = false;
            GridLotNo.Columns["HeightLength"].Visible = false;
            GridLotNo.Columns["CSType"].Visible = false;
            GridLotNo.Columns["DesignThickness"].Visible = false;
            GridLotNo.Columns["Weight"].Visible = false;
            GridLotNo.Columns["Amount"].Visible = false;
        }

        void FillRecord(ExpenseMaster _Im)
        {
            try
            {
                txtENo.Text = _Im.ExpenseNo;
                DtpDate.Value = Convert.ToDateTime(_Im.Date);
                txtSalary.Text = Convert.ToDecimal(_Im.Salary).ToString("N2");
                txtTransportation.Text = Convert.ToDecimal(_Im.Transportation).ToString("N2");
                txtBanking.Text = Convert.ToDecimal(_Im.Banking).ToString("N2");
                txtPower.Text = Convert.ToDecimal(_Im.Power).ToString("N2");
                txtFuel.Text = Convert.ToDecimal(_Im.Fuel).ToString("N2");
                txtOthers.Text = Convert.ToDecimal(_Im.Others).ToString("N2");
                txtAdmin.Text = Convert.ToDecimal(_Im.Admin).ToString("N2");
                foreach (ExpenseDetail item in _Im.ExpenseDetails.ToList())
                {
                    _ListOfMachine.Where(w => w.MachinId == item.MachineId).ToList().ForEach(f => f.IsExpense = true);
                }
                CalculateAmount();
                //txtSalary.Text = Convert.ToDecimal(_Im.Rate).ToString("F2"); ;
                //txtSalary.Enabled = false;
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
        private void txtSalary_Leave(object sender, EventArgs e)
        {
            CalculateAmount();
        }
        void CalculateAmount()
        {
            decimal Salary = txtSalary.Text != "" ? Decimal.Round(Convert.ToDecimal(txtSalary.Text), 0) : 0;
            decimal Banking = txtBanking.Text != "" ? Decimal.Round(Convert.ToDecimal(txtBanking.Text), 2) : 0;
            decimal Power = txtPower.Text != "" ? Decimal.Round(Convert.ToDecimal(txtPower.Text), 2) : 0;
            decimal Fuel = txtFuel.Text != "" ? Decimal.Round(Convert.ToDecimal(txtFuel.Text), 2) : 0;
            decimal Others = txtOthers.Text != "" ? Decimal.Round(Convert.ToDecimal(txtOthers.Text), 2) : 0;
            decimal Admin = txtAdmin.Text != "" ? Decimal.Round(Convert.ToDecimal(txtAdmin.Text), 2) : 0;
            decimal Transportation = txtTransportation.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTransportation.Text), 2) : 0;
            decimal Amount = Salary + Banking + Power + Fuel + Others + Admin + Transportation;
            LabTotalExpense.Text = Amount.ToString("N2");
        }
        private bool Cansave()
        {
            if (_ListOfMachine.Where(w => w.IsExpense == true).Count() == 0)
            {
                obj.Information("Select more than one LotNo.");
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
                    _ExpenseMaster.Date = DtpDate.Value;
                    _ExpenseMaster.Salary = txtSalary.Text != "" ? Decimal.Round(Convert.ToDecimal(txtSalary.Text), 0) : 0;
                    _ExpenseMaster.Banking = txtBanking.Text != "" ? Decimal.Round(Convert.ToDecimal(txtBanking.Text), 2) : 0;
                    _ExpenseMaster.Power = txtPower.Text != "" ? Decimal.Round(Convert.ToDecimal(txtPower.Text), 2) : 0;
                    _ExpenseMaster.Fuel = txtFuel.Text != "" ? Decimal.Round(Convert.ToDecimal(txtFuel.Text), 2) : 0;
                    _ExpenseMaster.Others = txtOthers.Text != "" ? Decimal.Round(Convert.ToDecimal(txtOthers.Text), 2) : 0;
                    _ExpenseMaster.Admin = txtAdmin.Text != "" ? Decimal.Round(Convert.ToDecimal(txtAdmin.Text), 2) : 0;
                    _ExpenseMaster.Transportation = txtTransportation.Text != "" ? Decimal.Round(Convert.ToDecimal(txtTransportation.Text), 2) : 0;
                    if (_ExpenseMaster.EMId != 0)
                    {
                        _KPDB.ExpenseDetails.DeleteAllOnSubmit(_ExpenseMaster.ExpenseDetails.ToList());
                    }
                    int Count = _ListOfMachine.Where(w => w.IsExpense == true).Count();
                    foreach (MachineMaster item in _ListOfMachine.Where(w => w.IsExpense == true).ToList())
                    {
                        if (item.IsExpense == true)
                        {
                            ExpenseDetail _ed = new ExpenseDetail();
                            _ed.MachineId = item.MachinId;
                            _ed.ExpenseMaster = _ExpenseMaster;
                            decimal Amt = Convert.ToDecimal(LabTotalExpense.Text);
                            _ed.Amount = Amt / Count;
                            _ed.CompanyId = Program.CompanyId;
                            _ed.AddDate = DateTime.Now;
                            _ed.EditDate = null;
                            _ed.IsDelete = false;
                            _ed.DeleteDate = null;
                            _KPDB.ExpenseDetails.InsertOnSubmit(_ed);
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {

                        _ExpenseMaster.ExpenseNo = txtENo.Text;
                        _ExpenseMaster.CompanyId = Program.CompanyId;
                        _ExpenseMaster.AddDate = DateTime.Now;
                        _ExpenseMaster.EditDate = null;
                        _ExpenseMaster.IsDelete = false;
                        _ExpenseMaster.DeleteDate = null;
                        _KPDB.ExpenseMasters.InsertOnSubmit(_ExpenseMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _ExpenseMaster.EditDate = DateTime.Now;
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

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


    }
}
