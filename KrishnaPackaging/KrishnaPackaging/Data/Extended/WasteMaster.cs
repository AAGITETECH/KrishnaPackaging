using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class WasteMaster
    {
        public string Machine
        {
            get
            {
                return this.MachineMaster.Machine;
            }
        }
        public string LotNo
        {
            get
            {
                return this.MachineMaster.LotNo;
            }
        }
    }
}
