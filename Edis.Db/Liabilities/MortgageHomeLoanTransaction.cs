using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Edis.Db.Assets;
using Shared;

namespace Edis.Db.Liabilities
{
    public class MortgageHomeLoanTransaction
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? LoanAquiredOn { get; set; }
        [Required]
        [ForeignKey("PropertyId")]
        public virtual Property CorrespondingProperty { get; set; }
        public string PropertyId { get; set; }
        [Required]
        public double? LoanAmount { get; set; }
        [Required]
        public virtual ICollection<LiabilityRate> InterestRate { get; set; }
        [Required]
        public DateTime? LoanExpiryDate { get; set; }
        [Required]
        public LoanRepaymentType LoanRepaymentType { get; set; }
        [Required]
        public string Institution { get; set; }
        [Required]
        public TypeOfMortgageRates TypeOfMortgageRates { get; set; }

        [Required]
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public string AccountId { get; set; }


        [Required]
        public bool? IsAcquire { get; set; }



    }
}
