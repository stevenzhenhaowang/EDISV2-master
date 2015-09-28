using System;
using System.Collections.Generic;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Liability
{
    public class Insurance : LiabilityBase
    {


        public InsuranceType InsuranceType { get; set; }
        public PolicyType PolicyType { get; set; }
        public string PolicyName { get; set; }
        public string Issurer { get; set; }
        public string PolicyNumber { get; set; }
        public string NameOfPolicy { get; set; }
        public string PolicyAddress { get; set; }
        public string AssetInsurerd { get; set; }

        public double AmountInsured { get; set; }
        public double Premium { get; set; }


        public Insurance(IRepository repo) : base(repo)
        {
        }


        public override List<ActivityBase> GetActivities(DateTime? beforeDate = null)
        {
            return this._repository.GetInsuranceActivities(this.Id, beforeDate ?? DateTime.Now).Result;
        }

        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            return this._repository.GetInsuranceActivitiesSync(this.Id, beforeDate ?? DateTime.Now);
        }
    }
}
