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
    public partial class ConsumptionView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<ConsumptionMaster> _ListOfConsumptionMaster;
        ConsumptionMaster _ConsumptionMaster;
        public ConsumptionView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfConsumptionMaster = new BindingList<ConsumptionMaster>(_KPDB.ConsumptionMasters.Where(w => (w.MachineMaster.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.MachineMaster.LotNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.ConId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfConsumptionMaster;
            GrinEditDeleteDetailView.Columns["ConId"].Visible = false;
            GrinEditDeleteDetailView.Columns["MachinId"].Visible = false;
            GrinEditDeleteDetailView.Columns["MachineMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            this.GrinEditDeleteDetailView.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _ConsumptionMaster = new ConsumptionMaster();
            if (new ConsumptionAdd().Show(ConsumptionAdd.FormMode.Add, _ConsumptionMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfConsumptionMaster.Insert(0, _ConsumptionMaster);
                _ListOfConsumptionMaster.ResetBindings();
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
                    _ConsumptionMaster = _ListOfConsumptionMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ConsumptionMaster != null)
                    {
                        new ConsumptionAdd().Show(ConsumptionAdd.FormMode.Edit, _ConsumptionMaster, _KPDB);
                        _ListOfConsumptionMaster.ResetBindings();
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
                    _ConsumptionMaster = _ListOfConsumptionMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ConsumptionMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            ConsumptionMaster Conm = _KPDB.ConsumptionMasters.Where(w => w.ConId == _ConsumptionMaster.ConId).SingleOrDefault();
                            if (Conm.ConsumptionDetails.Where(w => w.IsDelete != true).Count() > 0)
                            {
                                foreach (ConsumptionDetail item in Conm.ConsumptionDetails.Where(w => w.IsDelete != true).ToList())
                                {
                                    ItemMaster Pd = item.ItemMaster;
                                    if (item.MMId != null)
                                    {
                                        item.MixingMaster.IssueWeight = item.MixingMaster.IssueWeight - item.Qty;
                                    }
                                    else if (item.RDId != null)
                                    {
                                        item.RewinderDetail.IsUse = false;

                                    }
                                    else
                                    {
                                        Pd.Qty = Pd.Qty + item.Qty;
                                    }
                                    item.IsDelete = true;
                                    item.DeleteDate = DateTime.Now;
                                }
                            }

                            Conm.IsDelete = true;
                            Conm.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfConsumptionMaster.Remove(Conm);
                            if (_ListOfConsumptionMaster.Count == 0)
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
