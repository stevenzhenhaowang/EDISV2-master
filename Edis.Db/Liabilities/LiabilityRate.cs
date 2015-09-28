using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.Liabilities
{
    public class LiabilityRate
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public double? Rate { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? EffectiveFrom { get; set; }

        [ForeignKey("MortgageId")]
        public virtual MortgageHomeLoanTransaction MortgageHomeLoan { get; set; }
        public string MortgageId { get; set; }


        [ForeignKey("MarginLendingId")]
        public virtual MarginLendingTransaction MarginLending { get; set; }
        public string MarginLendingId { get; set; }




    }
}
