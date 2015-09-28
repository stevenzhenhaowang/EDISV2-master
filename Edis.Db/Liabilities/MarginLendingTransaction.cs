using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Db.Assets;

namespace Edis.Db.Liabilities
{
    public class MarginLendingTransaction
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? GrantedOn { get; set; }
        [Required]
        public double? LoanAmount { get; set; }
        [Required]
        public DateTime? ExpiryDate { get; set; }
        [Required]
        public virtual ICollection<LoanValueRatio> LoanValueRatios { get; set; }

        [Required]
        public bool? IsAcquire { get; set; }

        [Required]
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }


        [Required]
        public virtual ICollection<LiabilityRate> LiabilityRates { get; set; }


    }
}
