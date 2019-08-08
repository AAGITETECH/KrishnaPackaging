using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GeneralCodeLibrary;
using System.ComponentModel;
using KrishnaPackaging.Data;
using KrishnaPackaging.View;
using KrishnaPackaging.Report;

namespace KrishnaPackaging
{
    public partial class MainMDI : Form
    {
        KrishnaPackagingDbDataContext _KPDB = new KrishnaPackagingDbDataContext();
        GenC obj = new GenC();

        public MainMDI()
        {
            InitializeComponent();

            try
            {

                Loginpanel.Location = new Point(
                this.ClientSize.Width / 2 - Loginpanel.Size.Width / 2,
                this.ClientSize.Height / 2 - Loginpanel.Size.Height / 2);
                Loginpanel.Anchor = AnchorStyles.None;
                //  DBBR();
                //Properties.Settings.Default.ProductKey = "";
                //Properties.Settings.Default.KeyVaild = false;
                //Properties.Settings.Default.Save();

                //bool Key = ApplictionSecurity.KeyCheck();
                //if (Key == true)
                //{
                //    btnAdd.Enabled = true;
                //    btncancel.Enabled = true;
                //    btnkey.Visible = false;
                //}
                //else
                //{
                //    btnAdd.Enabled = false;
                //    btncancel.Enabled = false;
                //    btnkey.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                obj.Information(ex.ToString());
            }
        }

