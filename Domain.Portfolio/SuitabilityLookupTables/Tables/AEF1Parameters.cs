using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class AEF1Parameters
    {
        public AEForecastParameter Defensive { get; set; }
        public AEForecastParameter Conservative { get; set; }
        public AEForecastParameter Balance { get; set; }
        public AEForecastParameter Assertive { get; set; }
        public AEForecastParameter Aggressive { get; set; }
        public AEForecastParameter Increment { get; set; }
    }
}