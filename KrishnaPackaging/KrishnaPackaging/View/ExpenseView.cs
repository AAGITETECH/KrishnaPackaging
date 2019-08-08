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
    public partial class ExpenseView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<ExpenseMaster> _ListOfItem;
        ExpenseMaster _ExpenseMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");
        public ExpenseView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfItem = new BindingList<ExpenseMaster>(_KPDB.ExpenseMasters.Where(w => (w.ExpenseNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.EMId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfItem;
            GrinEditDeleteDetailView.Columns["EMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["TotalExpense"].DisplayIndex = 10;
            this.GrinEditDeleteDetailView.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            GrinEditDeleteDetailView.Columns["Salary"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Transportation"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Banking"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Power"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Fuel"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Others"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Admin"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["TotalExpense"].DefaultCellStyle.Format = "C2";

            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _ExpenseMaster = new ExpenseMaster();
            if (new ExpenseAdd().Show(ExpenseAdd.FormMode.Add, _ExpenseMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfItem.Insert(0, _ExpenseMaster);
                _ListOfItem.ResetBindings();
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
                    _ExpenseMaster = _ListOfItem[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ExpenseMaster.ExpenseDetails.Where(w => w.MachineMaster.IsClose == true).Count() > 0)
                    {
                        TSEdit.Enabled = false;
                    }
                    else
                    {
                        TSEdit.Enabled = true;
                        if (_ExpenseMaster != null)
                        {
                            new ExpenseAdd().Show(ExpenseAdd.FormMode.Edit, _ExpenseMaster, _KPDB);
                            _ListOfItem.ResetBindings();
                        }
                        else
                            obj.Information("Sorry No Record Found.");
                    }
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
                    _ExpenseMaster = _ListOfItem[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ExpenseMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            ExpenseMaster Im = _KPDB.ExpenseMasters.Where(w => w.EMId == _ExpenseMaster.EMId).SingleOrDefault();
                            Im.IsDelete = true;
                            Im.DeleteDate = DateTime.Now;
                            Im.ExpenseDetails.ToList().ForEach(s => s.IsDelete = true);
                            _KPDB.SubmitChanges();
                            _ListOfItem.Remove(Im);
                            if (_ListOfItem.Count == 0)
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