        void DBBR()
        {
            //string Path = @"C:\TRDS Work\Work\BackUp\TagBak.bak";
            //var SS = _TMDB.BackupAndRestore(Path, 0);
            //if (SS == 1)
            //{

            //}
            //else
            //{

            //}
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                obj.Information("Enter Username");
                txtUsername.Focus();

            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                obj.Information("Enter Password");
                txtPassword.Focus();
            }
            else
            {

                var _UserMaster = _KPDB.UserSignIn(txtUsername.Text.Trim(), txtPassword.Text.Trim()).SingleOrDefault();
                if (_UserMaster != null)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Loginpanel.Visible = false;
                    Mmenu.Visible = true;
                    panel2.Visible = false;
                    Program.CompanyId = (int)_UserMaster.CompanyId;
                    Program.UserId = (int)_UserMaster.UserId;
                    Program.UserType = (string)_UserMaster.Type;
                    BindingList<UserPermission> _Um = new BindingList<UserPermission>(_KPDB.UserPermissions.Where(w => w.UserId == _UserMaster.UserId).ToList());
                    GetUserPermission(_Um);
                    this.Cursor = Cursors.Default;

                }
                else
                {
                    obj.Information("Username or Password not valid");
                }
            }
        }

        void GetUserPermission(BindingList<UserPermission> Um)
        {
            if (Um.Where(w => w.FormMaster.MenuName == TSMaster.Text).Count() > 0)
            {
                TSMaster.Visible = true;
            }
            else
                TSMaster.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSMasterCompany.Text).Count() > 0)
            {
                TSMasterCompany.Visible = true;
            }
            else
                TSMasterCompany.Visible = false;
            if (Um.Where(w => w.FormMaster.MenuName == TSMasterUser.Text).Count() > 0)
            {
                TSMasterUser.Visible = true;
            }
            else
                TSMasterUser.Visible = false;
            if (Um.Where(w => w.FormMaster.MenuName == TSMasterItem.Text).Count() > 0)
            {
                TSMasterItem.Visible = true;
            }
            else
                TSMasterItem.Visible = false;
            if (Um.Where(w => w.FormMaster.MenuName == TSMasterParty.Text).Count() > 0)
            {
                TSMasterParty.Visible = true;
            }
            else
                TSMasterParty.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSReceiveNote.Text).Count() > 0)
            {
                TSReceiveNote.Visible = true;
            }
            else
                TSReceiveNote.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSInward.Text).Count() > 0)
            {
                TSInward.Visible = true;
            }
            else
                TSInward.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSRewinder.Text).Count() > 0)
            {
                TSRewinder.Visible = true;
            }
            else
                TSRewinder.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSMixing.Text).Count() > 0)
            {
                TSMixing.Visible = true;
            }
            else
                TSMixing.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSMachine.Text).Count() > 0)
            {
                TSMachine.Visible = true;
            }
            else
                TSMachine.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSConsumption.Text).Count() > 0)
            {
                TSConsumption.Visible = true;
            }
            else
                TSConsumption.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSProduction.Text).Count() > 0)
            {
                TSProduction.Visible = true;
            }
            else
                TSProduction.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSStock.Text).Count() > 0)
            {
                TSStock.Visible = true;
            }
            else
                TSStock.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSDispatch.Text).Count() > 0)
            {
                TSDispatch.Visible = true;
            }
            else
                TSDispatch.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSExpense.Text).Count() > 0)
            {
                TSExpense.Visible = true;
            }
            else
                TSExpense.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TsWaste.Text).Count() > 0)
            {
                TsWaste.Visible = true;
            }
            else
                TsWaste.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == TSReport.Text).Count() > 0)
            {
                TSReport.Visible = true;
            }
            else
                TSReport.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == CTCosting.Text).Count() > 0)
            {
                CTCosting.Visible = true;
            }
            else
                CTCosting.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == CTReceiveNote.Text).Count() > 0)
            {
                CTReceiveNote.Visible = true;
            }
            else
                CTReceiveNote.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == CTRewinder.Text).Count() > 0)
            {
                CTRewinder.Visible = true;
            }
            else
                CTRewinder.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == CTMixing.Text).Count() > 0)
            {
                CTMixing.Visible = true;
            }
            else
                CTMixing.Visible = false;

            if (Um.Where(w => w.FormMaster.MenuName == CTMixing.Text).Count() > 0)
            {
                CTMixing.Visible = true;
            }
            else
                CTMixing.Visible = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.ExitThread();
        }
        public void DisposeAllButThis(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm != form)
                {
                    frm.Dispose();
                    frm.Close();
                }
            }
            panel2.Visible = false;
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mmenu.Visible = false;
            foreach (Form frm in this.MdiChildren)
            {
                frm.Dispose();
                frm.Close();
            }
            Loginpanel.Visible = true;
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnAdd.Enabled == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnAdd_Click(sender, e);
                }
            }
        }

        public static void Dash()
        {
            MainMDI t = new MainMDI();
            t.fill();
        }

        void fill()
        {

        }
        private void MainMDI_Load(object sender, EventArgs e)
        {

        }
        private void _PartyView_FormClosed(object sender, FormClosedEventArgs e)
        {
            panel2.Visible = true; ;
        }

       
        private void yearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompanyDetail _CompanyDetail = new CompanyDetail(_KPDB);
            _CompanyDetail.MdiParent = this;
            _CompanyDetail.Show();
            //DisposeAllButThis(_YearView);
        }

        private void TSMasterUser_Click(object sender, EventArgs e)
        {
            UserView _UserView = new UserView(_KPDB);
            _UserView.MdiParent = this;
            _UserView.Show();
        }

        private void TSMasterItem_Click(object sender, EventArgs e)
        {
            ItemView _ItemView = new ItemView(_KPDB);
            _ItemView.MdiParent = this;
            _ItemView.Show();
        }

        private void TSMasterParty_Click(object sender, EventArgs e)
        {
            PartyView _PartyView = new PartyView(_KPDB);
            _PartyView.MdiParent = this;
            _PartyView.Show();
        }

        private void TSReceiveNote_Click(object sender, EventArgs e)
        {
            ReceiveNoteView _ReceiveNoteView = new ReceiveNoteView(_KPDB);
            _ReceiveNoteView.MdiParent = this;
            _ReceiveNoteView.Show();
        }

        private void inwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InwardView _InwardView = new InwardView(_KPDB);
            _InwardView.MdiParent = this;
            _InwardView.Show();
        }

        private void TSRewinder_Click(object sender, EventArgs e)
        {
            RewinderView _RewinderView = new RewinderView(_KPDB);
            _RewinderView.MdiParent = this;
            _RewinderView.Show();
        }

        private void TSMixing_Click(object sender, EventArgs e)
        {
            MixingView _MixingView = new MixingView(_KPDB);
            _MixingView.MdiParent = this;
            _MixingView.Show();
        }

        private void TSMachine_Click(object sender, EventArgs e)
        {
            MachineView _MachineView = new MachineView(_KPDB);
            _MachineView.MdiParent = this;
            _MachineView.Show();
        }

        private void TSConsumption_Click(object sender, EventArgs e)
        {
            ConsumptionView _ConsumptionView = new ConsumptionView(_KPDB);
            _ConsumptionView.MdiParent = this;
            _ConsumptionView.Show();
        }

       
        private void TSProduction_Click(object sender, EventArgs e)
        {
            ProductionView _ProductionView = new ProductionView(_KPDB);
            _ProductionView.MdiParent = this;
            _ProductionView.Show();
        }

        private void TSStock_Click(object sender, EventArgs e)
        {
            StockView _S = new StockView(_KPDB);
            _S.MdiParent = this;
            _S.Show();
        }
        private void TSExpense_Click(object sender, EventArgs e)
        {
            ExpenseView _S = new ExpenseView(_KPDB);
            _S.MdiParent = this;
            _S.Show();
        }
        private void wasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WasteView _S = new WasteView(_KPDB);
            _S.MdiParent = this;
            _S.Show();
        }
        private void passwordResetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PasswordReset _P = new PasswordReset(_KPDB);
            _P.ShowDialog();
        }

        private void costingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CostingReport _Cr = new CostingReport("CO");
            _Cr.Show();
        }

        private void receiveNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWindow _Rw = new ReportWindow("RN");
            _Rw.Show();
        }

        private void rewinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWindow _Rw = new ReportWindow("RewM");
            _Rw.Show();
        }

        private void mixingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWindow _Rw = new ReportWindow("MixM");
            _Rw.Show();
        }

        private void TSDispatch_Click(object sender, EventArgs e)
        {
            DispatchView _S = new DispatchView(_KPDB);
            _S.MdiParent = this;
            _S.Show();
        }

        private void dispatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWindow _Rw = new ReportWindow("DN");
            _Rw.Show();
        }

        private void TSReport_Click(object sender, EventArgs e)
        {

        }

        private void TSSConvert_Click(object sender, EventArgs e)
        {
           
        }

        private void lotProfitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CostingReport _Cr = new CostingReport("LOT");
            _Cr.Show();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CostingReport _Cr = new CostingReport("CONP");
            _Cr.Show();
        }

        private void partyBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportWindow _Cr = new ReportWindow("PB");
            _Cr.Show();
        }
    }
}
