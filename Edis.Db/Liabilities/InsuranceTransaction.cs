using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db.Liabilities
{
    public class InsuranceTransaction
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? PurchasedOn { get; set; }
        [Required]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Individual/purpose
        /// </summary>
        public string EntitiesInsured { get; set; }



        [Required]
        public bool? IsAcquire { get; set; }


        [Required]
        public string Issuer { get; set; }
        [Required]
        public InsuranceType InsuranceType { get; set; }
        [Required]
        public PolicyType PolicyType { get; set; }
        [Required]
        public string PolicyNumber { get; set; }
        [Required]
        public string NameOfPolicy { get; set; }
        [Required]
        public string PolicyAddress { get; set; }
        [Required]
        public double? AmountInsured { get; set; }
        [Required]
        public double? Premium { get; set; }


        [Required]
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }





    }
}
