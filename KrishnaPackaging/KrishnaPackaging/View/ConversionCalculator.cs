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
    public partial class ConversionCalculator : Form
    {
        public ConversionCalculator()
        {
            InitializeComponent();
            cmbConversionType.SelectedIndex = 0;
            txtFrom.Text = "1";
        }

        private void txtFrom_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFrom.Text.Trim()))
            {
                Conversation(cmbConversionType.Text.Trim());
            }
        }

        private void cmbConversionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbConversionType.Text.Trim()))
            {
                Conversation(cmbConversionType.Text.Trim());
            }
        }

        public void Conversation(string Type)
        {
            decimal from = string.IsNullOrEmpty(txtFrom.Text.Trim()) ? 0 : Convert.ToDecimal(txtFrom.Text.Trim());
            if (Type == "Inch to Meter")
            {
                lblFrom.Text = "From Inch";
                lblTo.Text = "To Meter";
                txtTo.Text = Convert.ToString((from > 0) ? from * (decimal)0.0254 : 0);
            }
            else if (Type == "MM to Inch")
            {
                lblFrom.Text = "From MM";
                lblTo.Text = "To Inch";
                txtTo.Text = Convert.ToString((from > 0) ? from * (decimal)0.0393701 : 0);
            }
            else if (Type == "MM to Meter")
            {
                lblFrom.Text = "From MM";
                lblTo.Text = "To Meter";
                txtTo.Text = Convert.ToString((from > 0) ? from * (decimal)0.001 : 0);
            }
        }
    }
}
