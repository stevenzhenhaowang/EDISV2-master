using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class IEF0Parameters
    {
        public IECurrentParameter Defensive { get; set; }
        public IECurrentParameter Conservative { get; set; }
        public IECurrentParameter Balance { get; set; }
        public IECurrentParameter Assertive { get; set; }
        public IECurrentParameter Aggressive { get; set; }
        public IECurrentParameter MaxScore { get; set; }
        public IECurrentParameter Increment { get; set; }
    }
}