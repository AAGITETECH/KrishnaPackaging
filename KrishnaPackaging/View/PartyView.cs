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
    public partial class PartyView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<PartyMaster> _ListOfParty;
        PartyMaster _PartyMaster;
        public PartyView(KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            InitializeComponent();
            BindPartyMasterDataGrid();
        }

        private void BindPartyMasterDataGrid()
        {
            _ListOfParty = new BindingList<PartyMaster>(_KPDB.PartyMasters.Where(w => (w.PartyName.Contains(TSTextBoxSearch.Text.Trim()) || w.BillingName.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.PartyId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfParty;

            GrinEditDeleteDetailView.Columns["PartyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["Website"].Visible = false;
            GrinEditDeleteDetailView.Columns["Country"].Visible = false;
            //GrinEditDeleteDetailView.Columns["PartyName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }


        private void TSAdd_Click(object sender, EventArgs e)
        {
            _PartyMaster = new PartyMaster();
            if (new PartyAdd().Show(PartyAdd.FormMode.Add, _PartyMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfParty.Insert(0, _PartyMaster);
                _ListOfParty.ResetBindings();
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
                    _PartyMaster = _ListOfParty[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_PartyMaster != null)
                    {
                        new PartyAdd().Show(PartyAdd.FormMode.Edit, _PartyMaster, _KPDB);
                        _ListOfParty.ResetBindings();
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

        private void TSCancel_Click(object sender, EventArgs e)
        {
            TSDelete.Enabled = false;
            TSEdit.Enabled = false;
            if (!String.IsNullOrEmpty(TSTextBoxSearch.Text))
            {
                TSTextBoxSearch.Text = "";
                BindPartyMasterDataGrid();
            }
        }

        private void TSGo_Click(object sender, EventArgs e)
        {
        }

        private void GrinEditDeleteDetailView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TSEdit.Enabled = true;
            TSDelete.Enabled = true;
        }

        private void GrinEditDeleteDetailView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex <= GrinEditDeleteDetailView.Rows.Count)
                TSEdit.PerformClick();
        }

        private void PartyView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Cursor.Current = Cursors.WaitCursor;
                TSEdit.Enabled = false;
                TSDelete.Enabled = false;
                TSTextBoxSearch.Text = "";
                BindPartyMasterDataGrid();
                Cursor.Current = Cursors.Default;
            }
        }

        private void TSDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _PartyMaster = _ListOfParty[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_PartyMaster != null)
                    {
                        if (_PartyMaster.ReceiveNoteMasters.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            obj.Information("Party is in used with ReceiveNote");
                        }
                        else
                        {
                            if (obj.Question("Are you sure ?") == DialogResult.Yes)
                            {
                                PartyMaster Pm = _KPDB.PartyMasters.Where(w => w.PartyId == _PartyMaster.PartyId).SingleOrDefault();
                                Pm.IsDelete = true;
                                Pm.DeleteDate = DateTime.Now;
                                _KPDB.SubmitChanges();
                                _ListOfParty.Remove(Pm);
                                if (_ListOfParty.Count == 0)
                                    TSCancel.PerformClick();
                            }
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

        private void TSTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            BindPartyMasterDataGrid();
        }
    }
}
