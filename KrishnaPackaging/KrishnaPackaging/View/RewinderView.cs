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
    public partial class RewinderView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<RewinderMaster> _ListOfRewinder;
        RewinderMaster _RewinderMaster;
        public RewinderView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfRewinder = new BindingList<RewinderMaster>(_KPDB.RewinderMasters.Where(w => (w.InwardMaster.InwNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.RewId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfRewinder;
            GrinEditDeleteDetailView.Columns["RewId"].Visible = false;
            GrinEditDeleteDetailView.Columns["InwId"].Visible = false;
            GrinEditDeleteDetailView.Columns["InwardMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["Size"].Visible = false;
            GrinEditDeleteDetailView.Columns["RewindeSize"].Visible = false;
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _RewinderMaster = new RewinderMaster();
            if (new RewinderAdd().Show(RewinderAdd.FormMode.Add, _RewinderMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfRewinder.Insert(0, _RewinderMaster);
                _ListOfRewinder.ResetBindings();
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
                    _RewinderMaster = _ListOfRewinder[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_RewinderMaster != null)
                    {
                        new RewinderAdd().Show(RewinderAdd.FormMode.Edit, _RewinderMaster, _KPDB);
                        _ListOfRewinder.ResetBindings();
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
                    _RewinderMaster = _ListOfRewinder[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_RewinderMaster != null)
                    {
                        //if (_RewinderMaster.ReceiveNoteDetails.Where(w => w.IsDelete != true).Count() > 0)
                        //{
                        //    obj.Information("Item is in used with ReceiveNote");
                        //}
                        //else
                        //{
                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            RewinderMaster Im = _KPDB.RewinderMasters.Where(w => w.RewId == _RewinderMaster.RewId).SingleOrDefault();
                            Im.InwardMaster.IsRew = false;
                            Im.RewinderDetails.ToList().ForEach(f => f.IsDelete = true);
                            Im.IsDelete = true;
                            Im.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfRewinder.Remove(Im);
                            if (_ListOfRewinder.Count == 0)
                                TSCancel.PerformClick();
                        }
                        //}

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

        private void GrinEditDeleteDetailView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GrinEditDeleteDetailView.Rows.Count)
                TSEdit.PerformClick();
        }

        private void GrinEditDeleteDetailView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TSEdit.Enabled = true;
            TSDelete.Enabled = true;
        }
    }
}
