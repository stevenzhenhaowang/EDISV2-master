using System;
using System.ComponentModel.DataAnnotations;
using EdisDatabase.Assets;

namespace EdisDatabase.IncomeRecords
{
    public class CouponPayment
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime? PaymentOn { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public virtual Bond Bond { get; set; }
    }
}