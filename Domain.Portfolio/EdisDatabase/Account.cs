using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.IncomeRecords;
using Database.Transactions;

namespace Database
{
    public class Account
    {
        [Key]
        public string AccountId { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        public virtual ICollection<BondTransaction> BondTransactions { get; set; }
        public virtual ICollection<CashTransaction> CashTransactions { get; set; }
        public virtual ICollection<EquityTransaction> EquityTransactions { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactions { get; set; }
        public virtual ICollection<CouponPayment> FixedIncomePayments { get; set; }
        public virtual ICollection<Interest> CashAndTermDepositPayments { get; set; }
        public virtual ICollection<Dividend> EquityPayments { get; set; }
        public virtual ICollection<Rental> DirectPropertyPayments { get; set; }

    }
}