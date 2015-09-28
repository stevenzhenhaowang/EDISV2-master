using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class InternationalEquitySuitabilityParameters
    {
        public InternationalEquitySuitabilityParameters()
        {
            #region current parameters

            Ief0Parameters = new IEF0Parameters
            {
                Increment = new IECurrentParameter
                {
                    ScoreRanking = 2,
                    BetaFiveYear = 0.2,
                    CurrentMarketCapitalisatiopn = 2000000000,
                    CurrentRatio = 1,
                    DividendYield = 0.01,
                    OneYearTotalReturn = 0.05,
                    PERatio = 3,
                    QuickRatio = 1,
                    ROA = 0.05,
                    ROE = 0.05,
                    TotalDebtTotalEquityRatio = 0.1
                },
                Assertive = new IECurrentParameter
                {
                    DividendYield = 0.03,
                    OneYearTotalReturn = -0.05,
                    PERatio = 19,
                    QuickRatio = 2,
                    ROA = 0.05,
                    ROE = 0.05,
                    TotalDebtTotalEquityRatio = 0.6,
                    ScoreRanking = 8,
                    BetaFiveYear = 1.2,
                    CurrentRatio = 2,
                    CurrentMarketCapitalisatiopn = 3000000000
                },
                Conservative = new IECurrentParameter
                {
                    ScoreRanking = 4,
                    BetaFiveYear = 0.8,
                    ROE = 0.15,
                    CurrentMarketCapitalisatiopn = 7000000000,
                    CurrentRatio = 4,
                    ROA = 0.15,
                    TotalDebtTotalEquityRatio = 0.4,
                    OneYearTotalReturn = 0.05,
                    PERatio = 13,
                    QuickRatio = 4,
                    DividendYield = 0.05
                },
                Defensive = new IECurrentParameter
                {
                    CurrentRatio = 5,
                    DividendYield = 0.06,
                    OneYearTotalReturn = 0.1,
                    PERatio = 10,
                    QuickRatio = 5,
                    ROA = 0.2,
                    TotalDebtTotalEquityRatio = 0.3,
                    ScoreRanking = 2,
                    BetaFiveYear = 0.6,
                    ROE = 0.2,
                    CurrentMarketCapitalisatiopn = 9000000000
                },
                Aggressive = new IECurrentParameter
                {
                    ScoreRanking = 8,
                    BetaFiveYear = 1.2,
                    ROE = 0.05,
                    CurrentMarketCapitalisatiopn = 3000000000,
                    QuickRatio = 2,
                    ROA = 0.05,
                    TotalDebtTotalEquityRatio = 0.6,
                    OneYearTotalReturn = -0.05,
                    PERatio = 19,
                    CurrentRatio = 2,
                    DividendYield = 0.03
                },
                Balance = new IECurrentParameter
                {
                    CurrentRatio = 3,
                    DividendYield = 0.04,
                    OneYearTotalReturn = 0,
                    PERatio = 16,
                    QuickRatio = 3,
                    ROA = 0.1,
                    TotalDebtTotalEquityRatio = 0.5,
                    ScoreRanking = 6,
                    BetaFiveYear = 1,
                    ROE = 0.1,
                    CurrentMarketCapitalisatiopn = 5000000000
                },
                MaxScore = new IECurrentParameter
                {
                    ScoreRanking = 100,
                    BetaFiveYear = 10,
                    ROE = 10,
                    CurrentMarketCapitalisatiopn = 10,
                    QuickRatio = 10,
                    ROA = 10,
                    TotalDebtTotalEquityRatio = 10,
                    OneYearTotalReturn = 10,
                    PERatio = 10,
                    DividendYield = 10,
                    CurrentRatio = 10
                }
            };

            #endregion

            #region forecast parameters

            Ief1Parameters = new IEF1Parameters
            {
                Defensive = new IEForecastParameter
                {
                    DividendYield = 0.06,
                    ScoreRanking = 2,
                    ROEF1 = 0.2,
                    ROAF1 = 0.2,
                    DERatioF1 = 0.3,
                    FairValueVariation = 0.2,
                    FinancialLeverageF1 = 0.1,
                    FiveYearTotalReturn = 0.1,
                    OneYearRevenueGrowthF1 = 0.2
                },
                Conservative = new IEForecastParameter
                {
                    DERatioF1 = 0.4,
                    FairValueVariation = 0.1,
                    FinancialLeverageF1 = 0.2,
                    FiveYearTotalReturn = 0.05,
                    OneYearRevenueGrowthF1 = 0.1,
                    ScoreRanking = 4,
                    DividendYield = 0.05,
                    ROEF1 = 0.15,
                    ROAF1 = 0.15
                },
                Assertive = new IEForecastParameter
                {
                    ScoreRanking = 8,
                    DividendYield = 0.03,
                    ROEF1 = 0.05,
                    ROAF1 = 0.05,
                    FairValueVariation = -0.1,
                    FinancialLeverageF1 = 0.4,
                    FiveYearTotalReturn = -0.05,
                    OneYearRevenueGrowthF1 = -0.1,
                    DERatioF1 = 0.6
                },
                Balance = new IEForecastParameter
                {
                    DERatioF1 = 0.5,
                    FairValueVariation = 0,
                    FinancialLeverageF1 = 0.3,
                    FiveYearTotalReturn = 0,
                    OneYearRevenueGrowthF1 = 0,
                    ScoreRanking = 6,
                    DividendYield = 0.04,
                    ROEF1 = 0.1,
                    ROAF1 = 0.1
                },
                Aggressive = new IEForecastParameter
                {
                    ScoreRanking = 10,
                    DividendYield = 0.02,
                    ROEF1 = 0,
                    ROAF1 = 0,
                    FairValueVariation = -0.2,
                    FinancialLeverageF1 = 0.5,
                    FiveYearTotalReturn = -0.1,
                    OneYearRevenueGrowthF1 = -0.2,
                    DERatioF1 = 0.7
                },
                Increment = new IEForecastParameter
                {
                    DERatioF1 = 0.1,
                    FairValueVariation = 0.1,
                    FinancialLeverageF1 = 0.05,
                    FiveYearTotalReturn = 0.05,
                    OneYearRevenueGrowthF1 = 0.1,
                    ScoreRanking = 2,
                    DividendYield = 0.01,
                    ROEF1 = 0.05,
                    ROAF1 = 0.05
                }
            };

            #endregion
        }

        public IEF0Parameters Ief0Parameters { get; set; }
        public IEF1Parameters Ief1Parameters { get; set; }
    }
}