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
    public partial class PasswordReset : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        UserMaster _UserMaster;
        public PasswordReset(KrishnaPackagingDbDataContext _kpdb)
        {
            InitializeComponent();
            _KPDB = _kpdb;
            _UserMaster = _KPDB.UserMasters.Where(w => w.UserId == Program.UserId && w.IsDelete != true).SingleOrDefault();
            txtuser.Text = _UserMaster.UserName;
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

        bool CanReset()
        {
            if (string.IsNullOrEmpty(txtoldPass.Text))
            {
                obj.Information("Enter OldPassword");
                return false;
            }
            else if (txtoldPass.Text.Trim() != _UserMaster.Password)
            {
                obj.Information("Incorrect Oldpassword.");
                txtoldPass.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtNewPass.Text))
            {
                obj.Information("Enter NewPassword");
                return false;
            }
            else if (string.IsNullOrEmpty(txtCNewPass.Text))
            {
                obj.Information("Enter ConfirmPassword");
                return false;
            }
            else if (txtNewPass.Text != txtCNewPass.Text)
            {
                obj.Information("NewPassword and ConfirmPassword Not Match.");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CanReset())
            {
                _UserMaster.Password = txtNewPass.Text;
                _KPDB.SubmitChanges();
                txtoldPass.Text = "";
                txtNewPass.Text = "";
                txtCNewPass.Text = "";
                obj.Information("PasswordReset successfully...");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
