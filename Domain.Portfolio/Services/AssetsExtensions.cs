using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.Values.Ratios;
using Domain.Portfolio.Values.Weighting;

namespace Domain.Portfolio.Services
{
    /// <summary>
    ///     Extension methods to calculate statistics for asset collections
    /// </summary>
    public static class AssetsExtensions    
    {
        /// <summary>
        ///     Get weighting for each asset within collection
        /// </summary>
        /// <param name="assets"></param>
        /// <returns>A list of weightings with corresponding weight-able object (e.g. an asset instance)</returns>
        public static List<Weighting> GetAssetWeightings(this List<AssetBase> assets)
        {
            var totalValue = assets.GetTotalMarketValue();
            //if (totalValue == 0)
            //{
            //    throw new Exception("Total market value of assets cannot be zero");
            //}
            return assets.Select(a => new Weighting
            {
                Percentage = totalValue == 0 ? 0 : a.GetTotalMarketValue() / totalValue,
                Weightable = a
            }).ToList();
        }

        #region diversification calculations

        public static async Task<Dictionary<string, double>> GetAssetSectorialDiversification<TEquityType>(
            this List<AssetBase> assets, IRepository repository)
            where TEquityType : Equity
        {
            var assetsOfInterest = assets.OfType<TEquityType>().ToList();
            var allSectors = await repository.GetAllSectors();
            return allSectors.Distinct()
                .ToDictionary(sector => sector, sector => assetsOfInterest
                    .Where(a => a.Sector == sector)
                    .Sum(a => a.GetTotalMarketValue()));
        }

        public static Dictionary<string, double> GetAssetSectorialDiversificationSync<TEquityType>(             //added
            this List<AssetBase> assets, IRepository repository)
            where TEquityType : Equity
        {
            var assetsOfInterest = assets.OfType<TEquityType>().ToList();
            var allSectors = repository.GetAllSectorsSync();
            return allSectors.Distinct()
                .ToDictionary(sector => sector, sector => assetsOfInterest
                    .Where(a => a.Sector == sector)
                    .Sum(a => a.GetTotalMarketValue()));
        }

        public static async Task<Dictionary<string, double>> GetFixedIncomeTypeDiversification(
            this List<AssetBase> assets, IRepository repository)
        {
            var allBondTypes = await repository.GetAllBondTypes();
            var assetOfInterest = assets.OfType<FixedIncome>();
            return allBondTypes.Distinct()
                .ToDictionary(btype => btype,
                    btype => assetOfInterest
                        .Where(a => a.BondType == btype)
                        .Sum(a => a.GetTotalMarketValue()));
        }

        public static async Task<Dictionary<string, double>> GetDirectPropertyStateDiversification(this List<AssetBase> assets,
            IRepository repository)
        {
            var states = await repository.GetAllAustralianStates();

            var assetOfInterests = assets.OfType<DirectProperty>();
            return states.Distinct().ToDictionary(state => state,
                state => assetOfInterests.Where(a => a.State == state).Sum(a => a.GetTotalMarketValue()));
        }

        public static async Task<Dictionary<string, double>> GetDirectPropertyTypeDiversification(this List<AssetBase> assets,
            IRepository repository)
        {
            var propertyTypes = await repository.GetAllPropertyTypes();
            var assetOfInterest = assets.OfType<DirectProperty>();
            return propertyTypes.Distinct()
                .ToDictionary(ptype => ptype,
                    ptype => assetOfInterest.Where(a => a.PropertyType == ptype).Sum(a => a.GetTotalMarketValue()));
        }

        public static async Task<Dictionary<string, double>> GetCashAccountTypeDiversification(this List<AssetBase> assets,
            IRepository repository)
        {
            var allCashAccountTypes = await repository.GetAllCashAccountTypes();
            var assetOfInterest = assets.OfType<Cash>();
            return allCashAccountTypes.Distinct()
                .ToDictionary(ctype => ctype.ToString(),
                    ctype => assetOfInterest.Where(a => a.CashAccountType == ctype).Sum(a => a.GetTotalMarketValue()));
        }

        #endregion

        #region Income/Cost calculation

        public static double GetTotalIncome(this List<AssetBase> assets)
        {
            return assets.Sum(a => a.GetIncome().TotalAmount);
        }

        public static double GetTotalMarketValue(this List<AssetBase> assets)
        {
            var totalMarketValue = assets.Sum(a => a.GetTotalMarketValue());
            return totalMarketValue;
        }

        public static double GetTotalMarketValue_ByEquityType<TEquity>(this List<AssetBase> assets)
            where TEquity : Equity
        {
            return
                assets.OfType<TEquity>()
                    .Sum(a => a.GetTotalMarketValue());
        }

        public static double GetTotalMarketValue_ByAssetType<TAssetType>(this List<AssetBase> assets)
            where TAssetType : AssetBase
        {
            return assets.Where(a => a is TAssetType).Sum(a => a.GetTotalMarketValue());
        }

        public static double GetProfitAndLoss(this List<AssetBase> assets)
        {
            return assets.GetTotalMarketValue() + assets.GetTotalIncome() - assets.GetTotalCost().Total;
        }

