using GeneralCodeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KrishnaPackaging.Data;

namespace KrishnaPackaging.View
{
    public partial class CompanyDetail : Form
    {
        KrishnaPackagingDbDataContext _KPDB;
        GenC obj = new GenC();
        CompanyMaster _CompanyMaster = new CompanyMaster();

        public CompanyDetail(KrishnaPackagingDbDataContext _kpdb)
        {
            InitializeComponent();
            _KPDB = _kpdb;
            //if (Program.CompanyMaster != null)
            //{
            _CompanyMaster = _KPDB.CompanyMasters.Where(w => w.CompanyId == Program.CompanyId).SingleOrDefault();
            if (File.Exists(Environment.CurrentDirectory + "\\KPData\\CompanyImage\\" + _CompanyMaster.ImageFile))
            {
                string path = Environment.CurrentDirectory + "\\KPData\\CompanyImage\\" + _CompanyMaster.ImageFile;
                PBLogo.Image = new Bitmap(path);
                PBLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                ImageFileName = "";
            }
            if (_CompanyMaster != null)
            {
                txtCmpName.Text = _CompanyMaster.CompanyName;
                txtBAddress.Text = _CompanyMaster.BillingAddress;
                txtAddress.Text = _CompanyMaster.DeliveryAddress;
                txtMobileNo.Text = _CompanyMaster.ContactNo;
                txtPersonName.Text = _CompanyMaster.PersonName;
                txtEmail.Text = _CompanyMaster.Email;
                txtState.Text = _CompanyMaster.State;
                txtStateCode.Text = _CompanyMaster.StateCode;
                txtcity.Text = _CompanyMaster.City;
                txtGSTIN.Text = _CompanyMaster.GSTIN;
                txtPanno.Text = _CompanyMaster.PANNo;

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



        private bool Cansave()
        {
            if (string.IsNullOrEmpty(txtCmpName.Text.Trim()))
            {
                obj.Information("Enter PartyName");
                return false;
            }
            else
                return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (Cansave())
            {

                if (_CompanyMaster == null)
                {
                    _CompanyMaster.CompanyName = txtCmpName.Text;
                    _CompanyMaster.PersonName = txtPersonName.Text;
                    _CompanyMaster.BillingAddress = txtBAddress.Text;
                    _CompanyMaster.DeliveryAddress = txtAddress.Text;
                    _CompanyMaster.Email = txtEmail.Text;
                    _CompanyMaster.ContactNo = txtMobileNo.Text;
                    _CompanyMaster.City = txtcity.Text;
                    _CompanyMaster.State = txtState.Text;
                    _CompanyMaster.GSTIN = txtGSTIN.Text;
                    _CompanyMaster.StateCode = txtStateCode.Text;
                    _CompanyMaster.PANNo = txtPanno.Text;
                    _CompanyMaster.IsDelete = false;
                    if (!string.IsNullOrEmpty(ImageFileName))
                    {
                        _CompanyMaster.ImageFile = SavePhoto(_CompanyMaster.CompanyName);
                    }
                    _KPDB.CompanyMasters.InsertOnSubmit(_CompanyMaster);
                    _KPDB.SubmitChanges();
                }
                else
                {
                    _CompanyMaster.CompanyName = txtCmpName.Text;
                    _CompanyMaster.PersonName = txtPersonName.Text;
                    _CompanyMaster.BillingAddress = txtBAddress.Text;
                    _CompanyMaster.DeliveryAddress = txtAddress.Text;
                    _CompanyMaster.Email = txtEmail.Text;
                    _CompanyMaster.ContactNo = txtMobileNo.Text;
                    _CompanyMaster.City = txtcity.Text;
                    _CompanyMaster.State = txtState.Text;
                    _CompanyMaster.GSTIN = txtGSTIN.Text;
                    _CompanyMaster.StateCode = txtStateCode.Text;
                    _CompanyMaster.PANNo = txtPanno.Text;
                    if (!string.IsNullOrEmpty(ImageFileName))
                    {
                        _CompanyMaster.ImageFile = SavePhoto(_CompanyMaster.CompanyName);
                    }
                    _KPDB.SubmitChanges();
                    this.Close();
                }
                ImageFileName = "";
                obj.Information("Successfully Saved");
            }

        }
        string ImageFileName;
        public string SavePhoto(String RowId)
        {
            string CompanyImagep = "";
            if (!string.IsNullOrEmpty(ImageFileName))
            {
                string Path = Environment.CurrentDirectory + "//KPData//CompanyImage";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                string FileP = ImageFileName;

                FileInfo FPhoto = new FileInfo(ImageFileName);
                string LogoName = "CompanyLogo" + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + FPhoto.Extension.ToString();
                string PhotoName = Path + @"\" + LogoName;
                CompanyImagep = LogoName;
                FPhoto.CopyTo(PhotoName.ToString(), true);

            }
            return CompanyImagep;

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "JPG|*.jpg;*.jpeg|PNG|*.png";
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                PBLogo.Image = new Bitmap(open.FileName);
                PBLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                // image file path  
                ImageFileName = open.FileName;
            }
        }
    }
}
