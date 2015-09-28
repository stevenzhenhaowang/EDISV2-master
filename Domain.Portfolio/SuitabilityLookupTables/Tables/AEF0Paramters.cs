using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class AEF0Paramters
    {
        public AECurrentParameter Defensive { get; set; }
        public AECurrentParameter Conservative { get; set; }
        public AECurrentParameter Balance { get; set; }
        public AECurrentParameter Assertive { get; set; }
        public AECurrentParameter Aggressive { get; set; }
        public AECurrentParameter MaxScore { get; set; }
        public AECurrentParameter Increment { get; set; }
    }
}