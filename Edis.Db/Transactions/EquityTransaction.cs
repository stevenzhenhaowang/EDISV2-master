using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.Assets;


namespace Edis.Db.Transactions
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
        [ForeignKey("EquityId")]
        public virtual Equity Equity { get; set; }

        public string EquityId { get; set; }


        [Required]
        public DateTime? TransactionDate { get; set; }



    }
}