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
    }
}
