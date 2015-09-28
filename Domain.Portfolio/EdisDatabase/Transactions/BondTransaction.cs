using System;
using System.ComponentModel.DataAnnotations;
using Database.Assets;
using Shared;

namespace Database.Transactions
{
    public class BondTransaction
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
        public virtual Bond Bond { get; set; }





    }
}