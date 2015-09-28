using System.ComponentModel;

namespace Shared
{
    public enum EquityTypes
    {
        [Description("Australian Equity")] AustralianEquity = 1,
        [Description("International Equity")] InternationalEquity = 2,
        [Description("Managed Investments")] ManagedInvestments = 3
    }
}