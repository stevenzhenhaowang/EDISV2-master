using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class IEF1Parameters
    {
        public IEForecastParameter Defensive { get; set; }
        public IEForecastParameter Conservative { get; set; }
        public IEForecastParameter Balance { get; set; }
        public IEForecastParameter Assertive { get; set; }
        public IEForecastParameter Aggressive { get; set; }
        public IEForecastParameter Increment { get; set; }
    }
}