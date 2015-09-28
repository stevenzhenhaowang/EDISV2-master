using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.Income;
using Domain.Portfolio.Values.Ratios;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Asset
{
    /// <summary>
    ///     Equity represents holding of a particular stock for an account.
    /// </summary>
    public abstract class Equity : AssetBase, IEvaluable
    {
        protected Equity(IRepository repo) : base(repo)
        {
        }


        public string Ticker { get; set; }
        public string Name { get; set; }
        public Ratios F0Ratios { get; set; }
        public Recommendation F1Recommendation { get; set; }
        public string Sector { get; set; }

        public abstract AssetSuitability GetRating();

        public override List<ActivityBase> GetActivities(DateTime? beforeDate=null)
        {
            //return _repository.GetEquityActivitiesForAccount(ClientAccountId, Ticker, beforeDate??DateTime.Now).Result;
            return _repository.GetEquityActivitiesForAccountSync(ClientAccountId, Ticker, beforeDate ?? DateTime.Now);
        }
        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            //return _repository.GetEquityActivitiesForAccount(ClientAccountId, Ticker, beforeDate??DateTime.Now).Result;
            return _repository.GetEquityActivitiesForAccountSync(ClientAccountId, Ticker, beforeDate ?? DateTime.Now);
        }

        public override Cost GetCost()
        {
            return GetCostForTransactionType<EquityTradingTransaction>();
        }
        public override Income GetIncome()
        {
            var totalDividend = GetActivitiesSync().Sum(a => a.Incomes.OfType<DividenRecord>()
                .Sum(i => i.Amount));
            var totalFranking = GetActivitiesSync().Sum(a => a.Incomes.OfType<DividenRecord>()
                .Sum(i => i.Franking));
            var yield = totalDividend/GetTotalMarketValue();
            return new Dividend
            {
                ReceivedAmount = totalDividend,
                FrankingAmount = totalFranking,
                Yield = yield,
                TotalAmount = totalDividend + totalFranking
            };
        }
        protected SuitabilityRating GetRatingScore(double score)
        {
            if (score < 0)
            {
                throw new Exception("Score must be greater or equal to 0");
            }

            if (score >= 200)
            {
                return SuitabilityRating.Danger;
            }
            if (score >= 131)
            {
                return SuitabilityRating.Aggresive;
            }
            if (score >= 111)
            {
                return SuitabilityRating.Assertive;
            }
            if (score >= 91)
            {
                return SuitabilityRating.Assertive;
            }
            if (score >= 70)
            {
                return SuitabilityRating.Conservative;
            }
            return SuitabilityRating.Defensive;
        }
    }
}