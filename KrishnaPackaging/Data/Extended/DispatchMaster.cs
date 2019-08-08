using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class DispatchMaster
    {
        public string Party { get { return this.PartyMaster.PartyName; } }
    }
}
