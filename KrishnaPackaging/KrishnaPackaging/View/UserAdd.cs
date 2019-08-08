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
    public partial class UserAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        UserMaster _UserMaster;
        BindingList<FormMaster> _ListOfFormMaster;
        public UserAdd()
        {
            InitializeComponent();
        }

        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, UserMaster _userMaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add User";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _UserMaster = _userMaster;
                FillForm();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit User";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _UserMaster = _userMaster;
                FillRecord(_UserMaster);
                FillForm();
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

        void FillForm()
        {
            _ListOfFormMaster = null;
            _ListOfFormMaster = new BindingList<FormMaster>(_KPDB.FormMasters.ToList());
            _ListOfFormMaster.ToList().ForEach(f => f.IsDisplay = false);
            if (_UserMaster.UserId != 0)
            {
                foreach (UserPermission item in _UserMaster.UserPermissions)
                {
                    _ListOfFormMaster.Where(w => w.FormId == item.FormId).All(a => a.IsDisplay = true);
                }

            }
            GridPermission.DataSource = null;
            GridPermission.DataSource = _ListOfFormMaster;
            GridPermission.Columns["FormId"].Visible = false;
            GridPermission.Columns["MenuName"].Visible = false;
            GridPermission.Columns["FormName"].DisplayIndex = 0;
            GridPermission.Columns["FormName"].ReadOnly = true;

        }

        void FillRecord(UserMaster _UserMaster)
        {
            try
            {
                txtUserName.Text = _UserMaster.UserName;
              
                txtPassword.Text = _UserMaster.Password;
                

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool Cansave()
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                obj.Information("Enter UserName");
                txtUserName.Focus();
                return false;
            }
            else if (_UserMaster.UserId == 0 && _KPDB.UserMasters.Where(w => w.IsDelete != true && w.UserName == txtUserName.Text.Trim()).ToList().Count() > 0)
            {
                obj.Information("UserName already exists.");
                txtUserName.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                obj.Information("Enter Password");
                txtPassword.Focus();
                return false;
            }
            else if (_ListOfFormMaster.Where(w => w.IsDisplay == true).Count() == 0)
            {
                obj.Information("Set Minimum One Permission.");
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
                    _UserMaster.UserName = txtUserName.Text;
                    _UserMaster.Password = txtPassword.Text;
                    _UserMaster.Type = "User";
                    if (_UserMaster.UserId != 0)
                    {
                        List<UserPermission> _listUP = new List<UserPermission>(_KPDB.UserPermissions.Where(w => w.UserId == _UserMaster.UserId).ToList());
                        _KPDB.UserPermissions.DeleteAllOnSubmit(_listUP);
                    }
                    foreach (FormMaster item in _ListOfFormMaster)
                    {
                        if (item.IsDisplay == true)
                        {
                            UserPermission _UP = new UserPermission();
                            _UP.FormMaster = item;
                            _UP.UserMaster = _UserMaster;
                            _KPDB.UserPermissions.InsertOnSubmit(_UP);
                        }
                    }
                    if (this.formMode == FormMode.Add)
                    {
                        _UserMaster.CompanyId = Program.CompanyId;
                        _UserMaster.AddDate = DateTime.Now;
                        _UserMaster.EditDate = null;
                        _UserMaster.IsDelete = false;
                        _UserMaster.DeleteDate = null;
                        _KPDB.UserMasters.InsertOnSubmit(_UserMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _UserMaster.EditDate = DateTime.Now;
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

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (_ListOfFormMaster != null)
            //{
            //    if (CmbType.SelectedItem.ToString() == "Admin")
            //    {
            //        _ListOfFormMaster.ToList().ForEach(f => f.IsDisplay = true);
            //    }
            //    else
            //    {
            //        _ListOfFormMaster.ToList().ForEach(f => f.IsDisplay = false);

            //    }
            //}
        }
    }
}
