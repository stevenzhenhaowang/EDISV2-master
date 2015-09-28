using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class CashAccountTransactionAccountCreation : TransactionCreationBase
    {
        public string CashAccountNumber { get; set; }
        public string CashAccountName { get; set; }
        public string Bsb { get; set; }
        public CashAccountType CashAccountType { get; set; }
        public DateTime MaturityDate { get; set; }
        public Frequency Frequency { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public int? TermsInMonths { get; set; }
        public double? InterestRate { get; set; }
        public double? AnnualInterestSoFar { get; set; }
        /// <summary>
        /// Negative amount denotes withdraw
        /// </summary>
        public double Amount { get; set; }//will also be face value
        public DateTime TransactionDate { get; set; }
        public List<TransactionFeeRecordCreation> TransactionFeeRecords { get; set; }



    }
}
