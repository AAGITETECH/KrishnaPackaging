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
    public partial class MachineView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<MachineMaster> _ListOfMachineMaster;
        MachineMaster _MachineMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");

        public MachineView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfMachineMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.LotNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId && w.IsClose == false).OrderByDescending(o => o.MachinId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfMachineMaster;
            GrinEditDeleteDetailView.Columns["MachinId"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsClose"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsExpense"].Visible = false;
            GrinEditDeleteDetailView.Columns["Machine"].DisplayIndex = 0;
            GrinEditDeleteDetailView.Columns["Date"].DisplayIndex = 1;
            GrinEditDeleteDetailView.Columns["LotNo"].DisplayIndex = 2;
            GrinEditDeleteDetailView.Columns["DegreeInnerDiameter"].DisplayIndex = 3;
            GrinEditDeleteDetailView.Columns["Size"].DisplayIndex = 4;
            GrinEditDeleteDetailView.Columns["HeightLength"].DisplayIndex = 5;
            GrinEditDeleteDetailView.Columns["CSType"].DisplayIndex = 6;
            GrinEditDeleteDetailView.Columns["DesignThickness"].DisplayIndex = 7;
            GrinEditDeleteDetailView.Columns["Weight"].DisplayIndex = 8;
            GrinEditDeleteDetailView.Columns["Stock"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Costing"].DefaultCellStyle.Format = "C2";
            GrinEditDeleteDetailView.Columns["Amount"].DefaultCellStyle.Format = "C2";

            this.GrinEditDeleteDetailView.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _MachineMaster = new MachineMaster();
            if (new MachineAdd().Show(MachineAdd.FormMode.Add, _MachineMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfMachineMaster.Insert(0, _MachineMaster);
                _ListOfMachineMaster.ResetBindings();
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
                    _MachineMaster = _ListOfMachineMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_MachineMaster != null)
                    {
                        new MachineAdd().Show(MachineAdd.FormMode.Edit, _MachineMaster, _KPDB);
                        _ListOfMachineMaster.ResetBindings();
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
                    _MachineMaster = _ListOfMachineMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_MachineMaster != null)
                    {
                        //if (_MachineMaster.ReceiveNoteDetails.Where(w => w.IsDelete != true).Count() > 0)
                        //{
                        //    obj.Information("Item is in used with ReceiveNote");
                        //}
                        //else
                        //{
                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            MachineMaster Im = _KPDB.MachineMasters.Where(w => w.MachinId == _MachineMaster.MachinId).SingleOrDefault();
                            Im.IsDelete = true;
                            Im.DeleteDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfMachineMaster.Remove(Im);
                            if (_ListOfMachineMaster.Count == 0)
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
        private void TSbtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _MachineMaster = _ListOfMachineMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_MachineMaster != null)
                    {
                        //if (_MachineMaster.ReceiveNoteDetails.Where(w => w.IsDelete != true).Count() > 0)
                        //{
                        //    obj.Information("Item is in used with ReceiveNote");
                        //}
                        //else
                        //{
                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            MachineMaster Im = _KPDB.MachineMasters.Where(w => w.MachinId == _MachineMaster.MachinId).SingleOrDefault();
                            Im.IsClose = true;
                            _KPDB.SubmitChanges();
                            _ListOfMachineMaster.Remove(Im);
                            if (_ListOfMachineMaster.Count == 0)
                                TSCancel.PerformClick();
                        }
                        // }

                    }
                    else
                        obj.Information("Sorry No Record Found");
                }
                else
                    obj.Information("Please Select Row To Close");
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
