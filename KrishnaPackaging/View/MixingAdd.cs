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
    public partial class MixingAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        MixingMaster _MixingMaster;
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-IN");

        public MixingAdd()
        {
            InitializeComponent();
            Application.CurrentCulture = cultureInfo;

        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, MixingMaster _mixingmaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            FillComboBox();
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Mixing";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _MixingMaster = _mixingmaster;
                FillMixingNo();
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Mixing";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _MixingMaster = _mixingmaster;
                FillRecord(_MixingMaster);
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

        void FillRecord(MixingMaster _Im)
        {
            try
            {
                CmbItem.SelectedValue = _Im.IMId;
                txtissueQty.Text =Convert.ToDecimal(_Im.IssueQty).ToString("0.00##");
                CmbInward.SelectedValue = _Im.InwId;
                txtMixingwater.Text = Convert.ToDecimal(_Im.MixingWater).ToString("0.00##");
                LabMixingNo.Text = _Im.MixingNo.ToString();
                txtfinisheqty.Text = Convert.ToDecimal(_Im.FinisheQty).ToString("0.00##");
                LabRate.Text = Convert.ToDecimal(_Im.Rate).ToString("0.00##");
                CmbItem.Enabled = false;
                CmbInward.Enabled = false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        void FillMixingNo()
        {
            var SM = _KPDB.MixingMasters.Where(w => w.IsDelete != true).OrderByDescending(o => o.MMId).FirstOrDefault();
            if (SM == null)
            {
                LabMixingNo.Text = "MIX001";
            }
            else
            {
                int INN = Convert.ToInt32(SM.MixingNo.Split('X')[1]) + 1;
                if (INN.ToString().Length == 1)
                {
                    LabMixingNo.Text = "MIX00" + INN.ToString();
                }
                else if (INN.ToString().Length == 2)
                {
                    LabMixingNo.Text = "MIX0" + INN.ToString();
                }
                else
                {
                    LabMixingNo.Text = "MIX" + INN.ToString();
                }
            }
        }
        void FillComboBox()
        {
            CmbItem.SelectedIndexChanged -= new EventHandler(CmbType_SelectedIndexChanged);
            BindingList<ItemMaster> ListOfItemMaster = new BindingList<ItemMaster>(_KPDB.ItemMasters.Where(w => w.ProcessType == "Mixable" && w.IsDelete != true && w.CompanyId == Program.CompanyId).ToList());
            CmbItem.DataSource = null;
            CmbItem.DataSource = ListOfItemMaster;
            CmbItem.DisplayMember = "Name";
            CmbItem.ValueMember = "IMId";
            CmbItem.SelectedIndex = -1;
            CmbItem.SelectedIndexChanged += new EventHandler(CmbType_SelectedIndexChanged);

        }
        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                LabAQty.Text = "";
                CmbInward.SelectedIndexChanged -= new EventHandler(CmbInward_SelectedIndexChanged);
                BindingList<InwardMaster> ListOfInwardMaster = new BindingList<InwardMaster>(_KPDB.InwardMasters.AsEnumerable().Where(w => w.IMId == Convert.ToInt64(CmbItem.SelectedValue) && w.IsDelete != true && w.CompanyId == Program.CompanyId&& w.Qty > Convert.ToDecimal(w.MixingMasters.AsEnumerable().Where(m => m.IsDelete != true).Sum(a => a.IssueQty))).ToList());
               // var NN = ListOfInwardMaster.Where(w => ).ToList();
                CmbInward.DataSource = null;
                CmbInward.DataSource = ListOfInwardMaster;
                CmbInward.DisplayMember = "InwNo";
                CmbInward.ValueMember = "InwId";
                CmbInward.SelectedIndex = -1;
                CmbInward.SelectedIndexChanged += new EventHandler(CmbInward_SelectedIndexChanged);
            }
            else
            {
                LabAQty.Text = "";
            }

        }
        private void CmbInward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                LabAQty.Text = "";
                InwardMaster _Im = CmbInward.SelectedItem as InwardMaster;
                decimal issuQty = _Im.MixingMasters.Where(w => w.IsDelete != true).Count() == 0 ? 0 : Convert.ToDecimal(_Im.MixingMasters.Where(w => w.IsDelete != true).ToList().Sum(s => s.IssueQty));
                decimal AQty = Convert.ToDecimal(_Im.Qty) - issuQty;
                LabAQty.Text = Convert.ToDecimal(AQty).ToString("0.00##");
                txtissueQty_Leave(sender, e);
            }
            else
            {
                LabAQty.Text = "";

            }
        }
        private void txtissueQty_Leave(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                InwardMaster _Im = CmbInward.SelectedItem as InwardMaster;
                decimal AQty = LabAQty.Text != "" ? Decimal.Round(Convert.ToDecimal(LabAQty.Text), 0) : 0;
                decimal IssueQty = txtissueQty.Text != "" ? Decimal.Round(Convert.ToDecimal(txtissueQty.Text), 0) : 0;

                decimal Weight = Convert.ToDecimal(_Im.Weight);
                decimal TotalWeight = Weight * IssueQty;
                LabIssueWeight.Text = TotalWeight.ToString("0.00##");
                if (_MixingMaster.MMId != 0)
                {
                    AQty = AQty + Convert.ToDecimal(_MixingMaster.IssueQty);
                }
                if (IssueQty > AQty)
                {
                    txtissueQty.Text = "";
                    obj.Information("Out Of Stock Qty..");
                }
                txtfinisheqty_Leave(sender, e);
            }
        }

        private void txtfinisheqty_Leave(object sender, EventArgs e)
        {
            if (CmbItem.SelectedIndex >= 0)
            {
                InwardMaster _Im = CmbInward.SelectedItem as InwardMaster;
                decimal issuQty = string.IsNullOrEmpty(txtissueQty.Text) ? 0 : Convert.ToDecimal(txtissueQty.Text);

                decimal Weight = Convert.ToDecimal(_Im.Weight);
                decimal TotalWeight = Weight * issuQty;
                ReceiveNoteDetail Data = _Im.ReceiveNoteMaster.ReceiveNoteDetails.Where(w => w.IMId == _Im.IMId && w.IsDelete != true).SingleOrDefault();
                decimal Rate = Convert.ToDecimal(Data.Rate);
                decimal TotalRate = TotalWeight * Rate;
                decimal finisheQty = string.IsNullOrEmpty(txtfinisheqty.Text) ? 0 : Convert.ToDecimal(txtfinisheqty.Text);
                if (finisheQty != 0)
                {
                    decimal NewRate = TotalRate / finisheQty;
                    LabRate.Text = Convert.ToDecimal(NewRate).ToString("0.00##");
                }
            }

        }

        private bool Cansave()
        {

            if (CmbItem.SelectedIndex < 0)
            {
                obj.Information("Select Item");
                CmbItem.Focus();
                return false;
            }
            if (CmbInward.SelectedIndex < 0)
            {
                obj.Information("Select Inward.");
                CmbInward.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtissueQty.Text.Trim()))
            {
                obj.Information("Enter Issue Qty");
                txtissueQty.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtMixingwater.Text.Trim()))
            {
                obj.Information("Enter Mixing Water");
                txtMixingwater.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtfinisheqty.Text.Trim()))
            {
                obj.Information("Enter Finished Qty.");
                txtMixingwater.Focus();
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
                    _MixingMaster.IMId = Convert.ToInt64(CmbItem.SelectedValue);
                    _MixingMaster.InwId = Convert.ToInt64(CmbInward.SelectedValue);
                    _MixingMaster.IssueQty = Convert.ToDecimal(txtissueQty.Text);
                    _MixingMaster.MixingWater = Convert.ToDecimal(txtMixingwater.Text);
                    _MixingMaster.Rate = Convert.ToDecimal(LabRate.Text);
                    _MixingMaster.FinisheQty = Convert.ToDecimal(txtfinisheqty.Text);
                    if (this.formMode == FormMode.Add)
                    {
                        _MixingMaster.IssueWeight = 0;
                        _MixingMaster.MixingNo = LabMixingNo.Text.ToString(); ;
                        _MixingMaster.CompanyId = Program.CompanyId;
                        _MixingMaster.AddDate = DateTime.Now;
                        _MixingMaster.EditDate = null;
                        _MixingMaster.IsDelete = false;
                        _MixingMaster.DeleteDate = null;
                        _KPDB.MixingMasters.InsertOnSubmit(_MixingMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _MixingMaster.EditDate = DateTime.Now;
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
