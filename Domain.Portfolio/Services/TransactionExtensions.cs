using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Internals;

namespace Domain.Portfolio.Services
{
    public static class TransactionExtensions
    {
        public static TransactionPosition CalculateCurrentTransactionHoldings<TTransactionType>(
            this List<TransactionBase> trs)
            where TTransactionType : TransactionBase
        {
            var transactions = trs.OfType<TTransactionType>().OrderBy(t => t.TransactionTime).ToList();
            var buys = transactions.Where(t => t.NumberOfUnits > 0).Select(t =>
                new BuyTransactionModel
                {
                    NumberOfUnitsLeft = t.NumberOfUnits,
                    Price = t.AmountPerUnit,
                    TransactionTime = t.TransactionTime
                }).ToList();
            var sells = transactions.Where(s => s.NumberOfUnits < 0).Select(t =>
                new SellTransactionModel
                {
                    NumberOfUnitsNeedToSell = t.NumberOfUnits,
                    Price = t.AmountPerUnit,
                    TransactionTime = t.TransactionTime
                }).ToList();


            double result = 0;
            foreach (var sell in sells)
            {
                var priorBuys = buys.Where(b => b.TransactionTime <= sell.TransactionTime).ToList();
                if (priorBuys.Sum(b => b.NumberOfUnitsLeft) < sell.NumberOfUnitsNeedToSell)
                {
                    throw new Exception(
                        "A sell transaction is selling more assets than the quantity of assets currently owned. " +
                        "Please check if you have properly retrieved correct collection of assets.");
                }
                var numberOfUnitsNeedsTobeSold = sell.NumberOfUnitsNeedToSell;
                foreach (var priorBuy in priorBuys)
                {
                    if (priorBuy.NumberOfUnitsLeft >= numberOfUnitsNeedsTobeSold)
                    {
                        priorBuy.NumberOfUnitsLeft -= numberOfUnitsNeedsTobeSold;
                        var soldValue = sell.Price*numberOfUnitsNeedsTobeSold;
                        var costValue = priorBuy.Price*numberOfUnitsNeedsTobeSold;
                        result += (soldValue - costValue);
                        numberOfUnitsNeedsTobeSold -= numberOfUnitsNeedsTobeSold;
                    }
                    else
                    {
                        var unitsSoldThisRound = priorBuy.NumberOfUnitsLeft;
                        var soldValue = sell.Price*unitsSoldThisRound;
                        var costValue = priorBuy.Price*unitsSoldThisRound;
                        result += (soldValue - costValue);
                        priorBuy.NumberOfUnitsLeft -= unitsSoldThisRound;
                        numberOfUnitsNeedsTobeSold -= unitsSoldThisRound;
                    }
                }
            }
            return new TransactionPosition
            {
                Sells = sells,
                Buys = buys,
                CapitalGain = result
            };
        }
    }
}