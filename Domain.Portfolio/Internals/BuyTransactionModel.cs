using System;

namespace Domain.Portfolio.Internals
{
    public class BuyTransactionModel
    {
        public int NumberOfUnitsLeft { get; set; }
        public double Price { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}