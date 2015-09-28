using Domain.Portfolio.Interfaces;
using Domain.Portfolio.SuitabilityLookupTables.Tables;
using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;
using Domain.Portfolio.Values;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Asset
{
    public class AustralianEquity : Equity 
    {
        public AustralianEquity(IRepository repo) : base(repo) 
        {
        }


        public override AssetSuitability GetRating()
        {
            return GetAustralianEquityAssetSuitability();
        }

        private AssetSuitability GetAustralianEquityAssetSuitability()
        {
            var table = new AustralianEquitySuitabilityParameters();
            var f0Score = new AECurrentParameter();
            SetF0Score_AustralianEquity(table, f0Score);
            f0Score.Total = f0Score.EpsGrowth + f0Score.CurrentMarketCapitalisation + f0Score.DivYieldF0 +
                            f0Score.FrankF0
                            + f0Score.ROAF0 + f0Score.ROEF0 + f0Score.InterestCoverF0 + f0Score.DebtEquityF0 +
                            f0Score.PEF0 + f0Score.BetaFiveYear;
            var f1Score = new AEForecastParameter();
            SetF1Score_AustralianEquity(table, f1Score);
            f1Score.Total = f1Score.EpsGrowth + f1Score.MorningStarRecommandation + f1Score.DivYieldF1 +
                            f1Score.FrankF1
                            + f1Score.ROAF1 + f1Score.ROEF1 + f1Score.InterestCoverF1 + f1Score.DebtEquityF1 +
                            f1Score.PEF1 + f1Score.IntrsicValueVariation;

            return new AssetSuitability
            {
                F1Parameters = f1Score,
                SuitabilityRating = GetRatingScore(f1Score.Total + f0Score.Total),
                F0Parameters = f0Score,
                TotalScore = f1Score.Total + f0Score.Total
            };
        }

        #region Australian equity suitability helpers

        private void SetF1Score_AustralianEquity(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            SetForecastEpsGrowthScore_AE(table, f1Score);
            SetForecastMorningstarRecommendationScore_AE(f1Score, table);
            SetForecastDividendYieldScore_AE(table, f1Score);
            SetForecastFrankScore_AE(table, f1Score);
            SetForecastReturnOnAssetScore_AE(table, f1Score);
            SetForecastReturnOnEquityScore_AE(table, f1Score);
            SetForecastInterestCoverScore_AE(f1Score, table);
            SetForecastDebtEquityScore_AE(f1Score, table);
            SetForecastPriceEarningRatioScore_AE(table, f1Score);
            SetForecastIntrinsicValueScore_AE(table, f1Score);
        }

        private void SetForecastIntrinsicValueScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.IntrinsicValue >= table.F1Parameters.Defensive.IntrsicValueVariation)
            {
                f1Score.IntrsicValueVariation = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.IntrinsicValue >= table.F1Parameters.Conservative.IntrsicValueVariation)
            {
                f1Score.IntrsicValueVariation = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.IntrinsicValue >= table.F1Parameters.Balance.IntrsicValueVariation)
            {
                f1Score.IntrsicValueVariation = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.IntrinsicValue >= table.F1Parameters.Assertive.IntrsicValueVariation)
            {
                f1Score.IntrsicValueVariation = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.IntrsicValueVariation = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastPriceEarningRatioScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.PriceEarningRatio <= table.F1Parameters.Defensive.PEF1)
            {
                f1Score.PEF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.PriceEarningRatio <= table.F1Parameters.Conservative.PEF1)
            {
                f1Score.PEF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.PriceEarningRatio <= table.F1Parameters.Balance.PEF1)
            {
                f1Score.PEF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.PriceEarningRatio <= table.F1Parameters.Assertive.PEF1)
            {
                f1Score.PEF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.PEF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastDebtEquityScore_AE(AEForecastParameter f1Score,
            AustralianEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.F1Parameters.Defensive.DebtEquityF1)
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.F1Parameters.Conservative.DebtEquityF1)
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.F1Parameters.Balance.DebtEquityF1)
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.F1Parameters.Assertive.DebtEquityF1)
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.DebtEquityF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastInterestCoverScore_AE(AEForecastParameter f1Score,
            AustralianEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                f1Score.InterestCoverF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.InterestCover >= table.F1Parameters.Defensive.InterestCoverF1)
            {
                f1Score.InterestCoverF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.InterestCover >= table.F1Parameters.Conservative.InterestCoverF1)
            {
                f1Score.InterestCoverF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.InterestCover >= table.F1Parameters.Balance.InterestCoverF1)
            {
                f1Score.InterestCoverF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.InterestCoverF1 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastReturnOnEquityScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.ReturnOnEquity >= table.F1Parameters.Defensive.ROEF1)
            {
                f1Score.ROEF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.F1Parameters.Conservative.ROEF1)
            {
                f1Score.ROEF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.F1Parameters.Balance.ROEF1)
            {
                f1Score.ROEF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.F1Parameters.Assertive.ROEF1)
            {
                f1Score.ROEF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.ROEF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastReturnOnAssetScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.ReturnOnAsset >= table.F1Parameters.Defensive.ROAF1)
            {
                f1Score.ROAF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.F1Parameters.Conservative.ROAF1)
            {
                f1Score.ROAF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.F1Parameters.Balance.ROAF1)
            {
                f1Score.ROAF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.F1Parameters.Assertive.ROAF1)
            {
                f1Score.ROAF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.ROAF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastFrankScore_AE(AustralianEquitySuitabilityParameters table, AEForecastParameter f1Score)
        {
            if (F1Recommendation.Frank >= table.F1Parameters.Defensive.FrankF1)
            {
                f1Score.FrankF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.Frank >= table.F1Parameters.Conservative.FrankF1)
            {
                f1Score.FrankF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.Frank >= table.F1Parameters.Balance.FrankF1)
            {
                f1Score.FrankF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.Frank >= table.F1Parameters.Assertive.FrankF1)
            {
                f1Score.FrankF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.FrankF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastDividendYieldScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.DividendYield >= table.F1Parameters.Defensive.DivYieldF1)
            {
                f1Score.DivYieldF1 = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.F1Parameters.Conservative.DivYieldF1)
            {
                f1Score.DivYieldF1 = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.F1Parameters.Balance.DivYieldF1)
            {
                f1Score.DivYieldF1 = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.F1Parameters.Assertive.DivYieldF1)
            {
                f1Score.DivYieldF1 = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.DivYieldF1 = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastMorningstarRecommendationScore_AE(AEForecastParameter f1Score,
            AustralianEquitySuitabilityParameters table)
        {
            if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Buy ||
                F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.HighlyRecommended)
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Accumulate ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Recommended)
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Hold ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.InvestmentGrade)
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Reduce)
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Assertive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Sell ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Avoid ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.UnderReview)
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Aggressive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.CeasedCoverage ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.None ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.NoRecommendation)
            {
                f1Score.MorningStarRecommandation = 20*table.F1Parameters.Aggressive.ScoreRanking;
            }
            else
            {
                f1Score.MorningStarRecommandation = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastEpsGrowthScore_AE(AustralianEquitySuitabilityParameters table,
            AEForecastParameter f1Score)
        {
            if (F1Recommendation.EpsGrowth >= table.F1Parameters.Defensive.EpsGrowth)
            {
                f1Score.EpsGrowth = table.F1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.EpsGrowth >= table.F1Parameters.Conservative.EpsGrowth)
            {
                f1Score.EpsGrowth = table.F1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.EpsGrowth >= table.F1Parameters.Balance.EpsGrowth)
            {
                f1Score.EpsGrowth = table.F1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.EpsGrowth >= table.F1Parameters.Assertive.EpsGrowth)
            {
                f1Score.EpsGrowth = table.F1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.EpsGrowth = table.F1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetF0Score_AustralianEquity(AustralianEquitySuitabilityParameters table, AECurrentParameter f0Score)
        {
            SetCurrentEpsGrowthScore_AE(table, f0Score);
            SetCurrentCapitalisationScore_AE(table, f0Score);
            SetCurrentDividendYieldScore_AE(table, f0Score);
            SetCurrentFrankScore_AE(table, f0Score);
            SetCurrentReturnOnAssetScore_AE(table, f0Score);
            SetReturnOnEquityScore_AE(table, f0Score);
            SetCurrentInterestCoverScore_AE(f0Score, table);
            SetCurrentDebtEquityScore_AE(f0Score, table);
            SetCurrentPriceEarningRatioScore_AE(table, f0Score);
            SetCurrentBetaFiveYearsScore_AE(table, f0Score);
        }

        private void SetCurrentBetaFiveYearsScore_AE(AustralianEquitySuitabilityParameters table,
            AECurrentParameter f0Score)
        {
            if (F0Ratios.BetaFiveYears <= table.F0Paramters.Defensive.BetaFiveYear)
            {
                f0Score.BetaFiveYear = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.F0Paramters.Conservative.BetaFiveYear)
            {
                f0Score.BetaFiveYear = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.F0Paramters.Balance.BetaFiveYear)
            {
                f0Score.BetaFiveYear = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.F0Paramters.Assertive.BetaFiveYear)
            {
                f0Score.BetaFiveYear = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.BetaFiveYear = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentPriceEarningRatioScore_AE(AustralianEquitySuitabilityParameters table,
            AECurrentParameter f0Score)
        {
            if (F0Ratios.PriceEarningRatio <= table.F0Paramters.Defensive.PEF0)
            {
                f0Score.PEF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.F0Paramters.Conservative.PEF0)
            {
                f0Score.PEF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.F0Paramters.Balance.PEF0)
            {
                f0Score.PEF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.F0Paramters.Assertive.PEF0)
            {
                f0Score.PEF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.PEF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentDebtEquityScore_AE(AECurrentParameter f0Score,
            AustralianEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.F0Paramters.Defensive.DebtEquityF0)
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.F0Paramters.Conservative.DebtEquityF0)
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.F0Paramters.Balance.DebtEquityF0)
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.F0Paramters.Assertive.DebtEquityF0)
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.DebtEquityF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentInterestCoverScore_AE(AECurrentParameter f0Score,
            AustralianEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.InterestCover >= table.F0Paramters.Defensive.InterestCoverF0)
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.InterestCover >= table.F0Paramters.Conservative.InterestCoverF0)
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.InterestCover >= table.F0Paramters.Balance.InterestCoverF0)
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.InterestCover >= table.F0Paramters.Assertive.InterestCoverF0)
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.InterestCoverF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetReturnOnEquityScore_AE(AustralianEquitySuitabilityParameters table, AECurrentParameter f0Score)
        {
            if (F0Ratios.ReturnOnEquity >= table.F0Paramters.Defensive.ROEF0)
            {
                f0Score.ROEF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.F0Paramters.Conservative.ROEF0)
            {
                f0Score.ROEF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.F0Paramters.Balance.ROEF0)
            {
                f0Score.ROEF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.F0Paramters.Assertive.ROEF0)
            {
                f0Score.ROEF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.ROEF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentReturnOnAssetScore_AE(AustralianEquitySuitabilityParameters table,
            AECurrentParameter f0Score)
        {
            if (F0Ratios.ReturnOnAsset >= table.F0Paramters.Defensive.ROAF0)
            {
                f0Score.ROAF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.F0Paramters.Conservative.ROAF0)
            {
                f0Score.ROAF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.F0Paramters.Balance.ROAF0)
            {
                f0Score.ROAF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.F0Paramters.Assertive.ROAF0)
            {
                f0Score.ROAF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.ROAF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentFrankScore_AE(AustralianEquitySuitabilityParameters table, AECurrentParameter f0Score)
        {
            if (F0Ratios.Frank >= table.F0Paramters.Defensive.FrankF0)
            {
                f0Score.FrankF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.Frank >= table.F0Paramters.Conservative.FrankF0)
            {
                f0Score.FrankF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.Frank >= table.F0Paramters.Balance.FrankF0)
            {
                f0Score.FrankF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.Frank >= table.F0Paramters.Assertive.FrankF0)
            {
                f0Score.FrankF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.FrankF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentDividendYieldScore_AE(AustralianEquitySuitabilityParameters table,
            AECurrentParameter f0Score)
        {
            if (F0Ratios.DividendYield >= table.F0Paramters.Defensive.DivYieldF0)
            {
                f0Score.DivYieldF0 = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.F0Paramters.Conservative.DivYieldF0)
            {
                f0Score.DivYieldF0 = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.F0Paramters.Balance.DivYieldF0)
            {
                f0Score.DivYieldF0 = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.F0Paramters.Assertive.DivYieldF0)
            {
                f0Score.DivYieldF0 = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.DivYieldF0 = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentCapitalisationScore_AE(AustralianEquitySuitabilityParameters table,
            AECurrentParameter f0Score)
        {
            if (F0Ratios.Capitalisation >= table.F0Paramters.Defensive.CurrentMarketCapitalisation)
            {
                f0Score.CurrentMarketCapitalisation = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.F0Paramters.Conservative.CurrentMarketCapitalisation)
            {
                f0Score.CurrentMarketCapitalisation = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.F0Paramters.Balance.CurrentMarketCapitalisation)
            {
                f0Score.CurrentMarketCapitalisation = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.F0Paramters.Assertive.CurrentMarketCapitalisation)
            {
                f0Score.CurrentMarketCapitalisation = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.CurrentMarketCapitalisation = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentEpsGrowthScore_AE(AustralianEquitySuitabilityParameters table, AECurrentParameter f0Score)
        {
            if (F0Ratios.EpsGrowth >= table.F0Paramters.Defensive.EpsGrowth)
            {
                f0Score.EpsGrowth = table.F0Paramters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.EpsGrowth >= table.F0Paramters.Conservative.EpsGrowth)
            {
                f0Score.EpsGrowth = table.F0Paramters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.EpsGrowth >= table.F0Paramters.Balance.EpsGrowth)
            {
                f0Score.EpsGrowth = table.F0Paramters.Balance.ScoreRanking;
            }
            else if (F0Ratios.EpsGrowth >= table.F0Paramters.Assertive.EpsGrowth)
            {
                f0Score.EpsGrowth = table.F0Paramters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.EpsGrowth = table.F0Paramters.Aggressive.ScoreRanking;
            }
        }

        #endregion
    }
}