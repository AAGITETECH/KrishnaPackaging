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
    public partial class InwardView : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        BindingList<InwardMaster> _ListOfInwardMaster;
        InwardMaster _InwardMaster;
        public InwardView(KrishnaPackagingDbDataContext _kpdb)
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
            _ListOfInwardMaster = new BindingList<InwardMaster>(_KPDB.InwardMasters.Where(w => (w.ReceiveNoteMaster.PartyMaster.PartyName.Contains(TSTextBoxSearch.Text.Trim()) || w.ReceiveNoteMaster.RNNo.Contains(TSTextBoxSearch.Text.Trim()) || w.InwNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId && w.IsClose != true).OrderByDescending(o => o.InwId).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfInwardMaster;
            GrinEditDeleteDetailView.Columns["InwId"].Visible = false;
            GrinEditDeleteDetailView.Columns["RNMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["IMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["ReceiveNoteMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["ItemMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsClose"].Visible = false;
            GrinEditDeleteDetailView.Columns["CloseDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["Stock"].Visible = false;
            GrinEditDeleteDetailView.Columns["StockWeight"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsRew"].Visible = false;
            GrinEditDeleteDetailView.Columns["SizeUnit"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Weight"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Size"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Stock"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["StockWeight"].DefaultCellStyle.Format = "0.00##";

            TSEdit.Enabled = false;
            TSDelete.Enabled = false;
        }

        private void TSAdd_Click(object sender, EventArgs e)
        {
            _InwardMaster = new InwardMaster();
            if (new InwardAdd().Show(InwardAdd.FormMode.Add, _InwardMaster, _KPDB) == DialogResult.OK)
            {
                //TSCancel.PerformClick();
                //_ListOfInwardMaster.Insert(0, _InwardMaster);
                //_ListOfInwardMaster.ResetBindings();
                //GrinEditDeleteDetailView.Refresh();
            }
            else
                TSCancel.PerformClick();
            BindDataGrid();
        }

        private void TSEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _InwardMaster = _ListOfInwardMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_InwardMaster != null)
                    {
                        if (_InwardMaster.RewinderMasters.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            TSEdit.Enabled = false;
                        }
                        else if (_InwardMaster.MixingMasters.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            TSEdit.Enabled = false;
                        }
                        else
                        {
                            new InwardAdd().Show(InwardAdd.FormMode.Edit, _InwardMaster, _KPDB);
                            _ListOfInwardMaster.ResetBindings();
                        }
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
                    _InwardMaster = _ListOfInwardMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_InwardMaster != null)
                    {
                        if (_InwardMaster.RewinderMasters.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            obj.Information("Inward is in used with Rewinder");
                        }
                        else if (_InwardMaster.MixingMasters.Where(w => w.IsDelete != true).Count() > 0)
                        {
                            obj.Information("Inward is in used with Mixing");
                        }
                        else
                        {
                            if (obj.Question("Are you sure ?") == DialogResult.Yes)
                            {
                                InwardMaster Im = _KPDB.InwardMasters.Where(w => w.InwId == _InwardMaster.InwId).SingleOrDefault();
                                Im.IsDelete = true;
                                Im.DeleteDate = DateTime.Now;
                                ReceiveNoteDetail _Rm = _KPDB.ReceiveNoteDetails.Where(w => w.IMId == Convert.ToInt64(_InwardMaster.IMId) && w.IsDelete != true).SingleOrDefault();
                                if (_Rm != null)
                                {
                                    _Rm.InwardQty = _Rm.InwardQty - Im.Qty;
                                    if (_Rm.Qty != _Rm.InwardQty)
                                    {
                                        _Rm.IsInward = false;
                                    }
                                }
                                _KPDB.SubmitChanges();
                                _ListOfInwardMaster.Remove(Im);
                                if (_ListOfInwardMaster.Count == 0)
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

        private void TSbtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrinEditDeleteDetailView.CurrentRow != null)
                {
                    _InwardMaster = _ListOfInwardMaster[GrinEditDeleteDetailView.CurrentRow.Index];
                    if (_InwardMaster != null)
                    {

                        if (obj.Question("Are you sure ?") == DialogResult.Yes)
                        {
                            InwardMaster Im = _KPDB.InwardMasters.Where(w => w.InwId == _InwardMaster.InwId).SingleOrDefault();
                            Im.IsClose = true;
                            Im.CloseDate = DateTime.Now;
                            _KPDB.SubmitChanges();
                            _ListOfInwardMaster.Remove(Im);
                            if (_ListOfInwardMaster.Count == 0)
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
    }
}
