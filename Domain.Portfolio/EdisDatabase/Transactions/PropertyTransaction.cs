using System;
using System.ComponentModel.DataAnnotations;
using Database.Assets;

namespace Database.Transactions
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
    }
}