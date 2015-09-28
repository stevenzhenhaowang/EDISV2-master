using System;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.SuitabilityLookupTables.Tables;
using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;
using Domain.Portfolio.Values;
using Shared;

namespace Domain.Portfolio.AggregateRoots.Asset
{
    public class InternationalEquity : Equity
    {
        public InternationalEquity(IRepository repo) : base(repo)
        {
        }

        public override AssetSuitability GetRating()
        {
            return GetInternationalEquityAssetSuitability();
        }

        private AssetSuitability GetInternationalEquityAssetSuitability()
        {
            var table = new InternationalEquitySuitabilityParameters();
            var f0Score = new IECurrentParameter();
            SetF0Score_InternationalEquity(table, f0Score);
            f0Score.Total = f0Score.OneYearTotalReturn + f0Score.CurrentMarketCapitalisatiopn +
                            f0Score.DividendYield + f0Score.ROA + f0Score.ROE
                            + f0Score.QuickRatio + f0Score.CurrentRatio + f0Score.TotalDebtTotalEquityRatio +
                            f0Score.PERatio + f0Score.BetaFiveYear;


            var f1Score = new IEForecastParameter();
            SetF1Score_InternationalEquity(table, f1Score);
            f1Score.Total = f1Score.FiveYearTotalReturn + f1Score.MorningStarRecommendation + f1Score.DividendYield +
                            f1Score.ROAF1 + f1Score.ROEF1
                            + f1Score.FinancialLeverageF1 + f1Score.OneYearRevenueGrowthF1 + f1Score.DERatioF1 +
                            f1Score.CreditRating + f1Score.FairValueVariation;

            return new AssetSuitability
            {
                F1Parameters = f1Score,
                SuitabilityRating = GetRatingScore(f1Score.Total + f0Score.Total),
                F0Parameters = f0Score,
                TotalScore = f1Score.Total + f0Score.Total
            };
        }

        #region international equity suitability helpers

        private void SetF1Score_InternationalEquity(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            SetForecastFiveYearReturnScore_IE(table, f1Score);
            SetForecastMorningstarScore_IE(f1Score, table);
            SetForecastDividendYieldScore_IE(table, f1Score);
            SetForecastReturnOnAssetScore_IE(table, f1Score);
            SetForecastReturnOnEquityScore_IE(table, f1Score);
            SetForecastFinancialLeverageScore_IE(table, f1Score);
            SetForecastOneYearRevenueGrowthScore_IE(table, f1Score);
            SetForecastDERatioScore_IE(f1Score, table);
            SetForecastCreditRatingScore_IE(f1Score, table);
            SetForecastFairValueVariationScore_IE(table, f1Score);
        }

        private void SetForecastFairValueVariationScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            var fairValueVariation = F1Recommendation.FairValueVariation;
            if (fairValueVariation.HasValue)
            {
                if (fairValueVariation.Value >= table.Ief1Parameters.Defensive.FairValueVariation)
                {
                    f1Score.FairValueVariation = table.Ief1Parameters.Defensive.ScoreRanking;
                }
                else if (fairValueVariation >= table.Ief1Parameters.Conservative.FairValueVariation)
                {
                    f1Score.FairValueVariation = table.Ief1Parameters.Conservative.ScoreRanking;
                }
                else if (fairValueVariation >= table.Ief1Parameters.Balance.FairValueVariation)
                {
                    f1Score.FairValueVariation = table.Ief1Parameters.Balance.ScoreRanking;
                }
                else if (fairValueVariation >= table.Ief1Parameters.Assertive.FairValueVariation)
                {
                    f1Score.FairValueVariation = table.Ief1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.FairValueVariation = table.Ief1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Fair value variation is not populated for current asset, which is of type International Equity");
            }
        }

