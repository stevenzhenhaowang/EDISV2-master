using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class MIF0Parameters
    {
        public MICurrentParameter Defensive { get; set; }
        public MICurrentParameter Conservative { get; set; }
        public MICurrentParameter Balance { get; set; }
        public MICurrentParameter Assertive { get; set; }
        public MICurrentParameter Aggressive { get; set; }
        public MICurrentParameter MaxScore { get; set; }
        public MICurrentParameter Increment { get; set; }
    }
}