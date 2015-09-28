using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.ManagedInvestmentValues
{
    public class FundAllocation : ValueBase
    {
        public SuitabilityAllocation SuitabilityAllocation { get; set; }
        public AlternativeAllocation AlternativeAllocation { get; set; }
        public EquityAllocation EquityAllocation { get; set; }
        public FixedIncomeAllocation FixedIncomeAllocation { get; set; }
        public PropertyAllocation PropertyAllocation { get; set; }
        public double Total { get; set; }
    }
}