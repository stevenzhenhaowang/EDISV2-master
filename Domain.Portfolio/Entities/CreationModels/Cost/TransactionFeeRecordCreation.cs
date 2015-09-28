using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Cost
{
    /// <summary>
    /// This fee class is only used by transaction creation models as additional expenses, and should not be used by itself.
    /// </summary>
    public class TransactionFeeRecordCreation
    {
        public TransactionExpenseType TransactionExpenseType { get; set; }
        public double Amount { get; set; }
    }
}
