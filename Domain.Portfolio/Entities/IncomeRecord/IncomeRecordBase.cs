using System;
using Domain.Portfolio.Base;

namespace Domain.Portfolio.Entities.IncomeRecord
{
    public abstract class IncomeRecordBase : EntityBase
    {
        public double Amount { get; set; }
        public DateTime RecordTime { get; set; }
    }
}