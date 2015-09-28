using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class InsuranceTransactionCreation : TransactionCreationBase
    {
        public string EntitiesInsured { get; set; }
        public string Issuer { get; set; }
        public InsuranceType InsuranceType { get; set; }
        public PolicyType PolicyType { get; set; }
        public string NameOfPolicy { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyAddress { get; set; }
        public double AmountInsured { get; set; }
        public double Premium { get; set; }
        public DateTime GrantedOn { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsAcquire { get; set; }



    }
}
