using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.ManagedInvestmentValues
{
    public class EquityAllocation : ValueBase
    {
        public double AustraliaEquity { get; set; }
        public double Asia { get; set; }
        public double EmergingMarketsEquity { get; set; }
        public double EuropeEquity { get; set; }
        public double GlobalEquity { get; set; }
        public double GlobalEquityLargeCap { get; set; }
        public double GlobalEquityMidSmallCap { get; set; }
        public double OtherSectorEquity { get; set; }
        public double RealEstateSectorEquity { get; set; }
        public double TechnologySectorEquity { get; set; }
        public double UsEquityLargeCapBlend { get; set; }
    }
}