using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class ExpenseMaster
    {
       
        public decimal TotalExpense
        {
            get
            {
                decimal Total = Convert.ToDecimal(this.Salary) + Convert.ToDecimal(this.Banking) + Convert.ToDecimal(this.Power) + Convert.ToDecimal(this.Fuel) + Convert.ToDecimal(this.Transportation) + Convert.ToDecimal(this.Others) + Convert.ToDecimal(this.Admin);

                return Decimal.Round(Total, 2);
            }
        }
    }
}
