using GeneralCodeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrishnaPackaging.Data;

namespace KrishnaPackaging.View
{
    public partial class PartyAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        FormMode formMode;
        PartyMaster _PartyMaster;
        public PartyAdd()
        {
            InitializeComponent();
        }


        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, PartyMaster _partyMaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Party";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _PartyMaster = _partyMaster;
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Party";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _PartyMaster = _partyMaster;
                FillRecord(_PartyMaster);
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

        private void FillRecord(PartyMaster _PartyMaster)
        {
            try
            {
                if (_PartyMaster != null)
                {
                    txtName.Text = _PartyMaster.PartyName;
                    txtAddress1.Text = _PartyMaster.BillingAddress;
                    txtAddress2.Text = _PartyMaster.DeliveryAddress;
                    txtPersonName.Text = _PartyMaster.BillingName;
                    txtcity.Text = _PartyMaster.City;
                    txtstate.Text = _PartyMaster.State;
                    txtcontactno.Text = _PartyMaster.ContactNo;
                    txtpanno.Text = _PartyMaster.PANNo;
                    txtgstin.Text = _PartyMaster.GSTIN;
                    txtStateCode.Text = _PartyMaster.StateCode;
                    txtPincode.Text = _PartyMaster.PinCode;
                    txtEmail.Text = _PartyMaster.Email;
                    Cmbtype.SelectedItem = _PartyMaster.Type.ToString();
                    if (_PartyMaster.ReceiveNoteMasters.Where(w => w.IsDelete != true).Count() > 0)
                    {
                        Cmbtype.Enabled = false;
                    }

                }
                else
                    obj.Information("Sorry No Record Found.");
            }
            catch (Exception _Exception)
            {
                obj.Error(_Exception.Message);
                Application.ExitThread();
            }
        }

        private bool Cansave()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                obj.Information("Enter PartyName");
                return false;
            }
            if (string.IsNullOrEmpty(Cmbtype.Text.Trim()))
            {
                obj.Information("Select PartyType");
                return false;
            }
            else
                return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cansave())
                {
                    _PartyMaster.PartyName = txtName.Text;
                    _PartyMaster.BillingAddress = txtAddress1.Text;
                    _PartyMaster.BillingName = txtPersonName.Text;
                    _PartyMaster.DeliveryAddress = txtAddress2.Text;
                    _PartyMaster.City = txtcity.Text;
                    _PartyMaster.State = txtstate.Text;
                    _PartyMaster.ContactNo = txtcontactno.Text;
                    _PartyMaster.PANNo = txtpanno.Text;
                    _PartyMaster.GSTIN = txtgstin.Text;
                    _PartyMaster.StateCode = txtStateCode.Text;
                    _PartyMaster.PinCode = txtPincode.Text;
                    _PartyMaster.Email = txtEmail.Text;
                    _PartyMaster.Type = Cmbtype.SelectedItem.ToString();
                    if (this.formMode == FormMode.Add)
                    {
                        _PartyMaster.CompanyId = Program.CompanyId;
                        _PartyMaster.AddDate = DateTime.Now;
                        _PartyMaster.EditDate = null;
                        _PartyMaster.IsDelete = false;
                        _PartyMaster.DeleteDate = null;
                        _KPDB.PartyMasters.InsertOnSubmit(_PartyMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _PartyMaster.EditDate = DateTime.Now;
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtPersonName.Text = txtName.Text;
        }

        private void txtgstin_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtgstin.Text.Trim()))
            {
                if (txtgstin.Text.Length == 15)
                {
                    txtStateCode.Text = txtgstin.Text.Substring(0, 2);
                    txtpanno.Text = txtgstin.Text.Substring(2, 10);
                }
                else
                {
                    txtgstin.Text = "";
                    txtStateCode.Text = "";
                    txtpanno.Text = "";
                    obj.Information("Enter valid GSTIN No");
                }
            }
        }
    }
}
