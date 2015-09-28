using System;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.SuitabilityLookupTables.Tables;
using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.ManagedInvestmentValues;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Asset
{
    public class ManagedInvestment : Equity
    {
        public ManagedInvestment(IRepository repo) : base(repo)
        {
        }

        public FundAllocation FundAllocation { get; set; }

        public override AssetSuitability GetRating()
        {
            return GetManagedFundAssetSuitability();
        }

        private AssetSuitability GetManagedFundAssetSuitability()
        {
            var table = new ManagedInvestmentSuitabilityParameters();
            var f0Score = new MICurrentParameter();
            SetF0Score_ManagedInvestment(table, f0Score);
            f0Score.Total = f0Score.FiveYearTotalReturn + f0Score.FiveYearAlphaRatio + f0Score.FiveYearBeta +
                            f0Score.FiveYearInformationRatio
                            + f0Score.FiveYearStandardDeviation + f0Score.FiveYearSkewnessRatio +
                            f0Score.FiveYearTrackingErrorRatio + f0Score.FiveYearSharpRatio
                            + f0Score.GlobalCategory + f0Score.FundSize;

            var f1Score = new MIForecastParameter();
            SetF1Score_ManagedInvestment(table, f1Score);
            f1Score.Total = f1Score.OneYearTotalReturn + f1Score.MorningStarAnalyst + f1Score.OneYearAlpha +
                            f1Score.OneYearBeta
                            + f1Score.OneYearInformationRatio + f1Score.OneYearTrackingError +
                            f1Score.OneYearSharpRatio + f1Score.MaxManagementExpenseRatio
                            + f1Score.PerformanceFee + f1Score.YearsSinceInception;

            return new AssetSuitability
            {
                F1Parameters = f1Score,
                SuitabilityRating = GetRatingScore(f1Score.Total + f0Score.Total),
                F0Parameters = f0Score,
                TotalScore = f1Score.Total + f0Score.Total
            };
        }

        #region managed investment helpers

        private void SetF1Score_ManagedInvestment(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            SetForecastOneYearReturnScore_MI(table, f1Score);
            SetForecastMorningstarAnalystScore_MI(f1Score, table);
            SetForecastOneYearAlphaScore_MI(table, f1Score);
            SetForecastOneYearBetaScore_MI(table, f1Score);
            SetForecastOneYearInformationScore_MI(table, f1Score);
            SetForecastOneYearTrackingErrorScore_MI(table, f1Score);
            SetForecastOneYearSharpRatioScore_MI(table, f1Score);
            SetForecastMaxManagementExpenseRatioScore_MI(table, f1Score);
            SetForecastPerformanceFeeScore_MI(table, f1Score);
            SetForecastYearSinceInceptionScore_MI(table, f1Score);
        }

        private void SetForecastYearSinceInceptionScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var yearsSinceInception = F1Recommendation.YearsSinceInception;
            if (yearsSinceInception.HasValue)
            {
                if (yearsSinceInception.Value >= table.Mif1Parameters.Defensive.YearsSinceInception)
                {
                    f1Score.YearsSinceInception = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (yearsSinceInception.Value >= table.Mif1Parameters.Conservative.YearsSinceInception)
                {
                    f1Score.YearsSinceInception = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (yearsSinceInception.Value >= table.Mif1Parameters.Balance.YearsSinceInception)
                {
                    f1Score.YearsSinceInception = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (yearsSinceInception.Value >= table.Mif1Parameters.Assertive.YearsSinceInception)
                {
                    f1Score.YearsSinceInception = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.YearsSinceInception = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Years since inception for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastPerformanceFeeScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var performanceFee = F1Recommendation.PerformanceFee;
            if (performanceFee.HasValue)
            {
                if (performanceFee.Value <= table.Mif1Parameters.Defensive.PerformanceFee)
                {
                    f1Score.PerformanceFee = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (performanceFee.Value <= table.Mif1Parameters.Conservative.PerformanceFee)
                {
                    f1Score.PerformanceFee = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (performanceFee.Value <= table.Mif1Parameters.Balance.PerformanceFee)
                {
                    f1Score.PerformanceFee = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (performanceFee.Value <= table.Mif1Parameters.Assertive.PerformanceFee)
                {
                    f1Score.PerformanceFee = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.PerformanceFee = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Performance fee for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastMaxManagementExpenseRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var maxManagementExpenseRatio = F1Recommendation.MaxManagementExpenseRatio;
            if (maxManagementExpenseRatio.HasValue)
            {
                if (maxManagementExpenseRatio.Value <= table.Mif1Parameters.Defensive.MaxManagementExpenseRatio)
                {
                    f1Score.MaxManagementExpenseRatio = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (maxManagementExpenseRatio.Value <=
                         table.Mif1Parameters.Conservative.MaxManagementExpenseRatio)
                {
                    f1Score.MaxManagementExpenseRatio = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (maxManagementExpenseRatio.Value <= table.Mif1Parameters.Balance.MaxManagementExpenseRatio)
                {
                    f1Score.MaxManagementExpenseRatio = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (maxManagementExpenseRatio.Value <= table.Mif1Parameters.Assertive.MaxManagementExpenseRatio)
                {
                    f1Score.MaxManagementExpenseRatio = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.MaxManagementExpenseRatio = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Max management expense ratio for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastOneYearSharpRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearSharpRatio = F1Recommendation.OneYearSharpRatio;
            if (oneYearSharpRatio.HasValue)
            {
                if (oneYearSharpRatio.Value >= table.Mif1Parameters.Defensive.OneYearSharpRatio)
                {
                    f1Score.OneYearSharpRatio = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearSharpRatio.Value >= table.Mif1Parameters.Conservative.OneYearSharpRatio)
                {
                    f1Score.OneYearSharpRatio = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearSharpRatio.Value >= table.Mif1Parameters.Balance.OneYearSharpRatio)
                {
                    f1Score.OneYearSharpRatio = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearSharpRatio.Value >= table.Mif1Parameters.Assertive.OneYearSharpRatio)
                {
                    f1Score.OneYearSharpRatio = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearSharpRatio = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year sharp ratio for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastOneYearTrackingErrorScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearTrackingError = F1Recommendation.OneYearTrackingError;
            if (oneYearTrackingError.HasValue)
            {
                if (oneYearTrackingError.Value >= table.Mif1Parameters.Defensive.OneYearTrackingError)
                {
                    f1Score.OneYearTrackingError = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearTrackingError.Value >= table.Mif1Parameters.Conservative.OneYearTrackingError)
                {
                    f1Score.OneYearTrackingError = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearTrackingError.Value >= table.Mif1Parameters.Balance.OneYearTrackingError)
                {
                    f1Score.OneYearTrackingError = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearTrackingError.Value >= table.Mif1Parameters.Assertive.OneYearTrackingError)
                {
                    f1Score.OneYearTrackingError = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearTrackingError = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year tracking error for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastOneYearInformationScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearInformationRatio = F1Recommendation.OneYearInformationRatio;
            if (oneYearInformationRatio.HasValue)
            {
                if (oneYearInformationRatio.Value >= table.Mif1Parameters.Defensive.OneYearInformationRatio)
                {
                    f1Score.OneYearInformationRatio = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearInformationRatio.Value >= table.Mif1Parameters.Conservative.OneYearInformationRatio)
                {
                    f1Score.OneYearInformationRatio = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearInformationRatio.Value >= table.Mif1Parameters.Balance.OneYearInformationRatio)
                {
                    f1Score.OneYearInformationRatio = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearInformationRatio.Value >= table.Mif1Parameters.Assertive.OneYearInformationRatio)
                {
                    f1Score.OneYearInformationRatio = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearInformationRatio = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year information ratio for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastOneYearBetaScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearBeta = F1Recommendation.OneYearBeta;
            if (oneYearBeta.HasValue)
            {
                if (oneYearBeta.Value <= table.Mif1Parameters.Defensive.OneYearBeta)
                {
                    f1Score.OneYearBeta = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearBeta.Value <= table.Mif1Parameters.Conservative.OneYearBeta)
                {
                    f1Score.OneYearBeta = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearBeta.Value <= table.Mif1Parameters.Balance.OneYearBeta)
                {
                    f1Score.OneYearBeta = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearBeta.Value <= table.Mif1Parameters.Assertive.OneYearBeta)
                {
                    f1Score.OneYearBeta = table.Mif1Parameters.Assertive.OneYearBeta;
                }
                else
                {
                    f1Score.OneYearBeta = table.Mif1Parameters.Aggressive.OneYearBeta;
                }
            }
            else
            {
                throw new Exception(
                    "One Year Beta for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastOneYearAlphaScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearAlpha = F1Recommendation.OneYearAlpha;
            if (oneYearAlpha.HasValue)
            {
                if (oneYearAlpha.Value >= table.Mif1Parameters.Defensive.OneYearAlpha)
                {
                    f1Score.OneYearAlpha = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearAlpha.Value >= table.Mif1Parameters.Conservative.OneYearAlpha)
                {
                    f1Score.OneYearAlpha = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearAlpha.Value >= table.Mif1Parameters.Balance.OneYearAlpha)
                {
                    f1Score.OneYearAlpha = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearAlpha.Value >= table.Mif1Parameters.Assertive.OneYearAlpha)
                {
                    f1Score.OneYearAlpha = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearAlpha = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year Alpha for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetForecastMorningstarAnalystScore_MI(MIForecastParameter f1Score,
            ManagedInvestmentSuitabilityParameters table)
        {
            if (F1Recommendation.MorningStarAnalyst == MorningStarAnalyst.Gold)
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.MorningStarAnalyst == MorningStarAnalyst.Silver)
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.MorningStarAnalyst == MorningStarAnalyst.Bronze)
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.MorningStarAnalyst == MorningStarAnalyst.Neutral)
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Assertive.ScoreRanking;
            }
            else if (F1Recommendation.MorningStarAnalyst == MorningStarAnalyst.Negative)
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Aggressive.ScoreRanking;
            }
            else
            {
                f1Score.MorningStarAnalyst = table.Mif1Parameters.Aggressive.ScoreRanking*20;
            }
        }

        private void SetForecastOneYearReturnScore_MI(ManagedInvestmentSuitabilityParameters table,
            MIForecastParameter f1Score)
        {
            var oneYearReturn = F1Recommendation.OneYearReturn;
            if (oneYearReturn.HasValue)
            {
                if (oneYearReturn.Value >= table.Mif1Parameters.Defensive.OneYearTotalReturn)
                {
                    f1Score.OneYearTotalReturn = table.Mif1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearReturn.Value >= table.Mif1Parameters.Conservative.OneYearTotalReturn)
                {
                    f1Score.OneYearTotalReturn = table.Mif1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearReturn.Value >= table.Mif1Parameters.Balance.OneYearTotalReturn)
                {
                    f1Score.OneYearTotalReturn = table.Mif1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearReturn.Value >= table.Mif1Parameters.Assertive.OneYearTotalReturn)
                {
                    f1Score.OneYearTotalReturn = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearTotalReturn = table.Mif1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year Return for forecast recommendation is not populated for current asset, which is of type managed fund.");
            }
        }

        private void SetF0Score_ManagedInvestment(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            SetCurrentFiveYearReturnScore_MI(table, f0Score);
            SetCurrentFiveYearAlphaRatioScore_MI(table, f0Score);
            SetCurrentBetaFiveYearScore_MI(table, f0Score);
            SetCurrentFiveYearAlphaRatio_MI(table, f0Score);
            SetCurrentFiveYearInfomrationScore_MI(table, f0Score);
            SetCurrentFiveYearStandardDeviationScore_MI(table, f0Score);
            SetCurrentFiveYearSkewnessRatioScore_MI(table, f0Score);
            SetCurrentFiveYearTrackingErrorRatioScore_MI(table, f0Score);
            SetCurrentFiveYearSharpRatioScore_MI(table, f0Score);
            SetCurrentGlobalCategoryScore_MI(table, f0Score);
            SetCurrentFundsizeScore_MI(table, f0Score);
        }

        private void SetCurrentFundsizeScore_MI(ManagedInvestmentSuitabilityParameters table, MICurrentParameter f0Score)
        {
            var fundSize = F0Ratios.FundSize;
            if (fundSize.HasValue)
            {
                if (fundSize.Value >= table.Mif0Parameters.Defensive.FundSize)
                {
                    f0Score.FundSize = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fundSize.Value >= table.Mif0Parameters.Conservative.FundSize)
                {
                    f0Score.FundSize = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fundSize.Value >= table.Mif0Parameters.Balance.FundSize)
                {
                    f0Score.FundSize = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fundSize.Value >= table.Mif0Parameters.Assertive.FundSize)
                {
                    f0Score.FundSize = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FundSize = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception("Fund Size is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentGlobalCategoryScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var globalCategory = F0Ratios.GlobalCategory;
            if (globalCategory.HasValue)
            {
                if (globalCategory.Value >= table.Mif0Parameters.Defensive.GlobalCategory)
                {
                    f0Score.GlobalCategory = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (globalCategory.Value >= table.Mif0Parameters.Conservative.GlobalCategory)
                {
                    f0Score.GlobalCategory = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (globalCategory.Value >= table.Mif0Parameters.Balance.GlobalCategory)
                {
                    f0Score.GlobalCategory = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (globalCategory.Value >= table.Mif0Parameters.Assertive.GlobalCategory)
                {
                    f0Score.GlobalCategory = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.GlobalCategory = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception("Global Category is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearSharpRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearSharpRatio = F0Ratios.FiveYearSharpRatio;
            if (fiveYearSharpRatio.HasValue)
            {
                if (fiveYearSharpRatio.Value >= table.Mif0Parameters.Defensive.FiveYearSharpRatio)
                {
                    f0Score.FiveYearSharpRatio = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearSharpRatio >= table.Mif0Parameters.Conservative.FiveYearSharpRatio)
                {
                    f0Score.FiveYearSharpRatio = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearSharpRatio >= table.Mif0Parameters.Balance.FiveYearSharpRatio)
                {
                    f0Score.FiveYearSharpRatio = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearSharpRatio >= table.Mif0Parameters.Assertive.FiveYearSharpRatio)
                {
                    f0Score.FiveYearSharpRatio = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearSharpRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year tracking error ratio is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearTrackingErrorRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearTrackingErrorRatio = F0Ratios.FiveYearTrackingErrorRatio;
            if (fiveYearTrackingErrorRatio.HasValue)
            {
                if (fiveYearTrackingErrorRatio.Value >= table.Mif0Parameters.Defensive.FiveYearTrackingErrorRatio)
                {
                    f0Score.FiveYearTrackingErrorRatio = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearTrackingErrorRatio.Value >=
                         table.Mif0Parameters.Conservative.FiveYearTrackingErrorRatio)
                {
                    f0Score.FiveYearTrackingErrorRatio = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearTrackingErrorRatio.Value >= table.Mif0Parameters.Balance.FiveYearTrackingErrorRatio)
                {
                    f0Score.FiveYearTrackingErrorRatio = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearTrackingErrorRatio.Value >=
                         table.Mif0Parameters.Assertive.FiveYearTrackingErrorRatio)
                {
                    f0Score.FiveYearTrackingErrorRatio = table.Mif1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearTrackingErrorRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year tracking error ratio is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearSkewnessRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearSkewnessRatio = F0Ratios.FiveYearSkewnessRatio;
            if (fiveYearSkewnessRatio.HasValue)
            {
                if (fiveYearSkewnessRatio.Value > table.Mif0Parameters.Defensive.FiveYearSkewnessRatio)
                {
                    f0Score.FiveYearSkewnessRatio = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearSkewnessRatio.Value >= table.Mif0Parameters.Conservative.FiveYearSkewnessRatio)
                {
                    f0Score.FiveYearSkewnessRatio = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearSkewnessRatio.Value >= table.Mif0Parameters.Balance.FiveYearSkewnessRatio)
                {
                    f0Score.FiveYearSkewnessRatio = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearSkewnessRatio.Value >= table.Mif0Parameters.Assertive.FiveYearSkewnessRatio)
                {
                    f0Score.FiveYearSkewnessRatio = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearSkewnessRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year skewness ratio is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearStandardDeviationScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearStandardDeviation = F0Ratios.FiveYearStandardDeviation;
            if (fiveYearStandardDeviation.HasValue)
            {
                if (fiveYearStandardDeviation.Value >= table.Mif0Parameters.Defensive.FiveYearStandardDeviation)
                {
                    f0Score.FiveYearStandardDeviation = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearStandardDeviation.Value >=
                         table.Mif0Parameters.Conservative.FiveYearStandardDeviation)
                {
                    f0Score.FiveYearStandardDeviation = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearStandardDeviation.Value >= table.Mif0Parameters.Balance.FiveYearStandardDeviation)
                {
                    f0Score.FiveYearStandardDeviation = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearStandardDeviation.Value >= table.Mif0Parameters.Assertive.FiveYearStandardDeviation)
                {
                    f0Score.FiveYearStandardDeviation = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearStandardDeviation = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year standard deviation is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearInfomrationScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearInfomation = F0Ratios.FiveYearInformation;
            if (fiveYearInfomation.HasValue)
            {
                if (fiveYearInfomation.Value >= table.Mif0Parameters.Defensive.FiveYearInformationRatio)
                {
                    f0Score.FiveYearInformationRatio = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearInfomation.Value >= table.Mif0Parameters.Conservative.FiveYearInformationRatio)
                {
                    f0Score.FiveYearInformationRatio = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearInfomation.Value >= table.Mif0Parameters.Balance.FiveYearInformationRatio)
                {
                    f0Score.FiveYearInformationRatio = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearInfomation.Value >= table.Mif0Parameters.Assertive.FiveYearInformationRatio)
                {
                    f0Score.FiveYearInformationRatio = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearInformationRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year information is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentFiveYearAlphaRatio_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            var fiveYearAlphaRatio = F0Ratios.FiveYearAlphaRatio;
            if (fiveYearAlphaRatio.HasValue)
            {
                if (fiveYearAlphaRatio.Value >= table.Mif0Parameters.Defensive.FiveYearAlphaRatio)
                {
                    f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Defensive.ScoreRanking;
                }
                else if (fiveYearAlphaRatio.Value >= table.Mif0Parameters.Conservative.FiveYearAlphaRatio)
                {
                    f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Conservative.ScoreRanking;
                }
                else if (fiveYearAlphaRatio.Value >= table.Mif0Parameters.Balance.FiveYearAlphaRatio)
                {
                    f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Balance.ScoreRanking;
                }
                else if (fiveYearAlphaRatio.Value >= table.Mif0Parameters.Assertive.FiveYearAlphaRatio)
                {
                    f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Five year alpha ratio is not populated for current asset, which is of type managed fund");
            }
        }

        private void SetCurrentBetaFiveYearScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            if (F0Ratios.BetaFiveYears <= table.Mif0Parameters.Defensive.FiveYearBeta)
            {
                f0Score.FiveYearBeta = table.Mif0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Mif0Parameters.Conservative.FiveYearBeta)
            {
                f0Score.FiveYearBeta = table.Mif0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Mif0Parameters.Balance.FiveYearBeta)
            {
                f0Score.FiveYearBeta = table.Mif0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Mif0Parameters.Assertive.FiveYearBeta)
            {
                f0Score.FiveYearBeta = table.Mif0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.FiveYearBeta = table.Mif0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentFiveYearAlphaRatioScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            if (F0Ratios.FiveYearAlphaRatio >= table.Mif0Parameters.Defensive.FiveYearAlphaRatio)
            {
                f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.FiveYearAlphaRatio >= table.Mif0Parameters.Conservative.FiveYearAlphaRatio)
            {
                f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.FiveYearAlphaRatio >= table.Mif0Parameters.Balance.FiveYearAlphaRatio)
            {
                f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.FiveYearAlphaRatio >= table.Mif0Parameters.Assertive.FiveYearAlphaRatio)
            {
                f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.FiveYearAlphaRatio = table.Mif0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentFiveYearReturnScore_MI(ManagedInvestmentSuitabilityParameters table,
            MICurrentParameter f0Score)
        {
            if (F0Ratios.FiveYearReturn >= table.Mif0Parameters.Defensive.FiveYearTotalReturn)
            {
                f0Score.FiveYearTotalReturn = table.Mif0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.FiveYearReturn >= table.Mif0Parameters.Conservative.FiveYearTotalReturn)
            {
                f0Score.FiveYearTotalReturn = table.Mif0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.FiveYearReturn >= table.Mif0Parameters.Balance.FiveYearTotalReturn)
            {
                f0Score.FiveYearTotalReturn = table.Mif0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.FiveYearReturn >= table.Mif0Parameters.Assertive.FiveYearTotalReturn)
            {
                f0Score.FiveYearTotalReturn = table.Mif0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.FiveYearTotalReturn = table.Mif0Parameters.Aggressive.ScoreRanking;
            }
        }

        #endregion
    }
}