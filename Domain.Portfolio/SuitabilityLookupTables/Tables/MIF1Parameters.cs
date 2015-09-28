using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class MIF1Parameters
    {
        public MIForecastParameter Defensive { get; set; }
        public MIForecastParameter Conservative { get; set; }
        public MIForecastParameter Balance { get; set; }
        public MIForecastParameter Assertive { get; set; }
        public MIForecastParameter Aggressive { get; set; }
        public MIForecastParameter MaxScore { get; set; }
        public MIForecastParameter Increment { get; set; }
    }
}