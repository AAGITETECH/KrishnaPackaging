using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class ConsumptionDetail
    {
        public string Item { get { return this.ItemMaster.Name; } }

        public string RewMixNo
        {
            get
            {
                string No = "";
                if (this.MMId!=null)
                {
                    No = this.MixingMaster.MixingNo;
                }
                else if (this.RDId!=null)
                {
                    No = this.RewinderDetail.RewNo;

                }
                else
                {
                    No = "NoApplicable";
                }
                return No;
            }
        }

    }
}
