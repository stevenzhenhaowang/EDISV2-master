using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class IEForecastParameter : ISuitabilityForecastParameter
    {
        public double FiveYearTotalReturn { get; set; }
        public double MorningStarRecommendation { get; set; }
        public double DividendYield { get; set; }
        public double ROAF1 { get; set; }
        public double ROEF1 { get; set; }
        public double FinancialLeverageF1 { get; set; }
        public double OneYearRevenueGrowthF1 { get; set; }
        public double DERatioF1 { get; set; }
        public double CreditRating { get; set; }
        public double FairValueVariation { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}