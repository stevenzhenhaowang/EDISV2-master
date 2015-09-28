using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.Income;
namespace Domain.Portfolio.AggregateRoots
{
    /// <summary>
    ///     Asset is relative, it will always be carried by an owner via accounts, and hence it's identity is the combination of it's ID and
    ///     account ID.
    /// It will be possible for the same owner having multiple accounts, and carrying the same assets with these different accounts, 
    /// if this is the case, even if assets from two accounts share the same assetId, they still represent different assets because 
    /// each has its own progression path.
    /// However, it will be meaningful for the same asset IDed assets combined to statistic results, and this has been supported.
    /// Notes: If distinctive asset statistics is required, use groupBy asset.id to get such results.
    /// </summary>
    public abstract class AssetBase : AggregateRootBase, IWeightable
    {
        protected AssetBase(IRepository repo) : base(repo)
        {
        }

        public string ClientAccountId { get; set; }
        public double TotalNumberOfUnits { get; set; }
        public double LatestPrice { get; set; }
        /// <summary>
        ///     Get all activities before asset.todate
        /// </summary>
        /// <returns></returns>
        public abstract List<ActivityBase> GetActivities(DateTime? beforeDate=null);

        public abstract List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null);
        /// <summary>
        ///     Every asset may have some sort of costs associated
        /// </summary>
        /// <returns></returns>
        public abstract Cost GetCost();
        /// <summary>
        ///     Every asset may generate some sort of income
        /// </summary>
        /// <returns> Any forms of income, might be dividend for equity, or fixed-income for term deposit, etc.</returns>
        public abstract Income GetIncome();
        public double GetTotalMarketValue()
        {
            return TotalNumberOfUnits * LatestPrice;
        }
        public double GetCapitalGain()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Cost of asset is the sum of asset cost and relevant expenses.
        /// </summary>
        /// <typeparam name="TTransactionType"></typeparam>
        /// <returns></returns>
        protected Cost GetCostForTransactionType<TTransactionType>()
            where TTransactionType : TransactionBase
        {
            var position =
                GetActivitiesSync()
                    .SelectMany(a => a.Transactions)
                    .ToList()
                    .CalculateCurrentTransactionHoldings<TTransactionType>();
            var assetCost = position.Buys.Sum(t => t.NumberOfUnitsLeft * t.Price);

            var expense = GetActivitiesSync().Sum(a => a.Expenses.Sum(e => e.Amount));
            return new Cost
            {
                AssetCost = assetCost,
                Expense = expense,
                Total = assetCost + expense
            };
        }
    }
}