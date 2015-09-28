using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Entities.CreationModels.Income
{
    public class DividendPaymentCreation : IncomeCreationBase
    {

        public string Ticker { get; set; }
        public double Amount { get; set; }
        public double Franking { get; set; }
        public DateTime PaymentOn { get; set; }
    }
}
