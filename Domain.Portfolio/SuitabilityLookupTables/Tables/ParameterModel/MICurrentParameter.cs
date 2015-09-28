using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class MICurrentParameter : ISuitabilityCurrentParameter
    {
        public double FiveYearTotalReturn { get; set; }
        public double FiveYearAlphaRatio { get; set; }
        public double FiveYearBeta { get; set; }
        public double FiveYearInformationRatio { get; set; }
        public double FiveYearStandardDeviation { get; set; }
        public double FiveYearSkewnessRatio { get; set; }
        public double FiveYearTrackingErrorRatio { get; set; }
        public double FiveYearSharpRatio { get; set; }
        public double GlobalCategory { get; set; }
        public double FundSize { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}