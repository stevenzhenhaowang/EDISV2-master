using System.Collections.Generic;

namespace Domain.Portfolio.Internals
{
    public class TransactionPosition
    {
        public List<BuyTransactionModel> Buys { get; set; }
        public List<SellTransactionModel> Sells { get; set; }
        public double CapitalGain { get; set; }
    }
}