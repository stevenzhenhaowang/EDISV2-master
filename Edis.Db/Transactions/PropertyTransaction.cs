using System;
using System.ComponentModel.DataAnnotations;
using Edis.Db.Assets;

namespace Edis.Db.Transactions
{
    public class PropertyTransaction
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public double? Price { get; set; }


        /// <summary>
        /// True for buy, false for sell
        /// </summary>
        [Required]
        public bool? IsBuy { get; set; }


        [Required]
        public virtual Property PropertyAddress { get; set; }
        [Required]
        public DateTime? TransactionDate { get; set; }



    }
}