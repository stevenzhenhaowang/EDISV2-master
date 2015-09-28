using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class IECurrentParameter : ISuitabilityCurrentParameter
    {
        public double OneYearTotalReturn { get; set; }
        public double CurrentMarketCapitalisatiopn { get; set; }
        public double DividendYield { get; set; }
        public double ROA { get; set; }
        public double ROE { get; set; }
        public double QuickRatio { get; set; }
        public double CurrentRatio { get; set; }
        public double TotalDebtTotalEquityRatio { get; set; }
        public double PERatio { get; set; }
        public double BetaFiveYear { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}