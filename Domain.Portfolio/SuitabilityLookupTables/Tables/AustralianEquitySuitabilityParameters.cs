using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class AustralianEquitySuitabilityParameters
    {
        public AustralianEquitySuitabilityParameters()
        {
            #region current parameters

            F0Paramters = new AEF0Paramters
            {
                Aggressive = new AECurrentParameter
                {
                    EpsGrowth = 0.04,
                    CurrentMarketCapitalisation = 1000000000,
                    DivYieldF0 = 0.02,
                    FrankF0 = 0.6,
                    ROAF0 = 0,
                    ROEF0 = 0,
                    InterestCoverF0 = 1,
                    BetaFiveYear = 1.4,
                    DebtEquityF0 = 0.7,
                    PEF0 = 22,
                    ScoreRanking = 10
                },
                Assertive = new AECurrentParameter
                {
                    InterestCoverF0 = 3,
                    BetaFiveYear = 1.2,
                    DebtEquityF0 = 0.6,
                    PEF0 = 19,
                    ROAF0 = 0.05,
                    ROEF0 = 0.05,
                    ScoreRanking = 8,
                    FrankF0 = 0.675,
                    DivYieldF0 = 0.03,
                    CurrentMarketCapitalisation = 3000000000,
                    EpsGrowth = 0.07
                },
                Balance = new AECurrentParameter
                {
                    FrankF0 = 0.75,
                    ROEF0 = 0.1,
                    ROAF0 = 0.1,
                    DivYieldF0 = 0.04,
                    CurrentMarketCapitalisation = 5000000000,
                    EpsGrowth = 0.1,
                    ScoreRanking = 6,
                    InterestCoverF0 = 5,
                    BetaFiveYear = 1,
                    DebtEquityF0 = 0.5,
                    PEF0 = 16
                },
                Conservative = new AECurrentParameter
                {
                    BetaFiveYear = 0.8,
                    CurrentMarketCapitalisation = 7000000000,
                    DebtEquityF0 = 0.4,
                    EpsGrowth = 0.13,
                    InterestCoverF0 = 7,
                    PEF0 = 13,
                    ScoreRanking = 4,
                    ROEF0 = 0.15,
                    ROAF0 = 0.15,
                    DivYieldF0 = 0.05,
                    FrankF0 = 0.825
                },
                Defensive = new AECurrentParameter
                {
                    ROEF0 = 0.2,
                    ROAF0 = 0.2,
                    DivYieldF0 = 0.06,
                    InterestCoverF0 = 9,
                    FrankF0 = 0.9,
                    ScoreRanking = 2,
                    PEF0 = 10,
                    DebtEquityF0 = 0.3,
                    CurrentMarketCapitalisation = 9000000000,
                    BetaFiveYear = 0.6,
                    EpsGrowth = 0.16
                },
                Increment = new AECurrentParameter
                {
                    BetaFiveYear = 0.2,
                    CurrentMarketCapitalisation = 2000000000,
                    DebtEquityF0 = 0.1,
                    EpsGrowth = 0.03,
                    FrankF0 = 0.075,
                    PEF0 = 3,
                    ScoreRanking = 2,
                    ROAF0 = 0.05,
                    DivYieldF0 = 0.01,
                    InterestCoverF0 = 2,
                    ROEF0 = 0.05
                },
                MaxScore = new AECurrentParameter
                {
                    ROAF0 = 10,
                    DivYieldF0 = 10,
                    InterestCoverF0 = 10,
                    FrankF0 = 10,
                    ROEF0 = 10,
                    ScoreRanking = 100,
                    PEF0 = 10,
                    DebtEquityF0 = 10,
                    CurrentMarketCapitalisation = 10,
                    EpsGrowth = 10,
                    BetaFiveYear = 10
                }
            };

            #endregion

            #region forecast parameters

            F1Parameters = new AEF1Parameters
            {
                Balance = new AEForecastParameter
                {
                    EpsGrowth = 0.1,
                    ScoreRanking = 6,
                    DebtEquityF1 = 0.5,
                    DivYieldF1 = 0.04,
                    FrankF1 = 0.75,
                    InterestCoverF1 = 5,
                    IntrsicValueVariation = 0,
                    PEF1 = 16,
                    ROAF1 = 0.1,
                    ROEF1 = 0.1
                },
                Defensive = new AEForecastParameter
                {
                    PEF1 = 10,
                    ROAF1 = 0.2,
                    ROEF1 = 0.2,
                    ScoreRanking = 2,
                    EpsGrowth = 0.16,
                    DivYieldF1 = 0.06,
                    FrankF1 = 0.9,
                    InterestCoverF1 = 9,
                    IntrsicValueVariation = 0.2,
                    DebtEquityF1 = 0.3
                },
                Conservative = new AEForecastParameter
                {
                    DebtEquityF1 = 0.4,
                    DivYieldF1 = 0.05,
                    FrankF1 = 0.825,
                    InterestCoverF1 = 7,
                    IntrsicValueVariation = 0.1,
                    ScoreRanking = 4,
                    EpsGrowth = 0.13,
                    ROEF1 = 0.15,
                    ROAF1 = 0.15,
                    PEF1 = 13
                },
                Assertive = new AEForecastParameter
                {
                    ScoreRanking = 8,
                    EpsGrowth = 0.07,
                    PEF1 = 19,
                    ROAF1 = 0.05,
                    ROEF1 = 0.05,
                    DivYieldF1 = 0.03,
                    FrankF1 = 0.675,
                    InterestCoverF1 = 3,
                    IntrsicValueVariation = -0.1,
                    DebtEquityF1 = 0.6
                },
                Increment = new AEForecastParameter
                {
                    DebtEquityF1 = 0.1,
                    DivYieldF1 = 0.01,
                    FrankF1 = 0.075,
                    InterestCoverF1 = 2,
                    IntrsicValueVariation = 0.1,
                    ROAF1 = 0.05,
                    ROEF1 = 0.05,
                    ScoreRanking = 2,
                    EpsGrowth = 0.03,
                    PEF1 = 3
                },
                Aggressive = new AEForecastParameter
                {
                    ScoreRanking = 10,
                    EpsGrowth = 0.04,
                    PEF1 = 22,
                    ROEF1 = 0,
                    DivYieldF1 = 0.02,
                    FrankF1 = 0.6,
                    InterestCoverF1 = 1,
                    IntrsicValueVariation = -0.2,
                    ROAF1 = 0,
                    DebtEquityF1 = 0.7
                }
            };

            #endregion
        }

        public AEF0Paramters F0Paramters { get; set; }
        public AEF1Parameters F1Parameters { get; set; }
    }
}