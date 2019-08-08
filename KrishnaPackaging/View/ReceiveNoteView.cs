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
    public partial class ReceiveNoteView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<ReceiveNoteMaster> _ListOfReceiveNote;
        ReceiveNoteMaster _ReceiveNoteMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");
        public ReceiveNoteView(KrishnaPackagingDbDataContext _kpdb)
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;
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
            _ListOfReceiveNote = new BindingList<ReceiveNoteMaster>(_KPDB.ReceiveNoteMasters.Where(w => (w.PartyMaster.PartyName.Contains(TSTextBoxSearch.Text.Trim()) || w.RNNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.RNMId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfReceiveNote;
            GrinEditDeleteDetailView.Columns["RNMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["PartyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["PartyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["Remarks"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["Inward"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            this.GrinEditDeleteDetailView.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            GrinEditDeleteDetailView.Columns["Amount"].DefaultCellStyle.Format = "C2";
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _ReceiveNoteMaster = new ReceiveNoteMaster();
            if (new ReceiveNoteAdd().Show(ReceiveNoteAdd.FormMode.Add, _ReceiveNoteMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfReceiveNote.Insert(0, _ReceiveNoteMaster);
                _ListOfReceiveNote.ResetBindings();
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
                    _ReceiveNoteMaster = _ListOfReceiveNote[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ReceiveNoteMaster != null)
                    {
                        new ReceiveNoteAdd().Show(ReceiveNoteAdd.FormMode.Edit, _ReceiveNoteMaster, _KPDB);
                        _ListOfReceiveNote.ResetBindings();
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
                    _ReceiveNoteMaster = _ListOfReceiveNote[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ReceiveNoteMaster != null)
                    {
                        //if (_ReceiveNoteMaster.ReceiveNoteDetails.Where(w => w.IsDelete != true).Count() > 0)
                        //{
                        //    obj.Information("Item is in used with ReceiveNote");
                        //}
                        //else
                        //{
                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            ReceiveNoteMaster Rm = _KPDB.ReceiveNoteMasters.Where(w => w.RNMId == _ReceiveNoteMaster.RNMId).SingleOrDefault();
                            Rm.IsDelete = true;
                            Rm.DeleteDate = DateTime.Now;
                            foreach (ReceiveNoteDetail item in Rm.ReceiveNoteDetails.Where(w=>w.IsDelete!=true).ToList())
                            {
                                ItemMaster Pd = item.ItemMaster;
                                Pd.Qty = Pd.Qty - item.Qty;
                                item.IsDelete = true;
                                item.DeleteDate = DateTime.Now;
                            }
                            _KPDB.SubmitChanges();
                            _ListOfReceiveNote.Remove(Rm);
                            if (_ListOfReceiveNote.Count == 0)
                                TSCancel.PerformClick();
                        }
                        // }

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
