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
    public partial class UserView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<UserMaster> _ListOfUser;
        UserMaster _UserMaster;
        public UserView(KrishnaPackagingDbDataContext _kpdb)
        {
            InitializeComponent();
            _KPDB = _kpdb;
            BindDataGrid();
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

        private void BindDataGrid()
        {
            _ListOfUser = new BindingList<UserMaster>(_KPDB.UserMasters.Where(w => (w.UserName.Contains(TSTextBoxSearch.Text.Trim())) && w.Type != "admin" && w.CompanyId == Program.CompanyId && w.IsDelete != true).OrderByDescending(o => o.UserId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfUser;
            GrinEditDeleteDetailView.Columns["UserId"].Visible = false;
            GrinEditDeleteDetailView.Columns["ContactNo"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["Type"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;

            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _UserMaster = new UserMaster();
            if (new UserAdd().Show(UserAdd.FormMode.Add, _UserMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfUser.Insert(0, _UserMaster);
                _ListOfUser.ResetBindings();
                GrinEditDeleteDetailView.Refresh();
            }
            else
                TSCancel.PerformClick();
        }

        private void TSEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _UserMaster = _ListOfUser[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_UserMaster != null)
                    {
                        new UserAdd().Show(UserAdd.FormMode.Edit, _UserMaster, _KPDB);
                        _ListOfUser.ResetBindings();
                    }
                    else
                        obj.Information("Sorry No Record Found.");
                }
                else
                    obj.Information("Please Select Row To Edit");
            }
            catch (Exception _Exception)
            {
                obj.Error(_Exception.Message);
                Application.ExitThread();
            }
        }

        private void TSDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _UserMaster = _ListOfUser[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_UserMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            UserMaster Um = _KPDB.UserMasters.Where(w => w.UserId == _UserMaster.UserId).SingleOrDefault();
                            Um.IsDelete = true;
                            Um.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfUser.Remove(Um);
                            if (_ListOfUser.Count == 0)
                                TSCancel.PerformClick();
                        }


                    }
                    else
                        obj.Information("Sorry No Record Found");
                }
                else
                    obj.Information("Please Select Row To Delete");
            }
            catch (Exception _Exception)
            {
                obj.Error(_Exception.Message);
                Application.ExitThread();
            }
        }

        private void TSCancel_Click(object sender, EventArgs e)
        {
            TSDelete.Enabled = false;
            TSEdit.Enabled = false;
            if (!String.IsNullOrEmpty(TSTextBoxSearch.Text))
            {
                TSTextBoxSearch.Text = "";
                BindDataGrid();
            }
        }

        private void TSTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        private void GrinEditDeleteDetailView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GrinEditDeleteDetailView.Rows.Count)
                TSEdit.PerformClick();
        }

        private void GrinEditDeleteDetailView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TSEdit.Enabled = true;
            TSDelete.Enabled = true;
        }
    }
}
