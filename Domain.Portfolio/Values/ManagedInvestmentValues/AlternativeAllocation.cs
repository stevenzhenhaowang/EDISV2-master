using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.ManagedInvestmentValues
{
    public class AlternativeAllocation : ValueBase
    {
        public double HedgeFund { get; set; }
        public double OtherFund { get; set; }
    }
}