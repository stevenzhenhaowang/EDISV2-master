using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.Income;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Asset
{
    public class FixedIncome : AssetBase
    {
        public FixedIncome(IRepository repo) : base(repo)
        {
        }

        public string Ticker { get; set; }
        public string Issuer { get; set; }
        public string FixedIncomeName { get; set; }
        public double? CouponRate { get; set; }
        public Frequency CouponFrequency { get; set; }
        public BondDetails BoundDetails { get; set; }
        public string BondType { get; set; }

        public override List<ActivityBase> GetActivities(DateTime? beforeDate=null)
        {
            return _repository.GetFixedIncomeActivitiesForAccount(ClientAccountId, Ticker, beforeDate??DateTime.Now).Result;
        }

        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)       //added
        {
            return _repository.GetFixedIncomeActivitiesForAccountSync(ClientAccountId, Ticker, beforeDate ?? DateTime.Now);
        }

        public override Cost GetCost()
        {
            return GetCostForTransactionType<BondTradingTransaction>();
        }
        public override Income GetIncome()
        {
            return new Coupon
            {
                TotalAmount = GetActivitiesSync().Sum(a => a.Incomes.OfType<CouponPaymentRecord>()
                    .Sum(i => i.Amount)),
                Ticker = Ticker
            };
        }
    }
}