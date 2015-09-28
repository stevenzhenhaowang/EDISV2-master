using System;
using Domain.Portfolio.Base;

namespace Domain.Portfolio.Entities.Transactions
{
    /// <summary>
    ///     Denote any changes of client asset holdings in an Unit of Work fashion.
    /// </summary>
    public class TransactionBase : EntityBase
    {
        public DateTime TransactionTime { get; set; }
        /// <summary>
        ///For asset transactions, if numberOfUnits is positive, then it's a buy, otherwise it's a sale;
        ///For loan transactions, if numberOfUnits is positive, then it's acquiring loan, otherwise it's a loan transfer/removal;
        /// </summary>
        public int NumberOfUnits { get; set; }
        /// <summary>
        /// For asset transaction, unit price is the actual unit price at the time of purchase;
        /// For liability transaction, unit price is the amount of loan granted as a result of this transaction.
        /// </summary>
        public double AmountPerUnit { get; set; }
    }
}