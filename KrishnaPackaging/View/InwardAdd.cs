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
    public partial class InwardAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        InwardMaster _InwardMaster;
        public InwardAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, InwardMaster _inwardmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Item";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                // _InwardMaster = _inwardmaster;
                FillComboBox();
                FillReceiveNoteNo();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Item";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                FillComboBox();
                _InwardMaster = _inwardmaster;
                FillRecord(_InwardMaster);
                return base.ShowDialog();
            }
            return DialogResult.Cancel;
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
        void FillReceiveNoteNo()
        {
            var SM = _KPDB.InwardMasters.Where(w => w.IsDelete != true).OrderByDescending(o => o.InwId).FirstOrDefault();
            if (SM == null)
            {
                LabReceiveNoteNo.Text = "INW001";
            }
            else
            {
                int INN = Convert.ToInt32(SM.InwNo.Split('W')[1]) + 1;
                if (INN.ToString().Length == 1)
                {
                    LabReceiveNoteNo.Text = "INW00" + INN.ToString();
                }
                else if (INN.ToString().Length == 2)
                {
                    LabReceiveNoteNo.Text = "INW0" + INN.ToString();
                }
                else
                {
                    LabReceiveNoteNo.Text = "INW" + INN.ToString();
                }
            }
        }
        void FillComboBox()
        {
            if (formMode == FormMode.Add)
            {
                CmbReceiveNote.SelectedIndexChanged -= new EventHandler(CmbType_SelectedIndexChanged);
                BindingList<ReceiveNoteMaster> ListOfCategoryMaster = new BindingList<ReceiveNoteMaster>(_KPDB.ReceiveNoteMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.ReceiveNoteDetails.Where(d => d.ItemMaster.Type == "Raw Material" && d.IsInward == false).Any()).ToList());
                CmbReceiveNote.DataSource = null;
                CmbReceiveNote.DataSource = ListOfCategoryMaster;
                CmbReceiveNote.DisplayMember = "Inward";
                CmbReceiveNote.ValueMember = "RNMId";
                CmbReceiveNote.SelectedIndex = -1;
                CmbReceiveNote.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            }
            else
            {
                CmbReceiveNote.SelectedIndexChanged -= new EventHandler(CmbType_SelectedIndexChanged);
                BindingList<ReceiveNoteMaster> ListOfCategoryMaster = new BindingList<ReceiveNoteMaster>(_KPDB.ReceiveNoteMasters.Where(w => w.IsDelete != true && w.CompanyId == Program.CompanyId && w.ReceiveNoteDetails.Where(d => d.ItemMaster.Type == "Raw Material").Any()).ToList());
                CmbReceiveNote.DataSource = null;
                CmbReceiveNote.DataSource = ListOfCategoryMaster;
                CmbReceiveNote.DisplayMember = "Inward";
                CmbReceiveNote.ValueMember = "RNMId";
                CmbReceiveNote.SelectedIndex = -1;
                CmbReceiveNote.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);
            }

        }
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formMode == FormMode.Add)
            {
                if (CmbReceiveNote.SelectedIndex >= 0)
                {
                    txtTotalQty.Text = "";
                    txttotalweight.Text = "";
                    ReceiveNoteMaster _Rm = CmbReceiveNote.SelectedItem as ReceiveNoteMaster;

                    CmbItem.SelectedIndexChanged -= new EventHandler(CmbItem_SelectedIndexChanged);
                    BindingList<ReceiveNoteDetail> ListOfItemMaster = new BindingList<ReceiveNoteDetail>(_KPDB.ReceiveNoteDetails.Where(w => w.IsDelete != true && w.RNMId == _Rm.RNMId && w.ItemMaster.Type == "Raw Material" && w.IsInward == false && w.CompanyId == Program.CompanyId).ToList());
                    CmbItem.DataSource = null;
                    CmbItem.DataSource = ListOfItemMaster;
                    CmbItem.DisplayMember = "Item";
                    CmbItem.ValueMember = "IMId";
                    CmbItem.SelectedIndex = -1;
                    CmbItem.SelectedIndexChanged += new EventHandler(CmbItem_SelectedIndexChanged);
                }
                else
                {
                    txtTotalQty.Text = "";
                    txttotalweight.Text = "";
                }
            }
            else
            {
                if (CmbReceiveNote.SelectedIndex >= 0)
                {
                    txtTotalQty.Text = "";
                    txttotalweight.Text = "";
                    ReceiveNoteMaster _Rm = CmbReceiveNote.SelectedItem as ReceiveNoteMaster;
                    CmbItem.SelectedIndexChanged -= new EventHandler(CmbItem_SelectedIndexChanged);
                    BindingList<ReceiveNoteDetail> ListOfItemMaster = new BindingList<ReceiveNoteDetail>(_KPDB.ReceiveNoteDetails.Where(w => w.IsDelete != true && w.RNMId == _Rm.RNMId && w.ItemMaster.Type == "Raw Material" && w.CompanyId == Program.CompanyId).ToList());
                    CmbItem.DataSource = null;
                    CmbItem.DataSource = ListOfItemMaster;
                    CmbItem.DisplayMember = "Item";
                    CmbItem.ValueMember = "IMId";
                    CmbItem.SelectedIndex = -1;
                    CmbItem.SelectedIndexChanged += new EventHandler(CmbItem_SelectedIndexChanged);
                }
                else
                {
                    txtTotalQty.Text = "";
                    txttotalweight.Text = "";
                }
            }
        }
        private void CmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                FillItemDetail();
            }
            else
            {
                txtTotalQty.Text = "";
            }
        }

        void FillItemDetail()
        {
            ReceiveNoteDetail _Rm = _KPDB.ReceiveNoteDetails.Where(w => w.RNMId == Convert.ToInt64(CmbReceiveNote.SelectedValue) && w.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.IsDelete != true).SingleOrDefault();
            if (_Rm != null)
            {
                decimal Qty = Convert.ToDecimal(_Rm.Qty) - Convert.ToDecimal(_Rm.InwardQty);
                decimal Weight = Convert.ToDecimal(_Rm.stockWeight) ;
                if (Qty != 0)
                {
                    txtTotalQty.Text = Qty.ToString("0.00##");
                    txttotalweight.Text = Weight.ToString("0.00##");
                    if (_Rm.ItemMaster.InwardType == "Single")
                    {
                        txtQty.Text = "1";
                    }
                    else if (_Rm.ItemMaster.InwardType == "Multiple")
                    {
                        txtQty.Text = Convert.ToDecimal(_Rm.Qty).ToString("0.00##");
                    }
                }
            }
        }

        void FillRecord(InwardMaster _Im)
        {
            try
            {
                CmbReceiveNote.SelectedValue = _Im.RNMId;
                CmbItem.SelectedValue = _Im.IMId;
                LabReceiveNoteNo.Text = _Im.InwNo;
                CmbItem.Enabled = false;
                CmbReceiveNote.Enabled = false;
                txtQty.Enabled = false;
                // CmbUnit.Enabled = false;
                txtQty.Text = Convert.ToDecimal(_Im.Qty).ToString("N2");
                decimal Size = 0;
                //MM=Inch*25.4
                //Inch=mm/25.4
                if (_Im.SizeUnit.ToString() == "Inch")
                {
                    decimal mm = Convert.ToDecimal(25.4);
                    Size = Convert.ToDecimal(_Im.Size) / mm;
                    txtSize.Text = Size.ToString("N2");
                }
                else
                {
                    txtSize.Text = Convert.ToDecimal(_Im.Size).ToString("N2");

                }
                txtWeight.Text = Convert.ToDecimal(_Im.Weight).ToString("N2");
                CmbUnit.SelectedItem = _Im.SizeUnit.ToString();
                txtGSM.Text = _Im.GSM;
                txtBF.Text = _Im.BF;
                txtPlybond.Text = _Im.Plybond;
                txtShade.Text = _Im.Shade;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool Cansave()
        {

            if (CmbReceiveNote.SelectedIndex < 0)
            {
                obj.Information("Select ReceiveNote");
                CmbReceiveNote.Focus();
                return false;
            }
            else if (CmbItem.SelectedIndex < 0)
            {
                obj.Information("Select Item");
                CmbItem.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtWeight.Text.Trim()))
            {
                obj.Information("Enter Weight");
                txtWeight.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtSize.Text.Trim()))
            {
                obj.Information("Enter Size");
                txtSize.Focus();
                return false;
            }
            else if (CmbUnit.SelectedIndex < 0)
            {
                obj.Information("Select Unit");
                CmbUnit.Focus();
                return false;
            }
            else
                return true;
        }
        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.SelectedText = "";
            e.Handled = !obj.IsNumericTwoDecimalPlace(txt.Text, e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cansave())
                {

                    if (this.formMode == FormMode.Add)
                    {
                        _InwardMaster = new InwardMaster();
                        _InwardMaster.RNMId = Convert.ToInt32(CmbReceiveNote.SelectedValue);
                        _InwardMaster.ReceiveNoteMaster = CmbReceiveNote.SelectedItem as ReceiveNoteMaster;
                        _InwardMaster.IMId = Convert.ToInt32(CmbItem.SelectedValue);
                        _InwardMaster.InwNo = LabReceiveNoteNo.Text.ToString();
                        _InwardMaster.Qty = Convert.ToDecimal(txtQty.Text);
                        _InwardMaster.GSM = txtGSM.Text;
                        _InwardMaster.BF = txtBF.Text;
                        _InwardMaster.Plybond = txtPlybond.Text;
                        _InwardMaster.Shade = txtShade.Text;
                        _InwardMaster.IsClose = false;
                        decimal size = 0;
                        //MM=Inch*25.4
                        //Inch=mm/25.4
                        if (CmbUnit.SelectedItem.ToString() == "Inch")
                        {
                            decimal mm = Convert.ToDecimal(25.4);
                            size = Convert.ToDecimal(txtSize.Text) * mm;
                            _InwardMaster.Size = size;
                        }
                        else
                        {
                            _InwardMaster.Size = Convert.ToDecimal(txtSize.Text);

                        }

                        _InwardMaster.Weight = Convert.ToDecimal(txtWeight.Text);
                        _InwardMaster.SizeUnit = CmbUnit.SelectedItem.ToString();
                        int Status = 0;
                        ReceiveNoteDetail _Rm = _KPDB.ReceiveNoteDetails.Where(w => w.RNMId == Convert.ToInt64(CmbReceiveNote.SelectedValue) && w.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.IsDelete != true).SingleOrDefault();
                        if (_Rm != null)
                        {
                            _Rm.InwardQty = _Rm.InwardQty + Convert.ToDecimal(txtQty.Text);
                            if (_Rm.Qty == _Rm.InwardQty)
                            {
                                _Rm.IsInward = true;
                                Status = 1;
                            }
                        }

                        _InwardMaster.IsRew = false;
                        _InwardMaster.CompanyId = Program.CompanyId;
                        _InwardMaster.AddDate = DateTime.Now;
                        _InwardMaster.EditDate = null;
                        _InwardMaster.IsDelete = false;
                        _InwardMaster.DeleteDate = null;
                        _KPDB.InwardMasters.InsertOnSubmit(_InwardMaster);
                        _KPDB.SubmitChanges();
                        decimal We= Convert.ToDecimal(txttotalweight.Text) - Convert.ToDecimal(txtWeight.Text);
                        txttotalweight.Text ="";
                        txttotalweight.Text = We.ToString();
                        if (Status == 1)
                        {
                            this.Close();
                        }
                        Clear();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _InwardMaster.RNMId = Convert.ToInt32(CmbReceiveNote.SelectedValue);
                        _InwardMaster.IMId = Convert.ToInt32(CmbItem.SelectedValue);
                        _InwardMaster.InwNo = LabReceiveNoteNo.Text.ToString();
                        _InwardMaster.Qty = Convert.ToDecimal(txtQty.Text);
                        _InwardMaster.GSM = txtGSM.Text;
                        _InwardMaster.BF = txtBF.Text;
                        _InwardMaster.Plybond = txtPlybond.Text;
                        _InwardMaster.Shade = txtShade.Text;
                        decimal size = 0;
                        //MM=Inch*25.4
                        //Inch=mm/25.4
                        if (CmbUnit.SelectedItem.ToString() == "Inch")
                        {
                            decimal mm = Convert.ToDecimal(25.4);
                            size = Convert.ToDecimal(txtSize.Text) * mm;
                            _InwardMaster.Size = size;
                        }
                        else
                        {
                            _InwardMaster.Size = Convert.ToDecimal(txtSize.Text);

                        }
                        _InwardMaster.Weight = Convert.ToDecimal(txtWeight.Text);

                        _InwardMaster.SizeUnit = CmbUnit.SelectedItem.ToString();
                        _InwardMaster.EditDate = DateTime.Now;
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Information(ex.ToString());
            }
        }
        void Clear()
        {
            FillReceiveNoteNo();
            FillItemDetail();
            txtWeight.Text = "";
            txtSize.Text = "";
            txtWeight.Focus();

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
