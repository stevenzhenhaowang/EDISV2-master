using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db
{
    public class RepaymentRecord
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Grouping key can be account id for margin lending, [property id * account id] for mortgage, or [policy number * policy name] for insurance
        /// </summary>
        [Required]
        public string CorrespondingLiabilityGroupingKey { get; set; }
        [Required]
        public LiabilityTypes LiabilityType { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? PaymentOn { get; set; }

        [Required]
        public double? PrincipleAmount { get; set; }
        [Required]
        public double? InterestAmount { get; set; }


        [Required]
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }


    }
}
