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
    public partial class DispatchView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<DispatchMaster> _ListOfDispatchMaster;
        DispatchMaster _DispatchMaster;
        public DispatchView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfDispatchMaster = new BindingList<DispatchMaster>(_KPDB.DispatchMasters.Where(w => (w.PartyMaster.PartyName.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.DMId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfDispatchMaster;
            GrinEditDeleteDetailView.Columns["PartyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["DMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["PartyMaster"].Visible = false;
            this.GrinEditDeleteDetailView.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            this.GrinEditDeleteDetailView.Columns["PartyPODate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _DispatchMaster = new DispatchMaster();
            if (new DispatchAdd().Show(DispatchAdd.FormMode.Add, _DispatchMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfDispatchMaster.Insert(0, _DispatchMaster);
                _ListOfDispatchMaster.ResetBindings();
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
                    _DispatchMaster = _ListOfDispatchMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_DispatchMaster != null)
                    {
                        new DispatchAdd().Show(DispatchAdd.FormMode.Edit, _DispatchMaster, _KPDB);
                        _ListOfDispatchMaster.ResetBindings();
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
                    _DispatchMaster = _ListOfDispatchMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_DispatchMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            DispatchMaster Conm = _KPDB.DispatchMasters.Where(w => w.DMId == _DispatchMaster.DMId).SingleOrDefault();
                            if (Conm.DispatchDetails.Where(w => w.IsDelete != true).Count() > 0)
                            {
                                Conm.DispatchDetails.Where(w => w.IsDelete != true).ToList().ForEach(f =>
                                {
                                    f.IsDelete = true;
                                    f.DeleteDate = DateTime.Now;
                                });
                            }
                            Conm.IsDelete = true;
                            Conm.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfDispatchMaster.Remove(Conm);
                            if (_ListOfDispatchMaster.Count == 0)
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