        public static Cost GetTotalCost(this List<AssetBase> assets)
        {
            var allCosts = assets.Select(a => a.GetCost()).ToList();
            return new Cost
            {
                Expense = allCosts.Sum(c => c.Expense),
                AssetCost = allCosts.Sum(c => c.AssetCost),
                Total = allCosts.Sum(c => c.Total)
            };
        }

        #endregion

        #region monthly cash flow generation

        public static List<Cashflow> GetMonthlyCashflows(this List<AssetBase> assets)
        {
            List<ActivityBase> activities = new List<ActivityBase>();
            List<Cashflow> result = new List<Cashflow>();

            foreach (var asset in assets)
            {
                activities.AddRange(asset.GetActivitiesSync());
            }

            List<string> months = new List<string>();

            for (int i = 1; i <= 12; i++)
            {
                var time = DateTime.Now.AddMonths(i - 12);
                months.Add(time.ToString("MMM-yyyy"));
            }

            var activityGroups = activities.GroupBy(ac => ac.ActivityDate.ToString("MMM-yyyy"));

            foreach (var monthly in months)
            {
                Cashflow flow = new Cashflow()
                {
                    Expenses = activityGroups.Any(ac => ac.Key == monthly) ? activityGroups.Where(ac => ac.Key == monthly).Sum(ac => ac.Sum(m => m.Expenses.Sum(ex => ex.Amount))) : 0,
                    Income = activityGroups.Any(ac => ac.Key == monthly) ? activityGroups.Where(ac => ac.Key == monthly).Sum(ac => ac.Sum(m => m.Incomes.Sum(inc => inc.Amount))) : 0,
                    Month = monthly.Split('-').First()
                };
                result.Add(flow);
            }
            return result;
        }

        #endregion

        #region ratios


        public static Ratios GetAverageRatiosFor<TEquity>(this List<AssetBase> assets) where TEquity : Equity//, new ()
        {
            var ratios = new Ratios()
            {
                Beta = 0,                           // Beta
                BetaFiveYears = 0,
                Capitalisation = 0,
                CurrentRatio = 0,                   // Current Ratio
                DebtEquityRatio = 0,                //	Debt/Equity
                DividendYield = 0,                  //Dividend Yield
                EarningsStability = 0,              //Earnings Stability
                EpsGrowth = 0,
                FiveYearAlphaRatio = 0,
                FiveYearInformation = 0,
                FiveYearReturn = 0,                 // 5 Year Return
                FiveYearSharpRatio = 0,
                FiveYearSkewnessRatio = 0,
                FiveYearStandardDeviation = 0,
                FiveYearTrackingErrorRatio = 0,
                Frank = 0,                          //	Franking	
                FundSize = 0,
                GlobalCategory = 0,
                InterestCover = 0,                  //	Interest Cover
                OneYearReturn = 0,                  //1 Year Return
                PayoutRatio = 0,                    //Payout Ratio
                PriceEarningRatio = 0,              //Price Earnings Ratio
                QuickRatio = 0,                     // Quick Ratio
                ReturnOnAsset = 0,                  //Return on Asset	
                ReturnOnEquity = 0,                 //	Return on Equity
                ThreeYearReturn = 0                 // 3 Year Return
            };
            var weightings = assets.GetAssetWeightings();

            foreach (var asset in weightings.Where(asset=>asset.Weightable is TEquity))
            {
            
                var equity =  asset.Weightable as TEquity;
                ratios.Frank += asset.Percentage*equity.F0Ratios.Frank;
                ratios.Beta += asset.Percentage*equity.F0Ratios.Beta;
                ratios.BetaFiveYears += asset.Percentage*equity.F0Ratios.BetaFiveYears;
                ratios.Capitalisation += asset.Percentage*equity.F0Ratios.Capitalisation;
                ratios.CurrentRatio += asset.Percentage*equity.F0Ratios.CurrentRatio;
                ratios.DebtEquityRatio += asset.Percentage*equity.F0Ratios.DebtEquityRatio;
                ratios.DividendYield += asset.Percentage*equity.F0Ratios.DividendYield;
                ratios.EarningsStability += asset.Percentage*equity.F0Ratios.EarningsStability;
                ratios.EpsGrowth += asset.Percentage*equity.F0Ratios.EpsGrowth;
                ratios.FiveYearAlphaRatio += asset.Percentage*equity.F0Ratios.FiveYearAlphaRatio;
                ratios.FiveYearInformation += asset.Percentage*equity.F0Ratios.FiveYearInformation;
                ratios.FiveYearReturn += asset.Percentage*equity.F0Ratios.FiveYearReturn;
                ratios.FiveYearSkewnessRatio += asset.Percentage*equity.F0Ratios.FiveYearSkewnessRatio;
                ratios.FiveYearSharpRatio += asset.Percentage*equity.F0Ratios.FiveYearSharpRatio;
                ratios.FiveYearStandardDeviation += asset.Percentage*equity.F0Ratios.FiveYearStandardDeviation;
                ratios.FiveYearTrackingErrorRatio += asset.Percentage*equity.F0Ratios.FiveYearTrackingErrorRatio;
                ratios.FundSize += asset.Percentage*equity.F0Ratios.FundSize;
                ratios.GlobalCategory += asset.Percentage*equity.F0Ratios.GlobalCategory;
                ratios.InterestCover += asset.Percentage*equity.F0Ratios.InterestCover;
                ratios.OneYearReturn += asset.Percentage*equity.F0Ratios.OneYearReturn;
                ratios.PayoutRatio += asset.Percentage*equity.F0Ratios.PayoutRatio;
                ratios.PriceEarningRatio += asset.Percentage*equity.F0Ratios.PriceEarningRatio;
                ratios.QuickRatio += asset.Percentage*equity.F0Ratios.QuickRatio;
                ratios.ReturnOnEquity += asset.Percentage*equity.F0Ratios.ReturnOnEquity;
                ratios.ReturnOnAsset += asset.Percentage*equity.F0Ratios.ReturnOnAsset;
                ratios.ThreeYearReturn += asset.Percentage*equity.F0Ratios.ThreeYearReturn;
            }


            return ratios;

        }

