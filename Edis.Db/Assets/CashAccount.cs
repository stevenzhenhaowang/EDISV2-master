using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


using Edis.Db.IncomeRecords;
using Edis.Db.Markers;
using Edis.Db.Transactions;

using Shared;

namespace Edis.Db.Assets
{
    public class CashAccount : IAsset
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Bsb { get; set; }

        [Required]
        public string AccountName { get; set; }
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public CashAccountType CashAccountType { get; set; }

        public DateTime? MaturityDate { get; set; }
        public Frequency Frequency { get; set; }
        public CurrencyType CurrencyType { get; set; }
        /// <summary>
        /// Number of months
        /// </summary>
        public int? TermsInMonths { get; set; }
        /// <summary>
        /// Percentage
        /// </summary>
        public double? InterestRate { get; set; }
        /// <summary>
        /// In dollars. This seems to be an accumulative field, and should be 
        /// used in a transactional fashion.
        /// TODO: Confirm this field can be used as an editable field rather a list of transactions.
        /// </summary>
        public double? AnnualInterest { get; set; }

        [Required]
        public double? FaceValue { get; set; }
        public virtual ICollection<CashTransaction> CashTransactions { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }

    }
}