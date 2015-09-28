using System;

namespace Domain.Portfolio.Internals
{
    public class SellTransactionModel
    {
        public int NumberOfUnitsNeedToSell { get; set; }
        public double Price { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}