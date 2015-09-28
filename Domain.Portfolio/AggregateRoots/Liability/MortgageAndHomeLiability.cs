using System;
using System.Collections.Generic;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Liability
{
    public class MortgageAndHomeLiability : LiabilityBase
    {
        /// <summary>
        /// Property is asset, hence any asset related info (e.g. market value, etc.) can be retrieved directly from asset
        /// </summary>
        public DirectProperty Property { get; set; }
        public string LoanProviderInstitution { get; set; }
        public double CurrentPropertyGearingRatio { get; set; }
        public CurrencyType CurrencyType { get; set; }
        public double LoanContractTermInYears { get; set; }
        public LoanRepaymentType LoanRepaymentType { get; set; }
        public TypeOfMortgageRates TypeOfMortgageRates { get; set; }
        public double CurrentFiancialYearInterest { get; set; }
        public MortgageAndHomeLiability(IRepository repo) : base(repo)
        {
        }
        public override List<ActivityBase> GetActivities(DateTime? beforeDate = null)
        {
            return this._repository.GetMortgageLoanActivities(this.Id, beforeDate ?? DateTime.Now).Result;
        }

        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            return this._repository.GetMortgageLoanActivitiesSync(this.Id, beforeDate ?? DateTime.Now);
        }
    }
}
