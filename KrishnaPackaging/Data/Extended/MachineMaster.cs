using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrishnaPackaging.Data
{
    public partial class MachineMaster
    {
        public decimal Stock
        {
            get
            {
                decimal QTY = 0;
                if (this.ProductionDetails.Where(w => w.IsDelete != true).Count() > 0)
                {
                    QTY = Convert.ToDecimal(this.ProductionDetails.Where(w => w.IsDelete != true).Sum(s => s.Production));
                }
                decimal IssuQTY = 0;
                if (this.DispatchDetails.Where(w => w.IsDelete != true).Count() > 0)
                {
                    IssuQTY = Convert.ToDecimal(this.DispatchDetails.Where(w => w.IsDelete != true).Sum(s => s.Qty));
                }
                return Decimal.Round(QTY - IssuQTY, 2);
            }
            set { }
        }

        public decimal Costing
        {
            get
            {
                decimal QTY = 0;
                if (this.ConsumptionMasters.Where(w => w.IsDelete != true).Count() > 0)
                {
                    QTY = Convert.ToDecimal(this.ConsumptionMasters.AsEnumerable().Where(w => w.IsDelete != true).Sum(s => s.ConsumptionDetails.Where(w => w.IsDelete == false).Sum(r => r.Rate * r.Qty)));
                    // data
                }
                decimal IssuQTY = 0;
                if (this.ProductionDetails.Where(w => w.IsDelete != true).Count() > 0)
                {
                    IssuQTY = Convert.ToDecimal(this.ProductionDetails.Where(w => w.IsDelete != true).Sum(s => s.Production));
                }
                decimal Expence = 0;
                if (this.ExpenseDetails.Where(w => w.IsDelete != true).Count() > 0)
                {
                    Expence = this.ExpenseDetails.Where(w => w.IsDelete != true).Sum(s => Convert.ToDecimal(s.Amount));
                }
                decimal Waste = 0;
                if (this.WasteMasters.Where(w => w.IsDelete != true).Count() > 0)
                {
                    Waste = this.WasteMasters.Where(w => w.IsDelete != true).Sum(s => Convert.ToDecimal(s.Amount));
                }
                decimal cos = 0;
                if (QTY != 0 && IssuQTY != 0)
                {
                    cos = Decimal.Round((QTY + Expence - Waste) / IssuQTY, 2);
                }

                return cos;
            }
            set { }
        }
        public bool IsExpense { get; set; }

        public decimal Amount
        {
            get
            {
                decimal QTY = Stock * Costing;
                return Decimal.Round(QTY, 2);
            }
            set { }
        }
    }
}
