using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;

namespace Domain.Portfolio.AggregateRoots
{
    public abstract class LiabilityBase : AggregateRootBase
    {

        public DateTime ExpiryDate { get; set; }
        public DateTime GrantedOn { get; set; }
        /// <summary>
        /// The amount of money still owned
        /// </summary>
        public double CurrentBalance { get; set; }



        public abstract List<ActivityBase> GetActivities(DateTime? beforeDate = null);
        public abstract List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null);

        /// <summary>
        /// Cost for liability is primarily expense, this include all loan interests that may be paid and any associated fees.
        /// </summary>
        /// <returns></returns>
        public Cost GetCost()
        {
            var activities = this.GetActivitiesSync();
            return new Cost()
            {
                AssetCost = 0,
                Expense = activities.Sum(ac => ac.Expenses.Sum(ex => ex.Amount)),
                Total = activities.Sum(ac => ac.Expenses.Sum(ex => ex.Amount))
            };
        }

        protected LiabilityBase(IRepository repo) : base(repo)
        {
        }
    }
}
