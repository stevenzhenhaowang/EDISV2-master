using System.ComponentModel;

namespace Shared
{
    public enum LiabilityTypes
    {
        [Description("Mortgage and Home Loan")] MortgageAndHomeLoan = 1,
        [Description("Margin Lending")] MarginLending = 2,
        //[Description("Personal & Credit Card Loan")] PersonalAndCreditCardLoan = 3,
        [Description("Insurance")] Insurance = 4
    }
}