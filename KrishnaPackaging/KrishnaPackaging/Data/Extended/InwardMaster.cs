using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class InwardMaster
    {
        public string ReceiveNote { get { return this.ReceiveNoteMaster.PartyMaster.PartyName + "-" + this.ReceiveNoteMaster.RNNo; } }
        public string Item { get { return this.ItemMaster.Name; } }

        public decimal Stock
        {
            get
            {
                decimal QTY = Convert.ToDecimal(this.Qty);
                decimal IssuQTY = 0;
                if (this.MixingMasters.Where(w => w.IsDelete != true).Count() > 0)
                {
                    IssuQTY = Convert.ToDecimal(this.MixingMasters.Where(w => w.IsDelete != true).Sum(s => s.IssueQty));
                }
                return QTY - IssuQTY;
            }
        }
        public decimal StockWeight
        {
            get
            {
                decimal QTY = Convert.ToDecimal(this.Qty) * Convert.ToDecimal(this.Weight);
                decimal IssuQTY = 0;
                if (this.MixingMasters.Where(w => w.IsDelete != true).Count() > 0)
                {
                    IssuQTY = Convert.ToDecimal(this.MixingMasters.Where(w => w.IsDelete != true).Sum(s => s.IssueWeight));
                }
                return QTY - IssuQTY;
            }
        }

       

    }
}
