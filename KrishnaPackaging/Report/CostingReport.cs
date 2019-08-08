using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrishnaPackaging.Data;
using GeneralCodeLibrary;

namespace KrishnaPackaging.Report
{
    public partial class CostingReport : Form
    {
        KrishnaPackagingDbDataContext _KPDB = new KrishnaPackagingDbDataContext();
        GenC obj = new GenC();
        string report = "";
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");
        string Type = "";
        public CostingReport(string type)
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;
            if (type == "CO")
            {
                PanleCL.Visible = false;
            }
            else
            {
                PanleCL.Visible = true;
                Type = type;
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
        private void CmbReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbReport.SelectedIndex >= 0)
            {
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.Machine == CmbReport.SelectedItem.ToString() && w.CompanyId == Program.CompanyId && w.ProductionDetails.Where(p => p.MachineId == w.MachinId).Any()).ToList());
                CmbPM.DataSource = null;
                CmbPM.DataSource = ListOfCategoryMaster;
                CmbPM.DisplayMember = "LotNo";
                CmbPM.ValueMember = "MachinId";
                CmbPM.SelectedIndex = -1;
            }
        }
        void CostingDataReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = CmbPM.SelectedValue.ToString() == "0" ? null : CmbPM.SelectedValue.ToString();

            var _listCompany = _KPDB.CompanyMasters.Where(w => w.CompanyId == Program.CompanyId).Select(s => new
            {
                s.CompanyName,
                s.BillingAddress,
                s.DeliveryAddress,
                s.PersonName,
                s.City,
                s.State,
                s.StateCode,
                s.Country,
                s.Email,
                s.ContactNo,
                s.Website,
                s.GSTIN,
                s.PANNo,
                ImageFile = @"file:\" + Environment.CurrentDirectory + "\\KPData\\CompanyImage\\" + s.ImageFile

            }).ToList();
            var Consumption = _KPDB.ConsumptionDetails.Where(w => w.ConsumptionMaster.MachinId == Convert.ToInt32(CmbPM.SelectedValue) && w.ConsumptionMaster.IsDelete != true && w.IsDelete != true && w.ConsumptionMaster.CompanyId == Program.CompanyId).Select(s => new
            {
                s.ConsumptionMaster.MachineMaster.Machine,
                s.ConsumptionMaster.MachineMaster.LotNo,
                s.ConsumptionMaster.Date,
                Name = s.ItemMaster.Name + (s.RDId != null ? " (" + s.RewinderDetail.RewNo + ")" : ""),
                s.Qty,
                s.ItemMaster.UOM,
                s.Rate,
                Amount = s.Rate * s.Qty,
            }).ToList();
            var Production = _KPDB.ProductionDetails.Where(w => w.MachineId == Convert.ToInt32(CmbPM.SelectedValue) && w.IsDelete != true && w.ProductionMaster.CompanyId == Program.CompanyId).Select(s => new
            {
                s.ProductionMaster.Machine,
                s.Production,
                s.Unit,
            }).ToList();

            decimal ConAmount = Convert.ToDecimal(Consumption.Sum(s => s.Amount));
            decimal ProAmount = Convert.ToDecimal(Production.Sum(s => s.Production));
            decimal Expence = _KPDB.ExpenseDetails.Where(w => w.MachineId == Convert.ToInt32(CmbPM.SelectedValue) && w.IsDelete != true).Count() == 0 ? 0 : _KPDB.ExpenseDetails.Where(w => w.MachineId == Convert.ToInt32(CmbPM.SelectedValue) && w.IsDelete != true).Sum(s => Convert.ToDecimal(s.Amount));
            decimal Waste = _KPDB.WasteMasters.Where(w => w.MachineId == Convert.ToInt32(CmbPM.SelectedValue) && w.IsDelete != true).Count() == 0 ? 0 : _KPDB.WasteMasters.Where(w => w.MachineId == Convert.ToInt32(CmbPM.SelectedValue) && w.IsDelete != true).Sum(s => Convert.ToDecimal(s.Amount));

            ReportPath = "KrishnaPackaging.Report.RDLC.Costing.rdlc";
            ReportDataSource RDS = new ReportDataSource("ConsumptionDataSet", Consumption);
            ReportDataSource RDS2 = new ReportDataSource("ProductionDataSet", Production);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[6];
            parms[0] = new ReportParameter("Machine", CmbReport.Text);
            parms[1] = new ReportParameter("LotNo", CmbPM.Text);
            parms[2] = new ReportParameter("Total", ((ConAmount + Expence - Waste) / ProAmount).ToString("C2"));
            parms[3] = new ReportParameter("Waste", Waste.ToString("0.00##"));
            parms[4] = new ReportParameter("Expense", Expence.ToString("0.00##"));
            parms[5] = new ReportParameter("ReportLanguage", "en-IN");
            this.reportViewer.LocalReport.ReportEmbeddedResource = ReportPath;
            //this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { rpInvoiceNo, rpInvoiceDate, rpPartyName, rpTotalFine, rpCompanyName, rpLogo, rpAddress, rpEmail, rpMobileNo, rpCity, rpState, rpGSTIN, rpPAN, rpTAC1, rpTAC2, rpPartyMN, rpSubTotal, rpTotalDiscount, rpCGST, rpSGST, rpIGST });
            reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.ProcessingMode = ProcessingMode.Local;
            this.reportViewer.LocalReport.SetParameters(parms);
            this.reportViewer.LocalReport.DataSources.Add(RDS);
            this.reportViewer.LocalReport.DataSources.Add(_RD1);
            this.reportViewer.LocalReport.DataSources.Add(RDS2);
            this.reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();

        }

        void ConsumptionReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = comboBox2.SelectedValue.ToString() == "0" ? null : comboBox2.SelectedValue.ToString();
            string Fd = String.Format("{0:dd-MMM-yyyy}", FromItemDTP.Value);
            string TD = String.Format("{0:dd-MMM-yyyy}", ToItemDTP.Value);
            var _listCompany = _KPDB.CompanyMasters.Where(w => w.CompanyId == Program.CompanyId).Select(s => new
            {
                s.CompanyName,
                s.BillingAddress,
                s.DeliveryAddress,
                s.PersonName,
                s.City,
                s.State,
                s.StateCode,
                s.Country,
                s.Email,
                s.ContactNo,
                s.Website,
                s.GSTIN,
                s.PANNo,
                ImageFile = @"file:\" + Environment.CurrentDirectory + "\\KPData\\CompanyImage\\" + s.ImageFile

            }).ToList();

			
            var R = _KPDB.ItemMasters.AsEnumerable().Where(w => w.ConsumptionDetails.Where(d => d.ConsumptionMaster.MachinId == (string.IsNullOrEmpty(PID) ? d.ConsumptionMaster.MachinId : Convert.ToInt32(PID)) && d.ConsumptionMaster.IsDelete == false && d.IsDelete == false && d.ConsumptionMaster.Date.Value.Date >= FromItemDTP.Value.Date && d.ConsumptionMaster.Date.Value.Date <= ToItemDTP.Value.Date).Any())
                .Select(s => new
                {
                    s.Name,
                    Qty = s.ConsumptionDetails.Sum(e => e.Qty),
                    Amount = s.ConsumptionDetails.Sum(e => e.Qty * e.Rate),
                }).ToList();

            ReportPath = "KrishnaPackaging.Report.RDLC.Consumption.rdlc";
            ReportDataSource RDS = new ReportDataSource("ConsumptionDetail", R);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[3];
            parms[0] = new ReportParameter("Machine", comboBox1.Text);
            parms[1] = new ReportParameter("Lot", comboBox2.Text);
            parms[2] = new ReportParameter("Date", "From " + Fd + " To " + TD);

            this.reportViewer.LocalReport.ReportEmbeddedResource = ReportPath;
            //this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { rpInvoiceNo, rpInvoiceDate, rpPartyName, rpTotalFine, rpCompanyName, rpLogo, rpAddress, rpEmail, rpMobileNo, rpCity, rpState, rpGSTIN, rpPAN, rpTAC1, rpTAC2, rpPartyMN, rpSubTotal, rpTotalDiscount, rpCGST, rpSGST, rpIGST });
            reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.ProcessingMode = ProcessingMode.Local;
            this.reportViewer.LocalReport.SetParameters(parms);
            this.reportViewer.LocalReport.DataSources.Add(RDS);
            this.reportViewer.LocalReport.DataSources.Add(_RD1);
            this.reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();

        }

        void LotReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = comboBox2.SelectedValue.ToString() == "0" ? null : comboBox2.SelectedValue.ToString();
            string Fd = String.Format("{0:dd-MMM-yyyy}", FromItemDTP.Value);
            string TD = String.Format("{0:dd-MMM-yyyy}", ToItemDTP.Value);
            var _listCompany = _KPDB.CompanyMasters.Where(w => w.CompanyId == Program.CompanyId).Select(s => new
            {
                s.CompanyName,
                s.BillingAddress,
                s.DeliveryAddress,
                s.PersonName,
                s.City,
                s.State,
                s.StateCode,
                s.Country,
                s.Email,
                s.ContactNo,
                s.Website,
                s.GSTIN,
                s.PANNo,
                ImageFile = @"file:\" + Environment.CurrentDirectory + "\\KPData\\CompanyImage\\" + s.ImageFile

            }).ToList();

   
            var MM = _KPDB.MachineMasters.AsEnumerable().Where(w => w.MachinId == (string.IsNullOrEmpty(PID) ? w.MachinId : Convert.ToInt32(PID)) && w.IsDelete == false && w.DispatchDetails.Where(d => d.DispatchMaster.IsDelete == false && d.IsDelete == false && d.DispatchMaster.Date >= FromItemDTP.Value.Date && d.DispatchMaster.Date.Value.Date <= ToItemDTP.Value.Date).Any()).
                Select(s => new
                {
                    s.Machine,
                    s.LotNo,
                   Qty=s.DispatchDetails.Sum(D=>D.Qty),
                   DispatchAmount=s.DispatchDetails.Sum(D=>D.Qty*D.Rate),
                   CostingAmount = s.DispatchDetails.Sum(D=>D.Qty*s.Costing),
                }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.LotProfitLoss.rdlc";
            ReportDataSource RDS = new ReportDataSource("LotPLDetail", MM);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[3];
            parms[0] = new ReportParameter("Machine", comboBox1.Text);
            parms[1] = new ReportParameter("Lot", comboBox2.Text);
            parms[2] = new ReportParameter("Date", "From " + Fd + " To " + TD);

            this.reportViewer.LocalReport.ReportEmbeddedResource = ReportPath;
            //this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { rpInvoiceNo, rpInvoiceDate, rpPartyName, rpTotalFine, rpCompanyName, rpLogo, rpAddress, rpEmail, rpMobileNo, rpCity, rpState, rpGSTIN, rpPAN, rpTAC1, rpTAC2, rpPartyMN, rpSubTotal, rpTotalDiscount, rpCGST, rpSGST, rpIGST });
            reportViewer.LocalReport.EnableExternalImages = true;
            this.reportViewer.ProcessingMode = ProcessingMode.Local;
            this.reportViewer.LocalReport.SetParameters(parms);
            this.reportViewer.LocalReport.DataSources.Add(RDS);
            this.reportViewer.LocalReport.DataSources.Add(_RD1);
            this.reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer.ZoomPercent = 100;
            this.reportViewer.RefreshReport();

        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (CmbReport.SelectedIndex < 0)
            {
                obj.Information("Select Machine");
                CmbReport.Focus();
            }
            else if (CmbPM.SelectedIndex < 0)
            {
                obj.Information("Select Lot No.");
                CmbPM.Focus();
            }
            else
                CostingDataReport();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                obj.Information("Select Machine");
                CmbReport.Focus();
            }
            else if (comboBox2.SelectedIndex < 0)
            {
                obj.Information("Select Lot No.");
                CmbPM.Focus();
            }
            else
            {
                if (Type == "CONP")
                {
                    ConsumptionReport();
                }
               else if (Type == "LOT")
                {
                    LotReport();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                BindingList<MachineMaster> ListOfCategoryMaster = new BindingList<MachineMaster>(_KPDB.MachineMasters.Where(w => w.IsDelete != true && w.Machine == comboBox1.SelectedItem.ToString() && w.CompanyId == Program.CompanyId && w.ProductionDetails.Where(p => p.MachineId == w.MachinId).Any()).ToList());
                ListOfCategoryMaster.Insert(0, new MachineMaster { MachinId = 0, LotNo = "All" });
                comboBox2.DataSource = null;
                comboBox2.DataSource = ListOfCategoryMaster;
                comboBox2.DisplayMember = "LotNo";
                comboBox2.ValueMember = "MachinId";
                comboBox2.SelectedIndex = 0;
            }
        }
    }
}
