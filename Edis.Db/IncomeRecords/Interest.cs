using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.Assets;


namespace Edis.Db.IncomeRecords
{
    public class Interest
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime? PaymentOn { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public virtual CashAccount CashAccount { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }





    }
}