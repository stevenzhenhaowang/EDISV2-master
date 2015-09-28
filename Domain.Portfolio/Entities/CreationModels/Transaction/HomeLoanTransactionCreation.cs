using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class HomeLoanTransactionCreation : TransactionCreationBase
    {
        public string PropertyId { get; set; }
        public string Institution { get; set; }
        public DateTime GrantedOn { get; set; }
        public bool IsAcquire { get; set; }
        public LoanRepaymentType LoanRepaymentType { get; set; }
        public double LoanAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
        public TypeOfMortgageRates TypeOfMortgageRates { get; set; }
        public double LoanRate { get; set; }
    }
}
