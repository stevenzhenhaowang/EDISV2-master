using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    /// <summary>
    /// Marker for all transaction creation models.
    /// Please use 'Navigate to implementation' for implementation classes
    /// </summary>
    public abstract class TransactionCreationBase
    {
        public string AccountNumber { get; internal set; }

    }
}
