using System;
using System.ComponentModel.DataAnnotations;
using EdisDatabase.Assets;

namespace EdisDatabase.Transactions
{
    public class EquityTransaction
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }

        [Required]
        public int? NumberOfUnits { get; set; }

        [Required]
        public double? UnitPriceAtPurchase { get; set; }

        [Required]
        public virtual Equity Equity { get; set; }
    }
}