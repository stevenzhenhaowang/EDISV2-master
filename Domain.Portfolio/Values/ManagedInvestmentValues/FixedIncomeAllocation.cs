using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.ManagedInvestmentValues
{
    public class FixedIncomeAllocation : ValueBase
    {
        public double AustraliaFixedIncome { get; set; }
        public double GlobalFixedIncome { get; set; }
        public double HighYieldFixedIncome { get; set; }
    }
}