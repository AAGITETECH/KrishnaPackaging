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
    public partial class MachineAdd : Form
    {
        KrishnaPackagingDbDataContext _KPDB;

        GenC obj = new GenC();
        FormMode formMode;
        MachineMaster _MachineMaster;
        public MachineAdd()
        {
            InitializeComponent();
        }
        internal enum FormMode
        {
            Add, Edit, View
        }

        internal DialogResult Show(FormMode formMode, MachineMaster _machinemaster, KrishnaPackagingDbDataContext _kpdb)
        {
            _KPDB = _kpdb;
            if (formMode == FormMode.Add)
            {
                this.Text = "Add Production Lot";
                this.formMode = formMode;
                this.btnSave.Text = "&Save";
                _MachineMaster = _machinemaster;
                return base.ShowDialog();
            }
            else if (formMode == FormMode.Edit)
            {
                this.Text = "Edit Production Lot";
                this.formMode = formMode;
                this.btnSave.Text = "&Update";
                _MachineMaster = _machinemaster;
                FillRecord(_MachineMaster);
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

        private void CmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbMachine.SelectedIndex >= 0)
            {
                var SM = _KPDB.MachineMasters.Where(w => w.IsDelete != true && w.Machine == CmbMachine.SelectedItem.ToString()).OrderByDescending(o => o.MachinId).FirstOrDefault();

                if (CmbMachine.SelectedItem.ToString() == "Cone Macking")
                {
                    if (SM == null)
                    {
                        txtLotNo.Text = "CM001";
                    }
                    else
                    {
                        int INN = Convert.ToInt32(SM.LotNo.Split('M')[1]) + 1;
                        if (INN.ToString().Length == 1)
                        {
                            txtLotNo.Text = "CM00" + INN.ToString();
                        }
                        else if (INN.ToString().Length == 2)
                        {
                            txtLotNo.Text = "CM0" + INN.ToString();
                        }
                        else
                        {
                            txtLotNo.Text = "CM" + INN.ToString();
                        }
                    }
                }
                else if (CmbMachine.SelectedItem.ToString() == "Core Pipe")
                {
                    if (SM == null)
                    {
                        txtLotNo.Text = "CP001";
                    }
                    else
                    {
                        int INN = Convert.ToInt32(SM.LotNo.Split('P')[1]) + 1;
                        if (INN.ToString().Length == 1)
                        {
                            txtLotNo.Text = "CP00" + INN.ToString();
                        }
                        else if (INN.ToString().Length == 2)
                        {
                            txtLotNo.Text = "CP0" + INN.ToString();
                        }
                        else
                        {
                            txtLotNo.Text = "CP" + INN.ToString();
                        }
                    }
                }
                else if (CmbMachine.SelectedItem.ToString() == "Edge Protector")
                {
                    if (SM == null)
                    {
                        txtLotNo.Text = "EP001";
                    }
                    else
                    {
                        int INN = Convert.ToInt32(SM.LotNo.Split('P')[1]) + 1;
                        if (INN.ToString().Length == 1)
                        {
                            txtLotNo.Text = "EP00" + INN.ToString();
                        }
                        else if (INN.ToString().Length == 2)
                        {
                            txtLotNo.Text = "EP0" + INN.ToString();
                        }
                        else
                        {
                            txtLotNo.Text = "EP" + INN.ToString();
                        }
                    }
                }
            }
            else
            {
                txtLotNo.Text = "";
            }


        }
        void FillRecord(MachineMaster _Im)
        {
            try
            {
                CmbMachine.SelectedItem = _Im.Machine.ToString();
                CmbMachine.Enabled = false;
                txtLotNo.Text = _Im.LotNo;
                txtDegID.Text = _Im.DegreeInnerDiameter;
                txtSize.Text = _Im.Size;
                txtHeightLength.Text = _Im.HeightLength;
                txtCsType.Text = _Im.CSType;
                txtDesignThickness.Text = _Im.DegreeInnerDiameter;
                txtWeight.Text = _Im.Weight;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool Cansave()
        {
            if (CmbMachine.SelectedIndex < 0)
            {
                obj.Information("Select Machine");
                CmbMachine.Focus();
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
                    _MachineMaster.Machine = CmbMachine.SelectedItem.ToString();
                    _MachineMaster.LotNo = txtLotNo.Text;
                    _MachineMaster.Date = DtpDate.Value;
                    _MachineMaster.DegreeInnerDiameter = txtDegID.Text;
                    _MachineMaster.Size = txtSize.Text;
                    _MachineMaster.HeightLength = txtHeightLength.Text;
                    _MachineMaster.CSType = txtCsType.Text;
                    _MachineMaster.DesignThickness = txtDesignThickness.Text;
                    _MachineMaster.Weight = txtWeight.Text;
                    if (this.formMode == FormMode.Add)
                    {
                        _MachineMaster.CompanyId = Program.CompanyId;
                        _MachineMaster.AddDate = DateTime.Now;
                        _MachineMaster.EditDate = null;
                        _MachineMaster.IsClose = false;
                        _MachineMaster.IsDelete = false;
                        _MachineMaster.DeleteDate = null;
                        _KPDB.MachineMasters.InsertOnSubmit(_MachineMaster);
                        _KPDB.SubmitChanges();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (this.formMode == FormMode.Edit)
                    {
                        _MachineMaster.EditDate = DateTime.Now;
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
