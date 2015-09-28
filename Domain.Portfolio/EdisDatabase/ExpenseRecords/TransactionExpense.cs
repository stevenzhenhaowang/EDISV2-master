using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Enums;
using Shared;

namespace Database.ExpenseRecords
{
    public class TransactionExpense
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public double? Amount { get; set; }
        [Required]
        public TransactionExpenseType ExpenseType { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        [Required]
        public string CorrespondingTransactionId { get; set; }
    }
}
