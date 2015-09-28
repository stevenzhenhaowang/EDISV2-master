using Domain.Portfolio.Base;

namespace Domain.Portfolio.Entities.CostRecord
{
    public abstract class CostRecordBase : EntityBase
    {
        public double Amount { get; set; }
    }
}