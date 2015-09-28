using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class MarginLendingTransactionCreation : TransactionCreationBase
    {
        public List<MarginLendingLoanValueRatio> Ratios { get; set; }
        public DateTime GrantedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double LoanAmount { get; set; }
        public bool IsAcquire { get; set; }
        public double InterestRate { get; set; }
    }

    public class MarginLendingLoanValueRatio
    {
        public string AssetId { get; set; }
        public AssetTypes AssetTypes { get; set; }
        public double Ratio { get; set; }
        public DateTime EffectiveFrom { get; set; }

    }





}
