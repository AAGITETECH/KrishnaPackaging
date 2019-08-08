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
    public partial class MixingView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<MixingMaster> _ListOfMixingMaster;
        MixingMaster _MixingMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");

        public MixingView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfMixingMaster = new BindingList<MixingMaster>(_KPDB.MixingMasters.Where(w => (w.ItemMaster.Name.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.MMId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfMixingMaster;
            GrinEditDeleteDetailView.Columns["MMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["InwId"].Visible = false;
            GrinEditDeleteDetailView.Columns["IMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["ItemMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["InwardMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["IssueQty"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["MixingWater"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["FinisheQty"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["IssueWeight"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.Format = "C2";
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _MixingMaster = new MixingMaster();
            if (new MixingAdd().Show(MixingAdd.FormMode.Add, _MixingMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfMixingMaster.Insert(0, _MixingMaster);
                _ListOfMixingMaster.ResetBindings();
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
                    _MixingMaster = _ListOfMixingMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_MixingMaster != null)
                    {
                        new MixingAdd().Show(MixingAdd.FormMode.Edit, _MixingMaster, _KPDB);
                        _ListOfMixingMaster.ResetBindings();
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
                    _MixingMaster = _ListOfMixingMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_MixingMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            MixingMaster Im = _KPDB.MixingMasters.Where(w => w.MMId == _MixingMaster.MMId).SingleOrDefault();
                            Im.IsDelete = true;
                            Im.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfMixingMaster.Remove(Im);
                            if (_ListOfMixingMaster.Count == 0)
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
