using System.ComponentModel;

namespace Shared
{
    public enum AssetTypes
    {
        [Description("Australian Equity")]
        AustralianEquity = 1,
        [Description("International Equity")]
        InternationalEquity = 2,
        [Description("Managed Investments")]
        ManagedInvestments = 3,

        [Description("Direct And Listed Property")]
        DirectAndListedProperty = 4,

        [Description("Fixed Income Investments")]
        FixedIncomeInvestments = 6,
        [Description("Cash and Term Deposit")]
        CashAndTermDeposit = 7
    }
}