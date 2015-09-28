using Domain.Portfolio.Entities.Transactions;
using Shared;


namespace Domain.Portfolio.Entities.CostRecord
{
    public class FinancialActivityCostRecord : CostRecordBase
    {
        public TransactionExpenseType ActivityCostType { get; set; }
        /// <summary>
        /// Transaction can be of any types from asset purchase to loan grant.
        /// </summary>
        public TransactionBase Transaction { get; set; }
    }
}