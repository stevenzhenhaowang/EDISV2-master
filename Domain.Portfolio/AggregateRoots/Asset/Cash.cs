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
    public class Cash : AssetBase
    {
        public Cash(IRepository repo) : base(repo)
        {
        }

        public string CashAccountNumber { get; set; }
        public string Bsb { get; set; }
        public string CashAccountName { get; set; }
        public CashAccountType CashAccountType { get; set; }

        /// <summary>
        ///     Maturity date of this cash account, null means NA
        /// </summary>
        public DateTime? MaturityDate { get; set; }

        public double? InterestRate { get; set; }
        public double? AnnualInterest { get; set; }
        public Frequency InterestFrequency { get; set; }

        /// <summary>
        ///     number of months of current term, null means NA
        /// </summary>
        public int? TermOfRatesMonth { get; set; }

        /// <summary>
        ///     Face value should be equal to asset cost for cash
        /// </summary>
        public double FaceValue { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public override List<ActivityBase> GetActivities(DateTime? beforeDate =null)
        {
            return _repository.GetCashActivitiesForAccount(ClientAccountId, CashAccountNumber, beforeDate??DateTime.Now).Result;
        }
        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            return _repository.GetCashActivitiesForAccountSync(ClientAccountId, CashAccountNumber, beforeDate ?? DateTime.Now);
        }


        public override Cost GetCost()
        {
            return GetCostForTransactionType<CashAccountTradingTransaction>();
        }

        public override Income GetIncome()
        {
            return new Interest
            {
                TotalAmount = GetActivitiesSync().Sum(a => a.Incomes.OfType<InterestPaymentRecord>()
                    .Sum(i => i.Amount))
            };
        }
    }
}