        private void SetForecastCreditRatingScore_IE(IEForecastParameter f1Score,
            InternationalEquitySuitabilityParameters table)
        {
            if (F1Recommendation.CreditRating == CreditRating.Aaa ||
                F1Recommendation.CreditRating == CreditRating.AaaPlus ||
                F1Recommendation.CreditRating == CreditRating.AaaMinus)
            {
                f1Score.CreditRating = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.CreditRating == CreditRating.AaPlus ||
                     F1Recommendation.CreditRating == CreditRating.Aa ||
                     F1Recommendation.CreditRating == CreditRating.AaMinus)
            {
                f1Score.CreditRating = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.CreditRating == CreditRating.A ||
                     F1Recommendation.CreditRating == CreditRating.APlus ||
                     F1Recommendation.CreditRating == CreditRating.AMinus)
            {
                f1Score.CreditRating = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.CreditRating == CreditRating.Bbb ||
                     F1Recommendation.CreditRating == CreditRating.BbbMinus ||
                     F1Recommendation.CreditRating == CreditRating.BbbPlus)
            {
                f1Score.CreditRating = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.CreditRating = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastDERatioScore_IE(IEForecastParameter f1Score,
            InternationalEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                f1Score.DERatioF1 = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.Ief1Parameters.Defensive.DERatioF1)
            {
                f1Score.DERatioF1 = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.Ief1Parameters.Conservative.DERatioF1)
            {
                f1Score.DERatioF1 = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.Ief1Parameters.Balance.DERatioF1)
            {
                f1Score.DERatioF1 = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.DebtEquityRatio <= table.Ief1Parameters.Assertive.DERatioF1)
            {
                f1Score.DERatioF1 = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.DERatioF1 = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastOneYearRevenueGrowthScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            var oneYearGrowth = F1Recommendation.OneYearRevenueGrowth;
            if (oneYearGrowth.HasValue)
            {
                if (oneYearGrowth.Value >= table.Ief1Parameters.Defensive.OneYearRevenueGrowthF1)
                {
                    f1Score.OneYearRevenueGrowthF1 = table.Ief1Parameters.Defensive.ScoreRanking;
                }
                else if (oneYearGrowth.Value >= table.Ief1Parameters.Conservative.OneYearRevenueGrowthF1)
                {
                    f1Score.OneYearRevenueGrowthF1 = table.Ief1Parameters.Conservative.ScoreRanking;
                }
                else if (oneYearGrowth.Value >= table.Ief1Parameters.Balance.OneYearRevenueGrowthF1)
                {
                    f1Score.OneYearRevenueGrowthF1 = table.Ief1Parameters.Balance.ScoreRanking;
                }
                else if (oneYearGrowth.Value >= table.Ief1Parameters.Assertive.OneYearRevenueGrowthF1)
                {
                    f1Score.OneYearRevenueGrowthF1 = table.Ief1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.OneYearRevenueGrowthF1 = table.Ief1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "One Year Revenue Growth is not populated for current asset, which is of type International Equity");
            }
        }

        private void SetForecastFinancialLeverageScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            var financialLeverage = F1Recommendation.FinancialLeverage;
            if (financialLeverage.HasValue)
            {
                if (financialLeverage.Value <= table.Ief1Parameters.Defensive.FinancialLeverageF1)
                {
                    f1Score.FinancialLeverageF1 = table.Ief1Parameters.Defensive.ScoreRanking;
                }
                else if (financialLeverage.Value <= table.Ief1Parameters.Conservative.FinancialLeverageF1)
                {
                    f1Score.FinancialLeverageF1 = table.Ief1Parameters.Conservative.ScoreRanking;
                }
                else if (financialLeverage.Value <= table.Ief1Parameters.Balance.FiveYearTotalReturn)
                {
                    f1Score.FinancialLeverageF1 = table.Ief1Parameters.Balance.ScoreRanking;
                }
                else if (financialLeverage.Value <= table.Ief1Parameters.Assertive.FiveYearTotalReturn)
                {
                    f1Score.FinancialLeverageF1 = table.Ief1Parameters.Assertive.ScoreRanking;
                }
                else
                {
                    f1Score.FinancialLeverageF1 = table.Ief1Parameters.Aggressive.ScoreRanking;
                }
            }
            else
            {
                throw new Exception(
                    "Financial Leverage is not populated for current asset, which is of type International Equity");
            }
        }

        private void SetForecastReturnOnEquityScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            if (F1Recommendation.ReturnOnEquity >= table.Ief1Parameters.Defensive.ROEF1)
            {
                f1Score.ROEF1 = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.Ief1Parameters.Conservative.ROEF1)
            {
                f1Score.ROEF1 = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.Ief1Parameters.Balance.ROEF1)
            {
                f1Score.ROEF1 = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnEquity >= table.Ief1Parameters.Assertive.ROEF1)
            {
                f1Score.ROEF1 = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.ROEF1 = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastReturnOnAssetScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            if (F1Recommendation.ReturnOnAsset >= table.Ief1Parameters.Defensive.ROAF1)
            {
                f1Score.ROAF1 = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.Ief1Parameters.Conservative.ROAF1)
            {
                f1Score.ROAF1 = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.Ief1Parameters.Balance.ROAF1)
            {
                f1Score.ROAF1 = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.ReturnOnAsset >= table.Ief1Parameters.Assertive.ROAF1)
            {
                f1Score.ROAF1 = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.ROAF1 = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastDividendYieldScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            if (F1Recommendation.DividendYield >= table.Ief1Parameters.Defensive.DividendYield)
            {
                f1Score.DividendYield = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.Ief1Parameters.Conservative.DividendYield)
            {
                f1Score.DividendYield = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.Ief1Parameters.Balance.DividendYield)
            {
                f1Score.DividendYield = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.DividendYield >= table.Ief1Parameters.Assertive.DividendYield)
            {
                f1Score.ScoreRanking = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.ScoreRanking = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastMorningstarScore_IE(IEForecastParameter f1Score,
            InternationalEquitySuitabilityParameters table)
        {
            if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Buy ||
                F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.HighlyRecommended)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Accumulate ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Recommended)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Hold ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.InvestmentGrade)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Reduce)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Sell ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.Avoid ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.UnderReview)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
            else if (F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.CeasedCoverage ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.None ||
                     F1Recommendation.MorningstarRecommendation == MorningStarRecommendation.NoRecommendation)
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Aggressive.ScoreRanking*20;
            }
            else
            {
                f1Score.MorningStarRecommendation = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetForecastFiveYearReturnScore_IE(InternationalEquitySuitabilityParameters table,
            IEForecastParameter f1Score)
        {
            var fiveYearTotalReturn = F1Recommendation.FiveYearTotalReturn;
            if (!fiveYearTotalReturn.HasValue)
            {
                throw new Exception(
                    "Five year total return is not populated for current asset, which is of type International Equity.");
            }
            if (fiveYearTotalReturn.Value >= table.Ief1Parameters.Defensive.FiveYearTotalReturn)
            {
                f1Score.FiveYearTotalReturn = table.Ief1Parameters.Defensive.ScoreRanking;
            }
            else if (fiveYearTotalReturn.Value >= table.Ief1Parameters.Conservative.FiveYearTotalReturn)
            {
                f1Score.FiveYearTotalReturn = table.Ief1Parameters.Conservative.ScoreRanking;
            }
            else if (fiveYearTotalReturn.Value >= table.Ief1Parameters.Balance.FiveYearTotalReturn)
            {
                f1Score.FiveYearTotalReturn = table.Ief1Parameters.Balance.ScoreRanking;
            }
            else if (fiveYearTotalReturn.Value >= table.Ief1Parameters.Assertive.FiveYearTotalReturn)
            {
                f1Score.FiveYearTotalReturn = table.Ief1Parameters.Assertive.ScoreRanking;
            }
            else
            {
                f1Score.FiveYearTotalReturn = table.Ief1Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetF0Score_InternationalEquity(InternationalEquitySuitabilityParameters table,
            IECurrentParameter f0Score)
        {
            SetCurrentOneYearReturnScore_IE(table, f0Score);
            SetCurrenCapitalisationScore_IE(table, f0Score);
            SetCurrentDividendScore_IE(table, f0Score);
            SetCurrentReturnOnAssetScore_IE(table, f0Score);
            SetCurrentReturnOnEquityScore_IE(table, f0Score);
            SetCurrentQuickRatioScore_IE(table, f0Score);
            SetCurrentCurrentRatioScore_IE(f0Score, table);
            SetCurrentDebtEquityRatioScore_IE(f0Score, table);
            SetCurrentPriceEarningRatioScore_IE(table, f0Score);
            SetCurrentBetaFiveYearsScore_IE(table, f0Score);
        }

        private void SetCurrentBetaFiveYearsScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.BetaFiveYears <= table.Ief0Parameters.Defensive.BetaFiveYear)
            {
                i0Score.BetaFiveYear = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Ief0Parameters.Conservative.BetaFiveYear)
            {
                i0Score.BetaFiveYear = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Ief0Parameters.Balance.BetaFiveYear)
            {
                i0Score.BetaFiveYear = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.BetaFiveYears <= table.Ief0Parameters.Assertive.BetaFiveYear)
            {
                i0Score.BetaFiveYear = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.BetaFiveYear = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentPriceEarningRatioScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.PriceEarningRatio <= table.Ief0Parameters.Defensive.PERatio)
            {
                i0Score.PERatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.Ief0Parameters.Conservative.PERatio)
            {
                i0Score.PERatio = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.Ief0Parameters.Balance.PERatio)
            {
                i0Score.PERatio = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.PriceEarningRatio <= table.Ief0Parameters.Assertive.PERatio)
            {
                i0Score.PERatio = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.PERatio = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentDebtEquityRatioScore_IE(IECurrentParameter i0Score,
            InternationalEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.Ief0Parameters.Defensive.TotalDebtTotalEquityRatio)
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.Ief0Parameters.Conservative.TotalDebtTotalEquityRatio)
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.Ief0Parameters.Balance.TotalDebtTotalEquityRatio)
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.DebtEquityRatio <= table.Ief0Parameters.Assertive.TotalDebtTotalEquityRatio)
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.TotalDebtTotalEquityRatio = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentCurrentRatioScore_IE(IECurrentParameter i0Score,
            InternationalEquitySuitabilityParameters table)
        {
            if (Sector == "Financial Services")
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.CurrentRatio >= table.Ief0Parameters.Defensive.CurrentRatio)
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.CurrentRatio >= table.Ief0Parameters.Conservative.CurrentRatio)
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.CurrentRatio >= table.Ief0Parameters.Balance.CurrentRatio)
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.CurrentRatio >= table.Ief0Parameters.Assertive.CurrentRatio)
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.CurrentRatio = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentQuickRatioScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.QuickRatio >= table.Ief0Parameters.Defensive.QuickRatio)
            {
                i0Score.QuickRatio = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.QuickRatio >= table.Ief0Parameters.Conservative.QuickRatio)
            {
                i0Score.QuickRatio = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.QuickRatio >= table.Ief0Parameters.Balance.QuickRatio)
            {
                i0Score.QuickRatio = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.QuickRatio >= table.Ief0Parameters.Assertive.QuickRatio)
            {
                i0Score.QuickRatio = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.QuickRatio = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentReturnOnEquityScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.ReturnOnEquity >= table.Ief0Parameters.Defensive.ROE)
            {
                i0Score.ROE = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.Ief0Parameters.Conservative.ROE)
            {
                i0Score.ROE = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.Ief0Parameters.Balance.ROE)
            {
                i0Score.ROE = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnEquity >= table.Ief0Parameters.Assertive.ROE)
            {
                i0Score.ROE = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.ROE = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentReturnOnAssetScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.ReturnOnAsset >= table.Ief0Parameters.Defensive.ROA)
            {
                i0Score.ROA = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.Ief0Parameters.Conservative.ROA)
            {
                i0Score.ROA = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.Ief0Parameters.Balance.ROA)
            {
                i0Score.ROA = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.ReturnOnAsset >= table.Ief0Parameters.Assertive.ROA)
            {
                i0Score.ROA = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.ROA = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentDividendScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.DividendYield >= table.Ief0Parameters.Defensive.DividendYield)
            {
                i0Score.DividendYield = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.Ief0Parameters.Conservative.DividendYield)
            {
                i0Score.DividendYield = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.Ief0Parameters.Balance.DividendYield)
            {
                i0Score.DividendYield = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.DividendYield >= table.Ief0Parameters.Assertive.DividendYield)
            {
                i0Score.DividendYield = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.DividendYield = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrenCapitalisationScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.Capitalisation >= table.Ief0Parameters.Defensive.CurrentMarketCapitalisatiopn)
            {
                i0Score.CurrentMarketCapitalisatiopn = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.Ief0Parameters.Conservative.CurrentMarketCapitalisatiopn)
            {
                i0Score.CurrentMarketCapitalisatiopn = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.Ief0Parameters.Balance.CurrentMarketCapitalisatiopn)
            {
                i0Score.CurrentMarketCapitalisatiopn = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.Capitalisation >= table.Ief0Parameters.Assertive.CurrentMarketCapitalisatiopn)
            {
                i0Score.CurrentMarketCapitalisatiopn = table.Ief0Parameters.Assertive.ScoreRanking;
            }
            else
            {
                i0Score.CurrentMarketCapitalisatiopn = table.Ief0Parameters.Aggressive.ScoreRanking;
            }
        }

        private void SetCurrentOneYearReturnScore_IE(InternationalEquitySuitabilityParameters table,
            IECurrentParameter i0Score)
        {
            if (F0Ratios.OneYearReturn >= table.Ief0Parameters.Defensive.OneYearTotalReturn)
            {
                i0Score.OneYearTotalReturn = table.Ief0Parameters.Defensive.ScoreRanking;
            }
            else if (F0Ratios.OneYearReturn >= table.Ief0Parameters.Conservative.OneYearTotalReturn)
            {
                i0Score.OneYearTotalReturn = table.Ief0Parameters.Conservative.ScoreRanking;
            }
            else if (F0Ratios.OneYearReturn >= table.Ief0Parameters.Balance.OneYearTotalReturn)
            {
                i0Score.OneYearTotalReturn = table.Ief0Parameters.Balance.ScoreRanking;
            }
            else if (F0Ratios.OneYearReturn >= table.Ief0Parameters.Assertive.OneYearTotalReturn)
            {
                i0Score.OneYearTotalReturn = table.Ief0Parameters.Assertive.OneYearTotalReturn;
            }
            else
            {
                i0Score.OneYearTotalReturn = table.Ief0Parameters.Aggressive.OneYearTotalReturn;
            }
        }

        #endregion
    }
}