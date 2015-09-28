using System;
using System.ComponentModel.DataAnnotations;
using EdisDatabase.Assets;

namespace EdisDatabase.Transactions
{
    public class CashTransaction
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public CashAccount CashAccount { get; set; }
        [Required]
        public double? Amount { get; set; }




    }
}