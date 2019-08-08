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
    public partial class RawMaterlalView : Form
    {
        GenC obj = new GenC();

        public RawMaterlalView(object datt, string Qty,string Item)
        {
            InitializeComponent();
            BindDataGrid(datt);
            TSLTQ.Text = Qty;
            TSLItem.Text = Item;
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

        private void BindDataGrid(object Data)
        {
            GrinEditDeleteDetailView.DataSource = null;
            GrinEditDeleteDetailView.DataSource = Data;
            //GrinEditDeleteDetailView.Columns["Qty"].DefaultCellStyle.Format = "N2";
            //GrinEditDeleteDetailView.Columns["Weight"].DefaultCellStyle.Format = "N2";
            //GrinEditDeleteDetailView.Columns["Size"].DefaultCellStyle.Format = "N2";
            //GrinEditDeleteDetailView.Columns["IMId"].Visible = false;
            //GrinEditDeleteDetailView.Columns["CompanyId"].Visible = false;
            //GrinEditDeleteDetailView.Columns["AddDate"].Visible = false;
            //GrinEditDeleteDetailView.Columns["EditDate"].Visible = false;
            //GrinEditDeleteDetailView.Columns["IsDelete"].Visible = false;
            //GrinEditDeleteDetailView.Columns["DeleteDate"].Visible = false;
            //GrinEditDeleteDetailView.Columns["CompanyMaster"].Visible = false;

        }


    }
}
