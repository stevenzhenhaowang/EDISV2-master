using System;
using System.ComponentModel.DataAnnotations;
using Edis.Db.Assets;


namespace Edis.Db.IncomeRecords
{
    public class Dividend
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? PaymentOn { get; set; }
        [Required]
        public double? Amount { get; set; }
        [Required]
        public double? FrankingCredit { get; set; }
        [Required]
        public virtual Equity Equity { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
    }
}