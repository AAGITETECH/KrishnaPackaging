using KrishnaPackaging.Data;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KrishnaPackaging.Report
{
    public partial class StockReport : Form
    {
        KrishnaPackagingDbDataContext _KPDB = new KrishnaPackagingDbDataContext();

        public StockReport(string Type, IEnumerable<object> Data)
        {
            InitializeComponent();
            if (Type == "IS1")
            {
                ItemStock(Data as BindingList<ItemMaster>);
            }
            else if (Type == "IS2")
            {
                RawMaterlalItemStock(Data);
            }
            else if (Type == "PS1")
            {
                ProductoinStock(Data);
            }
        }

        private void StockReport_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
        }
        void ItemStock(BindingList<ItemMaster> data)
        {
            reportViewer1.Reset();
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

            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);

            ReportDataSource _RD = new ReportDataSource("ItemStockDataSet", data);
            string ReportPath = "KrishnaPackaging.Report.RDLC.ItemStock.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = ReportPath;
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(_RD);
            reportViewer1.LocalReport.DataSources.Add(_RD1);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }

        void RawMaterlalItemStock(IEnumerable<Object> data)
        {
            reportViewer1.Reset();
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

            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);

            ReportDataSource _RD = new ReportDataSource("RWDataSet", data);
            string ReportPath = "KrishnaPackaging.Report.RDLC.RawMaterialStock.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = ReportPath;
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(_RD);
            reportViewer1.LocalReport.DataSources.Add(_RD1);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }

        void ProductoinStock(IEnumerable<Object> data)
        {
            reportViewer1.Reset();
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

            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);

            ReportDataSource _RD = new ReportDataSource("ProductionStockDataSet", data);
            string ReportPath = "KrishnaPackaging.Report.RDLC.ProductionStock.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = ReportPath;
            reportViewer1.LocalReport.EnableExternalImages = true;
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(_RD);
            reportViewer1.LocalReport.DataSources.Add(_RD1);
            this.reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.RefreshReport();
        }

    }
}
