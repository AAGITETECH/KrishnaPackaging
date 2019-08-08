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
    public partial class WasteAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        WasteMaster _WasteMaster;
        public WasteAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, WasteMaster _wasteMaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Waste";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _WasteMaster = _wasteMaster;
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Waste";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _WasteMaster = _wasteMaster;
                FillRecord(_WasteMaster);
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
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.IsClose != true && w.Machine == CmbMachine.SelectedItem.ToString() && w.CompanyId == Program.CompanyId).ToList());
                CmbLotno.DataSource = null;
                CmbLotno.DataSource = ListOfCategoryMaster;
                CmbLotno.DisplayMember = "LotNo";
                CmbLotno.ValueMember = "MachinId";
                CmbLotno.SelectedIndex = -1;
            }
        }
        void FillRecord(WasteMaster _Im)
        {
            try
            {

                CmbMachine.SelectedItem = _Im.MachineMaster.Machine;
                CmbLotno.SelectedValue = _Im.MachineId;
                txtWasteWeight.Text =Convert.ToDecimal(_Im.WasteQty).ToString("N2");
                txtAmount.Text =Convert.ToDecimal(_Im.Amount).ToString("N2");
                //txtName.Text = _Im.Name;
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
             if (CmbMachine.SelectedIndex < 0)
            {
                obj.Information("Select Machine");
                CmbMachine.Focus();
                return false;
            }
            else if (CmbLotno.SelectedIndex < 0)
            {
                obj.Information("Select LotNo.");
                CmbLotno.Focus();
                return false;
            }
           
            else if (string.IsNullOrEmpty(txtWasteWeight.Text))
            {
                obj.Information("Enter WasteWeight");
                txtWasteWeight.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtAmount.Text))
            {
                obj.Information("Enter Amount");
                txtWasteWeight.Focus();
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
                    _WasteMaster.MachineId =Convert.ToInt32(CmbLotno.SelectedValue);
                    _WasteMaster.WasteQty = Convert.ToDecimal(txtWasteWeight.Text);
                    _WasteMaster.Amount = Convert.ToDecimal(txtAmount.Text);
                   
                    if (this.formMode == FormMode.Add)
                    {
                        _WasteMaster.CompanyId = Program.CompanyId;
                        _WasteMaster.Date = DateTime.Now;
                        _WasteMaster.AddDate = DateTime.Now;
                        _WasteMaster.EditDate = null;
                        _WasteMaster.IsDelete = false;
                        _WasteMaster.DeleteDate = null;
                        _KPDB.WasteMasters.InsertOnSubmit(_WasteMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _WasteMaster.EditDate = DateTime.Now;
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
