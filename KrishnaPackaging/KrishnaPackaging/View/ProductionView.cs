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
    public partial class ProductionView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<ProductionMaster> _ListOfProductionMaster;
        ProductionMaster _ProductionMaster;
        public ProductionView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfProductionMaster = new BindingList<ProductionMaster>(_KPDB.ProductionMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.ProId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfProductionMaster;
            GrinEditDeleteDetailView.Columns["ProId"].Visible = false;
            GrinEditDeleteDetailView.Columns["Remarks"].Visible = false;
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
            _ProductionMaster = new ProductionMaster();
            if (new ProductionAdd().Show(ProductionAdd.FormMode.Add, _ProductionMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfProductionMaster.Insert(0, _ProductionMaster);
                _ListOfProductionMaster.ResetBindings();
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
                    _ProductionMaster = _ListOfProductionMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ProductionMaster != null)
                    {
                        new ProductionAdd().Show(ProductionAdd.FormMode.Edit, _ProductionMaster, _KPDB);
                        _ListOfProductionMaster.ResetBindings();
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
                    _ProductionMaster = _ListOfProductionMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ProductionMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            ProductionMaster Conm = _KPDB.ProductionMasters.Where(w => w.ProId == _ProductionMaster.ProId).SingleOrDefault();
                            if (Conm.ProductionDetails.Where(w => w.IsDelete != true).Count() > 0)
                            {
                                Conm.ProductionDetails.Where(w => w.IsDelete != true).ToList().ForEach(f =>
                                {
                                    f.IsDelete = true;
                                    f.DeleteDate = DateTime.Now;
                                });
                            }
                            Conm.IsDelete = true;
                            Conm.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfProductionMaster.Remove(Conm);
                            if (_ListOfProductionMaster.Count == 0)
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
