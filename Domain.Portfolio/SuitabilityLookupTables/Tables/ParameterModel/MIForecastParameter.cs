using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class MIForecastParameter : ISuitabilityForecastParameter
    {
        public double OneYearTotalReturn { get; set; }
        public double MorningStarAnalyst { get; set; }
        public double OneYearAlpha { get; set; }
        public double OneYearBeta { get; set; }
        public double OneYearInformationRatio { get; set; }
        public double OneYearTrackingError { get; set; }
        public double OneYearSharpRatio { get; set; }
        public double MaxManagementExpenseRatio { get; set; }
        public double PerformanceFee { get; set; }
        public double YearsSinceInception { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}