using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.Assets;


namespace Edis.Db.Transactions
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
        [ForeignKey("BondId")]
        public virtual Bond Bond { get; set; }

        public string BondId { get; set; }



        [Required]
        public DateTime? TransactionDate { get; set; }




    }
}