        public static Recommendation GetAverageExpectedFor<TEquity>(this List<AssetBase> assets) where TEquity : Equity//, new ()
        {
            var expected = new Recommendation()
            {
                CreditRating = 0,
                MorningStarAnalyst = 0,
                OneYearAlpha = 0, 
                ReturnOnAsset = 0, 
                DebtEquityRatio = 0,
                DividendYield = 0,
                EpsGrowth = 0,
                Frank = 0,
                FairValueVariation = 0,
                FinancialLeverage = 0,
                FiveYearTotalReturn = 0,
                InterestCover = 0,
                IntrinsicValue = 0,
                MaxManagementExpenseRatio = 0,
                MorningstarRecommendation = 0,
                OneYearBeta = 0,
                OneYearInformationRatio = 0,
                OneYearReturn = 0,
                OneYearRevenueGrowth = 0,
                OneYearSharpRatio = 0,
                OneYearTrackingError = 0,
                PerformanceFee = 0,
                PriceEarningRatio= 0,
                ReturnOnEquity = 0,
                YearsSinceInception = 0,
            };
            var weightings = assets.GetAssetWeightings();

            foreach (var asset in weightings.Where(asset => asset.Weightable is TEquity))
            {

                var equity = asset.Weightable as TEquity;
                expected.Frank += asset.Percentage * equity.F1Recommendation.Frank;
                //ratios.CreditRating += asset.Percentage * equity.F1Recommendation.CreditRating;
                expected.DebtEquityRatio += asset.Percentage * equity.F1Recommendation.DebtEquityRatio;
                expected.DividendYield += asset.Percentage * equity.F1Recommendation.DividendYield;
                expected.EpsGrowth += asset.Percentage * equity.F1Recommendation.EpsGrowth;
                expected.FairValueVariation += asset.Percentage * equity.F1Recommendation.FairValueVariation;
                expected.FinancialLeverage += asset.Percentage * equity.F1Recommendation.FinancialLeverage;
                expected.FiveYearTotalReturn += asset.Percentage * equity.F1Recommendation.FiveYearTotalReturn;
                expected.InterestCover += asset.Percentage * equity.F1Recommendation.InterestCover;
                expected.IntrinsicValue += asset.Percentage * equity.F1Recommendation.IntrinsicValue;
                expected.MaxManagementExpenseRatio += asset.Percentage * equity.F1Recommendation.MaxManagementExpenseRatio;
                //ratios.MorningStarAnalyst += asset.Percentage * equity.F1Recommendation.MorningStarAnalyst;
                //ratios.MorningstarRecommendation += asset.Percentage * equity.F1Recommendation.MorningstarRecommendation;
                expected.OneYearAlpha += asset.Percentage * equity.F1Recommendation.OneYearAlpha;
                expected.OneYearBeta += asset.Percentage * equity.F1Recommendation.OneYearBeta;
                expected.OneYearInformationRatio += asset.Percentage * equity.F1Recommendation.OneYearInformationRatio;
                expected.OneYearReturn += asset.Percentage * equity.F1Recommendation.OneYearReturn;
                expected.OneYearRevenueGrowth += asset.Percentage * equity.F1Recommendation.OneYearRevenueGrowth;
                expected.OneYearSharpRatio += asset.Percentage * equity.F1Recommendation.OneYearSharpRatio;
                expected.OneYearTrackingError += asset.Percentage * equity.F1Recommendation.OneYearTrackingError;
                expected.PerformanceFee += asset.Percentage * equity.F1Recommendation.PerformanceFee;
                expected.PriceEarningRatio += asset.Percentage * equity.F1Recommendation.PriceEarningRatio;
                expected.ReturnOnAsset += asset.Percentage * equity.F1Recommendation.ReturnOnAsset;
                expected.ReturnOnEquity += asset.Percentage * equity.F1Recommendation.ReturnOnEquity;
            }


            return expected;

        }

        #endregion

    }
}