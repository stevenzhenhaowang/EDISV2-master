using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class AECurrentParameter : ISuitabilityCurrentParameter
    {
        public double EpsGrowth { get; set; }
        public double CurrentMarketCapitalisation { get; set; }
        public double DivYieldF0 { get; set; }
        public double FrankF0 { get; set; }
        public double ROAF0 { get; set; }
        public double ROEF0 { get; set; }
        public double InterestCoverF0 { get; set; }
        public double PEF0 { get; set; }
        public double DebtEquityF0 { get; set; }
        public double BetaFiveYear { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}