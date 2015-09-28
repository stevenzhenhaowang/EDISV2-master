using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.ManagedInvestmentValues
{
    public class SuitabilityAllocation : ValueBase
    {
        public double AggressiveAllocation { get; set; }
        public double BalancedAllocation { get; set; }
        public double ConservativeAllocation { get; set; }
        public double ModerateAllocation { get; set; }
    }
}