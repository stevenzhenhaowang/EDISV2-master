using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Entities.CreationModels.Income
{
    public class CouponPaymentCreation : IncomeCreationBase
    {

        public string Ticker { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentOn { get; set; }
    }
}
