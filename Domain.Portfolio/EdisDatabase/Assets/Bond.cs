using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.IncomeRecords;
using Database.Markers;
using Database.Transactions;
using Shared;

namespace Database.Assets
{
    public class Bond : IAsset
    {
        [Key]
        public string BondId { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public Frequency Frequency { get; set; }
        [Required]
        public string BondType { get; set; }
        [Required]
        public string Issuer { get; set; }

        public virtual ICollection<CouponPayment> CouponPayments { get; set; }
        public virtual ICollection<BondTransaction> BondTransactions { get; set; }
    }
}