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
    public partial class ReportWindow : Form
    {
        KrishnaPackagingDbDataContext _KPDB = new KrishnaPackagingDbDataContext();
        string report = "";
        GenC obj = new GenC();
        public ReportWindow(string Type)
        {
            InitializeComponent();
            report = Type;
            FillComboBox();

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

        void FillComboBox()
        {
            if (report == "RN" || report == "DN")
            {
                BindingList<PartyMaster> ListOfPartyMaster = new BindingList<PartyMaster>(_KPDB.PartyMasters.AsEnumerable().Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.Type != (report == "DN" ? "Purchase" : "Sales")).ToList());
                ListOfPartyMaster.Insert(0, new PartyMaster { PartyId = 0, PartyName = "All" });
                CmbPM.DataSource = ListOfPartyMaster;
                CmbPM.DisplayMember = "PartyName";
                CmbPM.ValueMember = "PartyId";
                CmbPM.SelectedIndex = 0;
                panel2.Visible = false;
            }
            else if (report == "PB")
            {
                BindingList<PartyMaster> ListOfPartyMaster = new BindingList<PartyMaster>(_KPDB.PartyMasters.AsEnumerable().Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.Type == "All").ToList());
                ListOfPartyMaster.Insert(0, new PartyMaster { PartyId = 0, PartyName = "Select Party" });
                CmbPM.DataSource = ListOfPartyMaster;
                CmbPM.DisplayMember = "PartyName";
                CmbPM.ValueMember = "PartyId";
                CmbPM.SelectedIndex = 0;
                panel2.Visible = false;
            }
            else if (report == "RewM")
            {
                BindingList<ItemMaster> ListOfPartyMaster = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.ProcessType == "Re-windable").ToList());
                ListOfPartyMaster.Insert(0, new ItemMaster { IMId = 0, Name = "All" });
                CmbItem.DataSource = ListOfPartyMaster;
                CmbItem.DisplayMember = "Name";
                CmbItem.ValueMember = "IMId";
                CmbItem.SelectedIndex = 0;
                panel2.Visible = true;
            }
            else if (report == "MixM")
            {
                BindingList<ItemMaster> ListOfPartyMaster = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.ProcessType == "Mixable").ToList());
                ListOfPartyMaster.Insert(0, new ItemMaster { IMId = 0, Name = "All" });
                CmbItem.DataSource = ListOfPartyMaster;
                CmbItem.DisplayMember = "Name";
                CmbItem.ValueMember = "IMId";
                CmbItem.SelectedIndex = 0;
                panel2.Visible = true;
            }


        }

        void ReceiveNoteReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = CmbPM.SelectedValue.ToString() == "0" ? null : CmbPM.SelectedValue.ToString();
            string Fd = String.Format("{0:dd-MMM-yyyy}", dtpFromDate.Value);
            string TD = String.Format("{0:dd-MMM-yyyy}", dtpToDate.Value);
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
            var _rm = _KPDB.ReceiveNoteDetails.Where(w => w.ReceiveNoteMaster.IsDelete != true && w.ReceiveNoteMaster.CompanyId == Program.CompanyId && w.ReceiveNoteMaster.PartyId == (string.IsNullOrEmpty(PID) ? w.ReceiveNoteMaster.PartyId : Convert.ToInt32(PID)) && w.ReceiveNoteMaster.Date.Value.Date >= dtpFromDate.Value.Date && w.ReceiveNoteMaster.Date.Value.Date <= dtpToDate.Value.Date).Select(s => new
            {
                s.ReceiveNoteMaster.RNMId,
                s.ReceiveNoteMaster.PartyMaster.PartyName,
                s.ReceiveNoteMaster.RNNo,
                s.ReceiveNoteMaster.Date,
                Name = s.ItemMaster.Name,
                s.Qty,
                s.Rate,
                s.Discount,
                s.NetRate,
                s.CGST,
                s.SGST,
                s.IGST,
                s.OtherCharges,
                s.Amount,
                TotalAmount = s.ReceiveNoteMaster.Amount,
            }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.ReceiveNote.rdlc";
            ReportDataSource RDS = new ReportDataSource("ReceiveNoteDataSet", _rm);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[3];
            parms[0] = new ReportParameter("PartyName", CmbPM.Text);
            parms[1] = new ReportParameter("Date", "From " + Fd + " To " + TD);
            parms[2] = new ReportParameter("Total", "Total : " + TotalPurchase.ToString());
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

        void RewinderReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string ID = CmbItem.SelectedValue.ToString() == "0" ? null : CmbItem.SelectedValue.ToString();
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
            var _rm = _KPDB.RewinderDetails.Where(w => w.RewinderMaster.InwardMaster.IMId == (string.IsNullOrEmpty(ID) ? w.RewinderMaster.InwardMaster.IMId : Convert.ToInt32(ID)) && w.RewinderMaster.CompanyId == Program.CompanyId && w.IsDelete != true && w.RewinderMaster.IsDelete != true && w.RewinderMaster.AddDate.Value.Date >= FromItemDTP.Value.Date && w.RewinderMaster.AddDate.Value.Date <= ToItemDTP.Value.Date).Select(s => new
            {
                s.RewinderMaster.InwardMaster.ItemMaster.Name,
                s.RewinderMaster.InwardMaster.InwNo,
                Date = s.RewinderMaster.AddDate,
                s.RewNo,
                s.Size,
                s.Weight,
                Inwardweight = s.RewinderMaster.InwardMaster.Weight
            }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.RewinderReport.rdlc";
            ReportDataSource RDS = new ReportDataSource("RewinderDataSet", _rm);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[2];
            parms[0] = new ReportParameter("Item", CmbItem.Text);
            parms[1] = new ReportParameter("Date", "From " + Fd + " To " + TD);

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

        void MixingReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string ID = CmbItem.SelectedValue.ToString() == "0" ? null : CmbItem.SelectedValue.ToString();
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
            var _rm = _KPDB.MixingMasters.Where(w => w.InwardMaster.IMId == (string.IsNullOrEmpty(ID) ? w.InwardMaster.IMId : Convert.ToInt32(ID)) && w.CompanyId == Program.CompanyId && w.IsDelete != true && w.AddDate.Value.Date >= FromItemDTP.Value.Date && w.AddDate.Value.Date <= ToItemDTP.Value.Date).Select(s => new
            {
                s.InwardMaster.ItemMaster.Name,
                s.InwardMaster.InwNo,
                Date = s.AddDate,
                s.MixingNo,
                s.IssueQty,
                s.MixingWater,
                s.FinisheQty,
                s.Rate,
            }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.MixingReport.rdlc";
            ReportDataSource RDS = new ReportDataSource("MixingDataSet", _rm);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[2];
            parms[0] = new ReportParameter("Item", CmbItem.Text);
            parms[1] = new ReportParameter("Date", "From " + Fd + " To " + TD);

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

        void DispatchReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = CmbPM.SelectedValue.ToString() == "0" ? null : CmbPM.SelectedValue.ToString();
            string Fd = String.Format("{0:dd-MMM-yyyy}", dtpFromDate.Value);
            string TD = String.Format("{0:dd-MMM-yyyy}", dtpToDate.Value);
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
            var _rm = _KPDB.DispatchDetails.Where(w => w.DispatchMaster.IsDelete != true && w.DispatchMaster.CompanyId == Program.CompanyId && w.DispatchMaster.PartyId == (string.IsNullOrEmpty(PID) ? w.DispatchMaster.PartyId : Convert.ToInt32(PID)) && w.DispatchMaster.Date.Value.Date >= dtpFromDate.Value.Date && w.DispatchMaster.Date.Value.Date <= dtpToDate.Value.Date).Select(s => new
            {
                s.DispatchMaster.DMId,
                s.DispatchMaster.PartyMaster.PartyName,
                s.DispatchMaster.DNo,
                s.DispatchMaster.Date,
                s.DispatchMaster.PartyPODate,
                s.DispatchMaster.PartyPoNo,
                s.DispatchMaster.InvoiceNo,
                s.DispatchMaster.VehicleNo,
                s.MachineMaster.Machine,
                s.MachineMaster.LotNo,
                s.Qty,
                s.Rate,
                s.Amount,
                Costing = s.MachineMaster.Costing
            }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.DispatchReport.rdlc";
            ReportDataSource RDS = new ReportDataSource("DispatchDataSet", _rm);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[3];
            parms[0] = new ReportParameter("PartyName", CmbPM.Text);
            parms[1] = new ReportParameter("Date", "From " + Fd + " To " + TD);
            parms[2] = new ReportParameter("Total", "Total : " + TotalPurchase.ToString());
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

        void PartyBalanceReport()
        {
            string ReportPath = null;

            this.reportViewer.LocalReport.DataSources.Clear();
            decimal TotalPurchase = 0;
            string PID = CmbPM.SelectedValue.ToString() == "0" ? null : CmbPM.SelectedValue.ToString();
            string Fd = String.Format("{0:dd-MMM-yyyy}", dtpFromDate.Value);
            string TD = String.Format("{0:dd-MMM-yyyy}", dtpToDate.Value);
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
            var _rm = _KPDB.DispatchMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.PartyId == (string.IsNullOrEmpty(PID) ? w.PartyId : Convert.ToInt32(PID)) && w.PartyMaster.Type == "All" && w.Date.Value.Date >= dtpFromDate.Value.Date && w.Date.Value.Date <= dtpToDate.Value.Date).Select(s => new
            {
                s.DMId,
                s.PartyMaster.PartyName,
                s.DNo,
                s.Date,
                s.PartyPODate,
                s.PartyPoNo,
                s.InvoiceNo,
                s.VehicleNo,
                Amount = s.DispatchDetails.Where(w => w.IsDelete == false).Sum(d => d.Amount),
            }).ToList();

            var _RN = _KPDB.ReceiveNoteMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.PartyId == (string.IsNullOrEmpty(PID) ? w.PartyId : Convert.ToInt32(PID)) && w.PartyMaster.Type == "All" && w.Date.Value.Date >= dtpFromDate.Value.Date && w.Date.Value.Date <= dtpToDate.Value.Date).Select(s => new
            {
                s.RNMId,
                s.PartyMaster.PartyName,
                s.RNNo,
                s.Date,
                Amount = s.Amount
            }).ToList();
            ReportPath = "KrishnaPackaging.Report.RDLC.PartyReceiveDispatch.rdlc";
            ReportDataSource RDS = new ReportDataSource("DispaychDataSet", _rm);
            ReportDataSource RDS2 = new ReportDataSource("ReceiveNoteDataSet", _RN);
            ReportDataSource _RD1 = new ReportDataSource("CompanyDataSet", _listCompany);
            ReportParameter[] parms = new ReportParameter[3];
            parms[0] = new ReportParameter("PartyName", CmbPM.Text);
            parms[1] = new ReportParameter("Date", "From " + Fd + " To " + TD);
            parms[2] = new ReportParameter("Total", "Total : " + TotalPurchase.ToString());
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (report == "RN")
            {
                ReceiveNoteReport();
            }
            else if (report == "DN")
            {
                DispatchReport();
            }
            else if (report == "PB")
            {
                if (CmbPM.SelectedIndex < 1)
                {
                    obj.Information("Seletc Party.");
                    CmbItem.Focus();
                }
                else
                {
                    PartyBalanceReport();
                }
            }
        }
        private void BtnOk2_Click_1(object sender, EventArgs e)
        {
            if (report == "RewM")
            {
                RewinderReport();
            }
            else if (report == "MixM")
            {
                MixingReport();
            }
        }
    }
}
