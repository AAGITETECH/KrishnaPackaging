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
    public partial class ItemAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        ItemMaster _ItemMaster;
        public ItemAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, ItemMaster _itemmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Item";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _ItemMaster = _itemmaster;
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Item";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _ItemMaster = _itemmaster;
                FillRecord(_ItemMaster);
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

        void FillRecord(ItemMaster _Im)
        {
            try
            {
                txtName.Text = _Im.Name;
                txtRate.Text = Convert.ToDecimal(_Im.Rate).ToString("F2"); ;
                txtRate.Enabled = false;
                CmbType.SelectedItem = _Im.Type.ToString();
                CmbProcessType.SelectedItem = _Im.ProcessType.ToString();
                CmbInwardType.SelectedItem = _Im.InwardType.ToString();
                CmbUnit.SelectedItem = _Im.UOM.ToString();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool Cansave()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                obj.Information("Enter CategoryName");
                txtName.Focus();
                return false;
            }
            else if (CmbType.SelectedIndex < 0)
            {
                obj.Information("Select Type");
                CmbType.Focus();
                return false;
            }
            else if (CmbType.SelectedItem.ToString() == "Raw Material")
            {
                if (CmbProcessType.SelectedIndex < 0)
                {
                    obj.Information("Select ProcessType");
                    CmbProcessType.Focus();
                    return false;
                }
                else if (CmbInwardType.SelectedIndex < 0)
                {
                    obj.Information("Select InwardType");
                    CmbInwardType.Focus();
                    return false;
                }
                else
                    return true;
            }
            else if (CmbUnit.SelectedIndex < 0)
            {
                obj.Information("Select UOM");
                CmbUnit.Focus();
                return false;
            }
            else if (_KPDB.ItemMasters.Where(w => w.IsDelete != true && w.Name == txtName.Text.Trim() && w.IMId != _ItemMaster.IMId).ToList().Count() > 0)
            {
                obj.Information("Item Name already exists.");
                txtName.Focus();
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
                    _ItemMaster.Name = txtName.Text;
                    _ItemMaster.Type = CmbType.SelectedItem.ToString();
                    if (CmbType.SelectedItem.ToString() == "Raw Material")
                    {
                        _ItemMaster.ProcessType = CmbProcessType.SelectedItem.ToString();
                        _ItemMaster.InwardType = CmbInwardType.SelectedItem.ToString();
                    }
                    else
                    {
                        _ItemMaster.ProcessType = "No Applicable";
                        _ItemMaster.InwardType = "No Applicable";
                    }
                    _ItemMaster.UOM = CmbUnit.SelectedItem.ToString();
                    if (this.formMode == FormMode.Add)
                    {
                        decimal Qty = 0;
                        decimal Rate = string.IsNullOrEmpty(txtRate.Text) ? 0 : Convert.ToDecimal(txtRate.Text);
                        _ItemMaster.Rate = Rate;
                        _ItemMaster.Qty = Qty;
                        _ItemMaster.CompanyId = Program.CompanyId;
                        _ItemMaster.AddDate = DateTime.Now;
                        _ItemMaster.EditDate = null;
                        _ItemMaster.IsDelete = false;
                        _ItemMaster.DeleteDate = null;
                        _KPDB.ItemMasters.InsertOnSubmit(_ItemMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _ItemMaster.EditDate = DateTime.Now;
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
            if (CmbType.SelectedItem.ToString() == "Raw Material")
            {
                CmbProcessType.Enabled = true;
                CmbInwardType.Enabled = true;
            }
            else
            {
                CmbProcessType.Enabled = false;
                CmbInwardType.Enabled = false;
            }

        }
    }
}
