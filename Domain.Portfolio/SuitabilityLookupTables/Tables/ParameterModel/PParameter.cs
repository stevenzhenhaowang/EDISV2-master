using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel
{
    public class PParameter : ISuitabilityCurrentParameter, ISuitabilityForecastParameter
    {
        public double CurrentAverageAgeOfClient { get; set; }
        public double YearsToRetirement { get; set; }
        public double PropertyLeverage { get; set; }
        public double AbilityToPayAboveCurrentInterestRate { get; set; }
        public double ScoreRanking { get; set; }
        public double Total { get; set; }
    }
}