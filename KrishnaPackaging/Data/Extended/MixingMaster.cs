using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class MixingMaster
    {
        public string Item { get { return this.ItemMaster.Name; } }
        public string Inward { get { return this.InwardMaster.InwNo; } }

    }
}
