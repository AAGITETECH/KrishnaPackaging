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
using System.Drawing;
using KrishnaPackaging.Report;
using System.Globalization;
using System.Threading;

namespace KrishnaPackaging.View
{
    public partial class StockView : Form
    {
        KrishnaPackagingDbDataContext _KPDB = new KrishnaPackagingDbDataContext();
        GenC obj = new GenC();
        BindingList<ItemMaster> _ListOfItem;
        ItemMaster _ItemMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");
        public StockView(KrishnaPackagingDbDataContext _kpdb)
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;
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

            var RawM = _KPDB.ItemMasters.Where(w => w.Name.Contains(TSTextBoxSearch.Text.Trim()) && w.IsDelete != true && w.CompanyId == Program.CompanyId && w.Type == "Raw Material").ToList();
            var d = RawM.Select(s => new
            {
                s.Name,
                s.ProcessType,
                Qty = s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Qty) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.Stock)),
                Weight = s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Weight) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.StockWeight)),
                s.UOM,
                s.Rate,
                Amount = (s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Weight) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.StockWeight))) * s.Rate,
                s.IMId,
            }).ToList();
            RWdataGridView.DataSource = null;
            RWdataGridView.DataSource = d.Where(w => w.Qty != 0).ToList();
            RWdataGridView.Columns["IMId"].Visible = false;
            RWdataGridView.Columns["Qty"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#39ac73");
            RWdataGridView.Columns["Qty"].DefaultCellStyle.ForeColor = Color.White;
            RWdataGridView.Columns["Qty"].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            RWdataGridView.Columns["Rate"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#29a3a3");
            RWdataGridView.Columns["Rate"].DefaultCellStyle.ForeColor = Color.White;
            RWdataGridView.Columns["Rate"].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            RWdataGridView.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            RWdataGridView.Columns["Weight"].DefaultCellStyle.Format = "0.000###";
            RWdataGridView.Columns["Rate"].DefaultCellStyle.Format = "C2";
            RWdataGridView.Columns["Amount"].DefaultCellStyle.Format = "C2";

            _ListOfItem = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => (w.Name.Contains(TSTextBoxSearch.Text.Trim())) && w.Type != "Raw Material" && w.IsDelete != true && w.CompanyId == Program.CompanyId).OrderBy(o => o.Name).ToList());
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = _ListOfItem.Where(w => w.Qty != 0).ToList();
            GrinEditDeleteDetailView.Columns["IMId"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            GrinEditDeleteDetailView.Columns["ProcessType"].Visible = false;
            GrinEditDeleteDetailView.Columns["InwardType"].Visible = false;
            GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#39ac73");
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.ForeColor = Color.White;
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#29a3a3");
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.ForeColor = Color.White;
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.Format = "0.00##";
            GrinEditDeleteDetailView.Columns["Rate"].DefaultCellStyle.Format = "C2";

            var Production = _KPDB.MachineMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.LotNo.Contains(TSTextBoxSearch.Text.Trim()) || w.DegreeInnerDiameter.Contains(TSTextBoxSearch.Text.Trim()) || w.Size.Contains(TSTextBoxSearch.Text.Trim()) || w.HeightLength.Contains(TSTextBoxSearch.Text.Trim()) || w.CSType.Contains(TSTextBoxSearch.Text.Trim()) || w.DesignThickness.Contains(TSTextBoxSearch.Text.Trim()) || w.Weight.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList();
            GridProduction.DataSource = null;
            GridProduction.DataSource = Production.Where(w => w.Stock != 0).ToList();
            GridProduction.Columns["MachinId"].Visible = false;
            GridProduction.Columns["Date"].Visible = false;

            GridProduction.Columns["DegreeInnerDiameter"].Visible = false;

            GridProduction.Columns["CompanyId"].Visible = false;
            GridProduction.Columns["AddDate"].Visible = false;
            GridProduction.Columns["EditDate"].Visible = false;
            GridProduction.Columns["IsDelete"].Visible = false;
            GridProduction.Columns["DeleteDate"].Visible = false;
            GridProduction.Columns["CompanyMaster"].Visible = false;
            GridProduction.Columns["Machine"].DisplayIndex = 0;
            GridProduction.Columns["LotNo"].DisplayIndex = 1;
            GridProduction.Columns["Size"].DisplayIndex = 2;
            GridProduction.Columns["HeightLength"].DisplayIndex = 3;
            GridProduction.Columns["CSType"].DisplayIndex = 4;
            GridProduction.Columns["DesignThickness"].DisplayIndex = 5;
            GridProduction.Columns["Weight"].DisplayIndex = 6;
            GridProduction.Columns["Costing"].DisplayIndex = 7;
            GridProduction.Columns["Amount"].DisplayIndex = 8;
            GridProduction.Columns["IsClose"].Visible = false;
            GridProduction.Columns["IsExpense"].Visible = false;
            GridProduction.Columns["Amount"].DefaultCellStyle.Format = "C2";
            GridProduction.Columns["Costing"].DefaultCellStyle.Format = "C2";
            tabControl_SelectedIndexChanged(null, null);
        }


        private void TSTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            BindDataGrid();
        }

        private void GrinEditDeleteDetailView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {

                if (e.RowIndex >= 0 && e.RowIndex <= RWdataGridView.Rows.Count)
                {
                    var value = RWdataGridView.Rows[e.RowIndex].Cells["IMId"].Value;
                    string Type = RWdataGridView.Rows[e.RowIndex].Cells["ProcessType"].Value.ToString();
                    var RawM = _KPDB.ItemMasters.Where(w => w.IMId == Convert.ToInt32(value) && w.IsDelete != true && w.CompanyId == Program.CompanyId).SingleOrDefault();
                    if (Type == "Re-windable")
                    {
                        var Data = RawM.InwardMasters.AsEnumerable().Where(w => w.IsDelete != true && w.IsRew != true).Select(s => new
                        {
                            s.ItemMaster.Name,
                            InwardNo = s.InwNo,
                            s.Qty,
                            s.Weight,
                            s.Size,
                            s.SizeUnit,
                        }).ToList();
                        decimal Qty = Convert.ToDecimal(RawM.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Qty));
                        RawMaterlalView _REV = new RawMaterlalView(Data, Qty.ToString("0.00##"), RawM.Name);
                        _REV.Show();
                    }
                    else
                    {
                        var Data = RawM.InwardMasters.AsEnumerable().Where(w => w.IsDelete != true && w.IsRew != true).Select(s => new
                        {
                            s.ItemMaster.Name,
                            InwardNo = s.InwNo,
                            Qty = decimal.Round(s.Stock, 2),
                            Weight = decimal.Round(Convert.ToDecimal(s.Weight), 2),
                            Size = decimal.Round(Convert.ToDecimal(s.Size), 2),
                            s.SizeUnit,
                        }).ToList();
                        decimal Qty = Convert.ToDecimal(RawM.InwardMasters.Where(w => w.IsDelete != true).Sum(a => a.Stock));
                        RawMaterlalView _REV = new RawMaterlalView(Data, Qty.ToString("0.00##"), RawM.Name);
                        _REV.Show();
                    }
                }
            }
            else if (tabControl.SelectedIndex == 2)
            {
                if (e.RowIndex >= 0 && e.RowIndex <= GridProduction.Rows.Count)
                {
                    var Id = GridProduction.Rows[e.RowIndex].Cells["MachinId"].Value;
                    var LotNo = GridProduction.Rows[e.RowIndex].Cells["LotNo"].Value;
                    var Machin = GridProduction.Rows[e.RowIndex].Cells["Machine"].Value;
                    decimal Produc = Convert.ToDecimal(GridProduction.Rows[e.RowIndex].Cells["Stock"].Value);
                    var Data = _KPDB.MachineMasters.Where(w => w.MachinId == Convert.ToInt32(Id) && w.IsDelete != true && w.CompanyId == Program.CompanyId).Select(s => new
                    {
                        s.Machine,
                        s.LotNo,
                        s.DegreeInnerDiameter,
                        s.Size,
                        s.HeightLength,
                        s.CSType,
                        s.DesignThickness,
                        s.Weight,
                    }).ToList();
                    RawMaterlalView _REV = new RawMaterlalView(Data, Produc.ToString("0.00##"), Machin + " (" + LotNo + ")");
                    _REV.Show();
                }
            }

        }

        private void GrinEditDeleteDetailView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TSPrint_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                var RawM = _KPDB.InwardMasters.Where(w => w.ItemMaster.Type == "Raw Material" && w.ItemMaster.IsDelete != true && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList();

                var Data = RawM.AsEnumerable().Where(w => w.IsDelete != true && w.IsRew != true).Select(s => new
                {
                    s.ItemMaster.Name,
                    InwardNo = s.InwNo,
                    Qty = s.Stock,
                    s.Weight,
                    s.Size,
                    s.SizeUnit,
                    s.ItemMaster.ProcessType,
                }).ToList();
                StockReport _SR = new StockReport("IS2", Data);
                _SR.Show();
            }
            else if (tabControl.SelectedIndex == 1)
            {
                StockReport _SR = new StockReport("IS1", _ListOfItem.Where(w => w.Qty != 0).ToList());
                _SR.Show();
            }
            else
            {
                var Production = _KPDB.MachineMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.LotNo.Contains(TSTextBoxSearch.Text.Trim()) || w.DegreeInnerDiameter.Contains(TSTextBoxSearch.Text.Trim()) || w.Size.Contains(TSTextBoxSearch.Text.Trim()) || w.HeightLength.Contains(TSTextBoxSearch.Text.Trim()) || w.CSType.Contains(TSTextBoxSearch.Text.Trim()) || w.DesignThickness.Contains(TSTextBoxSearch.Text.Trim()) || w.Weight.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList();
                StockReport _SR = new StockReport("PS1", Production.Where(w => w.Stock != 0).ToList());
                _SR.Show();
            }

        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                var RawM = _KPDB.ItemMasters.Where(w => w.Name.Contains(TSTextBoxSearch.Text.Trim()) && w.IsDelete != true && w.CompanyId == Program.CompanyId && w.Type == "Raw Material").ToList();
                var d = RawM.Select(s => new
                {
                    s.Name,
                    s.ProcessType,
                    Qty = s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Qty) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.Stock)),
                    Weight = s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Weight) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.StockWeight)),
                    s.UOM,
                    Amount = (s.ProcessType == "Re-windable" ? s.InwardMasters.Where(w => w.IsDelete != true && w.IsRew != true).Sum(a => a.Weight) : Convert.ToDecimal(s.InwardMasters.Sum(I => I.StockWeight))) * s.Rate,
                    s.Rate,
                    s.IMId,
                }).ToList();
                decimal Qty = Convert.ToDecimal(d.Where(w => w.Qty != 0).Sum(s => s.Qty));
                decimal Weight = Convert.ToDecimal(d.Where(w => w.Qty != 0).Sum(s => s.Weight));
                decimal Amount = Convert.ToDecimal(d.Where(w => w.Qty != 0).Sum(s => s.Amount));
                TSTotalStock.Text = Qty.ToString("0.00##");
                TSTotalWeight.Text = Weight.ToString("0.00##");
                TSTotalAmount.Text = Amount.ToString("C2");
            }
            else if (tabControl.SelectedIndex == 1)
            {

                decimal Qty = Convert.ToDecimal(_ListOfItem.Where(w => w.Qty != 0).Sum(s => s.Qty));
                decimal Amount = Convert.ToDecimal(_ListOfItem.Where(w => w.Qty != 0).Sum(s => s.Qty * s.Rate));
                TSTotalStock.Text = Qty.ToString("0.00##");
                TSTotalWeight.Text = "";
                TSTotalAmount.Text = Amount.ToString("C2");
            }
            else if (tabControl.SelectedIndex == 2)
            {
                var Production = _KPDB.MachineMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.LotNo.Contains(TSTextBoxSearch.Text.Trim()) || w.DegreeInnerDiameter.Contains(TSTextBoxSearch.Text.Trim()) || w.Size.Contains(TSTextBoxSearch.Text.Trim()) || w.HeightLength.Contains(TSTextBoxSearch.Text.Trim()) || w.CSType.Contains(TSTextBoxSearch.Text.Trim()) || w.DesignThickness.Contains(TSTextBoxSearch.Text.Trim()) || w.Weight.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList();
                //  var Production = _KPDB.MachineMasters.Where(w => (w.Machine.Contains(TSTextBoxSearch.Text.Trim()) || w.LotNo.Contains(TSTextBoxSearch.Text.Trim())) && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList();
                decimal Qty = Convert.ToDecimal(Production.Where(w => w.Stock != 0).Sum(s => s.Stock));
                decimal Weight = Convert.ToDecimal(Production.Where(w => w.Stock != 0).Sum(s => s.Stock * Convert.ToDecimal(s.ProductionDetails.Where(w => w.IsDelete == false).Select(r => r.Weight).FirstOrDefault())));
                decimal Amount = Convert.ToDecimal(Production.Where(w => w.Stock != 0).Sum(s => s.Amount));
                TSTotalStock.Text = Qty.ToString("0.00##");
                TSTotalWeight.Text = Weight.ToString("0.00##");
                TSTotalAmount.Text = Amount.ToString("C2");
            }
        }
    }
}
