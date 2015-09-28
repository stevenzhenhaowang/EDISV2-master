using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db.ExpenseRecords
{
    public class ConsultancyExpense
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? IncurredOn { get; set; }
        [Required]
        public double? Amount { get; set; }
        [Required]
        public ConsultancyExpenseType ConsultancyExpenseType { get; set; }

        public string Notes { get; set; }
        [Required]
        [ForeignKey("AdviserId")]
        public virtual Adviser Adviser{ get; set; }
        [Required]
        public string AdviserId { get; set; }

        [Required]
        [ForeignKey("AccountId")]
        public virtual Account Account{ get; set; }
        [Required]
        public string AccountId { get; set; }


    }
}
