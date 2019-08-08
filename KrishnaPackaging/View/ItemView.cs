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
    public partial class ItemView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<ItemMaster> _ListOfItem;
        ItemMaster _ItemMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");
        public ItemView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfItem = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => (w.Name.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderByDescending(o => o.IMId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfItem;
            GrinEditDeleteDetailView.Columns["IMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.Format = "C2";

            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _ItemMaster = new ItemMaster();
            if (new ItemAdd().Show(ItemAdd.FormMode.Add, _ItemMaster, _KPDB) == DialogResult.OK)
            {
                TSCancel.PerformClick();
                _ListOfItem.Insert(0, _ItemMaster);
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
                    _ItemMaster = _ListOfItem[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ItemMaster != null)
                    {
                        new ItemAdd().Show(ItemAdd.FormMode.Edit, _ItemMaster, _KPDB);
                        _ListOfItem.ResetBindings();
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
                    _ItemMaster = _ListOfItem[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_ItemMaster != null)
                    {
                        if (_ItemMaster.ReceiveNoteDetails.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            obj.Information("Item is in used with ReceiveNote");
                        }
                        else
                        {
                            if (obj.Question("Are you sure ?") == DialogResult.Yes)
                            {
                                ItemMaster Im = _KPDB.ItemMasters.Where(w => w.IMId == _ItemMaster.IMId).SingleOrDefault();
                                Im.IsDelete = true;
                                Im.DeleteDate = DateTime.Now;
                                _KPDB.SubmitChanges();
                                _ListOfItem.Remove(Im);
                                if (_ListOfItem.Count == 0)
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
