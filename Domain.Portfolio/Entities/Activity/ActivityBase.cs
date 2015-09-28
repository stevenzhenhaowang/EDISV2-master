using System;
using System.Collections.Generic;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.CostRecord;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Entities.Transactions;

namespace Domain.Portfolio.Entities.Activity
{
    /// <summary>
    ///     Activity denotes any behavior that will influence our domain.
    ///     Activity will always produce at least one of the following side-affects:
    ///     incur costs, generate transactions, generate revenues, and increase/decrease/change holdings/loans.
    /// </summary>
    public abstract class ActivityBase : EntityBase
    {
        public DateTime ActivityDate { get; set; }
        /// <summary>
        ///     Any expenses that are incurred during this activity. 
        /// In most cases, if there are transactions resulted 
        /// from this activity, there will also be corresponding expenses.
        /// </summary>
        public List<FinancialActivityCostRecord> Expenses { get; set; }
        /// <summary>
        ///     Any transactions that have been created as a result of this activity. 
        /// In most cases, this collection will
        /// only contain one item, represents the core outcome of an activity.
        /// </summary>
        public List<TransactionBase> Transactions { get; set; }
        /// <summary>
        ///     Any incomes that are generated as a result of this activity. In most cases, 
        /// incomes alone are sufficient to
        /// represent an activity, and hence no expenses/transactions involved in 
        /// such activities (income generation activity).
        /// </summary>
        public List<IncomeRecordBase> Incomes { get; set; }
    }
}