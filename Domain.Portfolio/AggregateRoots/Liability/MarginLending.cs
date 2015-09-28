using System;
using System.Collections.Generic;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;

namespace Domain.Portfolio.AggregateRoots.Liability
{


    public class MarginLending : LiabilityBase
    {

        /// <summary>
        /// Lvr is recorded in security
        /// </summary>
        public List<Security> Securities { get; set; }
        //public double LoanValueRatio { get; set; }
        public double LoanAmount { get; set; }
        public double CurrentInterestRate { get; set; }

        public MarginLending(IRepository repo) : base(repo)
        {
        }


        public override List<ActivityBase> GetActivities(DateTime? beforeDate = null)
        {
            return this._repository.GetMarginLendingActivities(this.Id, beforeDate ?? DateTime.Now).Result;
        }

        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            return this._repository.GetInsuranceActivitiesSync(this.Id, beforeDate ?? DateTime.Now)
                ;
        }
    }
}
