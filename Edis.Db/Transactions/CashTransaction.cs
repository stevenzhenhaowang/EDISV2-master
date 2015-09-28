using System;
using System.ComponentModel.DataAnnotations;
using Edis.Db.Assets;
using System.ComponentModel.DataAnnotations.Schema;


namespace Edis.Db.Transactions
{
    public class CashTransaction
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        [ForeignKey("CashAccountId")]
        public virtual CashAccount CashAccount { get; set; }

        public string CashAccountId { get; set; }

        [Required]
        public double? Amount { get; set; }
        [Required]
        public DateTime? TransactionDate { get; set; }



    }
}