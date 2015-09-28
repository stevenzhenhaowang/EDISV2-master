using System.ComponentModel;

namespace Shared
{
    public enum SuitabilityRating
    {
        [Description("Defensive")] Defensive = 1,
        [Description("Conservative")] Conservative = 2,
        [Description("Balance")] Balance = 3,
        [Description("Assertive")] Assertive = 4,
        [Description("Aggressive")] Aggresive = 5,
        [Description("Danger - Not on APL")] Danger = 6
    }
}