using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Values.Cashflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Services
{
    public static class LiabilitiesExtensions
    {

        public static List<Cashflow> GetMonthlyCashflows(this List<LiabilityBase> liabilities)
        {
            List<ActivityBase> activities = new List<ActivityBase>();
            List<Cashflow> result = new List<Cashflow>();

            foreach (var liability in liabilities)
            {
                activities.AddRange(liability.GetActivitiesSync());
            }

            List<string> months = new List<string>();

            for (int i = 1; i <= 12; i++)
            {
                var time = DateTime.Now.AddMonths(i - 12);
                months.Add(time.ToString("MMM-yyyy"));
            }

            var activityGroups = activities.GroupBy(ac => ac.ActivityDate.ToString("MMM-yyyy"));

            foreach (var monthly in months)
            {
                Cashflow flow = new Cashflow()
                {
                    Expenses = activityGroups.Any(ac => ac.Key == monthly) ? activityGroups.Where(ac => ac.Key == monthly).Sum(ac => ac.Sum(m => m.Expenses.Sum(ex => ex.Amount))) : 0,
                    Income = activityGroups.Any(ac => ac.Key == monthly) ? activityGroups.Where(ac => ac.Key == monthly).Sum(ac => ac.Sum(m => m.Incomes.Sum(inc => inc.Amount))) : 0,
                    Month = monthly.Split('-').First()
                };
                result.Add(flow);
            }
            return result;
        }
    }
}
