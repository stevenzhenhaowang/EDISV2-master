using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class ManagedInvestmentSuitabilityParameters
    {
        public ManagedInvestmentSuitabilityParameters()
        {
            #region current parameters

            Mif0Parameters = new MIF0Parameters
            {
                Assertive = new MICurrentParameter
                {
                    ScoreRanking = 8,
                    FiveYearTotalReturn = 0.05,
                    FiveYearAlphaRatio = -5,
                    FiveYearBeta = 1.2,
                    FiveYearInformationRatio = 1,
                    FiveYearSharpRatio = 1,
                    FiveYearSkewnessRatio = 1,
                    FiveYearStandardDeviation = 5,
                    FiveYearTrackingErrorRatio = -0.05,
                    FundSize = 100000000,
                    GlobalCategory = 0.02
                },
                Conservative = new MICurrentParameter
                {
                    FiveYearInformationRatio = 3,
                    FiveYearSharpRatio = 3,
                    FiveYearSkewnessRatio = 3,
                    FiveYearStandardDeviation = 15,
                    FiveYearTrackingErrorRatio = 0.05,
                    FundSize = 200000000,
                    GlobalCategory = 0.04,
                    ScoreRanking = 4,
                    FiveYearTotalReturn = 0.15,
                    FiveYearBeta = 0.8,
                    FiveYearAlphaRatio = 5
                },
                Increment = new MICurrentParameter
                {
                    ScoreRanking = 2,
                    FiveYearTotalReturn = 0.05,
                    FiveYearBeta = 0.2,
                    FiveYearAlphaRatio = 10,
                    FundSize = 0,
                    FiveYearTrackingErrorRatio = 0.01,
                    GlobalCategory = 3,
                    FiveYearSharpRatio = 1,
                    FiveYearStandardDeviation = 0.01,
                    FiveYearSkewnessRatio = 0.01,
                    FiveYearInformationRatio = 0.01
                },
                Aggressive = new MICurrentParameter
                {
                    FiveYearInformationRatio = 0,
                    FiveYearSharpRatio = 0,
                    FiveYearSkewnessRatio = 0,
                    FiveYearStandardDeviation = 0,
                    FiveYearTrackingErrorRatio = -0.1,
                    FundSize = 50000000,
                    GlobalCategory = 0.01,
                    ScoreRanking = 10,
                    FiveYearTotalReturn = 0,
                    FiveYearBeta = 1.4,
                    FiveYearAlphaRatio = -0.1
                },
                Balance = new MICurrentParameter
                {
                    ScoreRanking = 6,
                    FiveYearTotalReturn = 0.1,
                    FiveYearBeta = 1,
                    FiveYearAlphaRatio = 0,
                    FundSize = 150000000,
                    FiveYearTrackingErrorRatio = 0,
                    GlobalCategory = 0.03,
                    FiveYearSharpRatio = 2,
                    FiveYearStandardDeviation = 10,
                    FiveYearSkewnessRatio = 2,
                    FiveYearInformationRatio = 2
                },
                Defensive = new MICurrentParameter
                {
                    FiveYearInformationRatio = 4,
                    FiveYearSharpRatio = 4,
                    FiveYearSkewnessRatio = 4,
                    FiveYearStandardDeviation = 20,
                    FiveYearTrackingErrorRatio = 0.1,
                    FundSize = 250000000,
                    GlobalCategory = 0.05,
                    ScoreRanking = 2,
                    FiveYearTotalReturn = 0.2,
                    FiveYearBeta = 0.6,
                    FiveYearAlphaRatio = 10
                },
                MaxScore = new MICurrentParameter
                {
                    ScoreRanking = 100,
                    FiveYearTotalReturn = 10,
                    FiveYearBeta = 10,
                    FiveYearAlphaRatio = 10,
                    FundSize = 10,
                    FiveYearTrackingErrorRatio = 10,
                    GlobalCategory = 10,
                    FiveYearSharpRatio = 10,
                    FiveYearStandardDeviation = 10,
                    FiveYearSkewnessRatio = 10,
                    FiveYearInformationRatio = 10
                }
            };

            #endregion

            #region forecast parameters

            Mif1Parameters = new MIF1Parameters
            {
                Conservative = new MIForecastParameter
                {
                    ScoreRanking = 4,
                    OneYearTotalReturn = 0.05,
                    MaxManagementExpenseRatio = 0.02,
                    OneYearAlpha = 5,
                    OneYearBeta = 0.8,
                    OneYearInformationRatio = 3,
                    OneYearSharpRatio = 3,
                    OneYearTrackingError = 5,
                    PerformanceFee = 0.05,
                    YearsSinceInception = 7
                },
                Increment = new MIForecastParameter
                {
                    OneYearAlpha = 0.1,
                    OneYearBeta = 0.05,
                    OneYearInformationRatio = 0.05,
                    OneYearSharpRatio = 0.1,
                    OneYearTrackingError = 2,
                    PerformanceFee = 4,
                    YearsSinceInception = 0.1,
                    ScoreRanking = 2,
                    OneYearTotalReturn = 0.03,
                    MaxManagementExpenseRatio = 3
                },
                Assertive = new MIForecastParameter
                {
                    ScoreRanking = 8,
                    OneYearTotalReturn = -0.05,
                    MaxManagementExpenseRatio = 0.04,
                    YearsSinceInception = 3,
                    OneYearSharpRatio = 1,
                    OneYearBeta = 1.2,
                    OneYearInformationRatio = 1,
                    OneYearTrackingError = -5,
                    PerformanceFee = 0.1,
                    OneYearAlpha = -5
                },
                Aggressive = new MIForecastParameter
                {
                    OneYearAlpha = -10,
                    OneYearBeta = 1.4,
                    OneYearInformationRatio = 0,
                    OneYearSharpRatio = 0,
                    OneYearTrackingError = -10,
                    PerformanceFee = 0.2,
                    YearsSinceInception = 1,
                    ScoreRanking = 10,
                    OneYearTotalReturn = -0.1,
                    MaxManagementExpenseRatio = 0.05
                },
                Balance = new MIForecastParameter
                {
                    ScoreRanking = 6,
                    OneYearTotalReturn = 0,
                    MaxManagementExpenseRatio = 0.03,
                    YearsSinceInception = 5,
                    OneYearSharpRatio = 2,
                    OneYearBeta = 1,
                    OneYearInformationRatio = 2,
                    OneYearTrackingError = 0,
                    PerformanceFee = 0.1,
                    OneYearAlpha = 0
                },
                Defensive = new MIForecastParameter
                {
                    OneYearAlpha = 10,
                    OneYearBeta = 0.6,
                    OneYearInformationRatio = 4,
                    OneYearSharpRatio = 4,
                    OneYearTrackingError = 10,
                    PerformanceFee = 0,
                    YearsSinceInception = 9,
                    ScoreRanking = 2,
                    OneYearTotalReturn = 0.1,
                    MaxManagementExpenseRatio = 0.01
                },
                MaxScore = new MIForecastParameter
                {
                    ScoreRanking = 100,
                    OneYearTotalReturn = 10,
                    MaxManagementExpenseRatio = 10,
                    YearsSinceInception = 10,
                    OneYearSharpRatio = 10,
                    OneYearBeta = 10,
                    OneYearInformationRatio = 10,
                    OneYearTrackingError = 10,
                    PerformanceFee = 10,
                    OneYearAlpha = 10
                }
            };

            #endregion
        }

        public MIF0Parameters Mif0Parameters { get; set; }
        public MIF1Parameters Mif1Parameters { get; set; }
    }
}