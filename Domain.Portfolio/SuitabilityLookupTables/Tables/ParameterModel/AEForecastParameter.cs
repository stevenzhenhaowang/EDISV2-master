using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class AEForecastParameter : ISuitabilityForecastParameter
    {
        public double EpsGrowth { get; set; }
        public double MorningStarRecommandation { get; set; }
        public double DivYieldF1 { get; set; }
        public double FrankF1 { get; set; }
        public double ROAF1 { get; set; }
        public double ROEF1 { get; set; }
        public double InterestCoverF1 { get; set; }
        public double PEF1 { get; set; }
        public double DebtEquityF1 { get; set; }
        public double IntrsicValueVariation { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}