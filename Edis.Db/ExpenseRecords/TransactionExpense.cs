using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.Enums;
using Shared;

namespace Edis.Db.ExpenseRecords
{
    public class TransactionExpense
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public DateTime? IncurredOn { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public double? Amount { get; set; }
        [Required]
        public TransactionExpenseType ExpenseType { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        /// <summary>
        /// For asset transactions, this id will be the corresponding transaction id,
        /// For liability, since there's no transaction table, this id is the corresponding liability item id
        /// E.g. id of Insurance, id of MarginLending, etc.
        /// </summary>
        [Required]
        public string CorrespondingTransactionId { get; set; }

        /// <summary>
        /// Optional receiver of this expense
        /// </summary>
        [ForeignKey("AdviserId")]
        public virtual Adviser Adviser { get; set; }
        public string AdviserId { get; set; }
    }
}
