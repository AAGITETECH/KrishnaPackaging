using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class ReceiveNoteDetail
    {
        public string Item { get { return this.ItemMaster.Name; } }
        public decimal stockWeight
        {
            get
            {
                decimal SWeight = 0;
                if (this.ReceiveNoteMaster != null)
                {

                    if (this.ReceiveNoteMaster.InwardMasters.AsEnumerable().Where(w => w.IsDelete == false).Count() > 0)
                    {
                        SWeight = Convert.ToDecimal(this.ReceiveNoteMaster.InwardMasters.AsEnumerable().Where(w => w.IsDelete == false).Sum(s => Convert.ToDecimal(s.Weight)));
                    }
                }
                return decimal.Round(Convert.ToDecimal(Weight) - SWeight, 2);
            }
        }
    }
}
