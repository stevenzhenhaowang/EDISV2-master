

using Shared;

namespace Domain.Portfolio.Values.Ratios
{
    public class Recommendation
    {
        public double EpsGrowth { get; set; }
        public MorningStarRecommendation MorningstarRecommendation { get; set; }
        public double DividendYield { get; set; }
        public double Frank { get; set; }
        public double ReturnOnAsset { get; set; }
        public double ReturnOnEquity { get; set; }
        public double InterestCover { get; set; }
        public double DebtEquityRatio { get; set; }
        public double PriceEarningRatio { get; set; }
        public double IntrinsicValue { get; set; }

        /// <summary>
        ///     This property is exclusive for international equity
        /// </summary>
        public double? FiveYearTotalReturn { get; set; }

        /// <summary>
        ///     This property is exclusive for international equity
        /// </summary>
        public double? FinancialLeverage { get; set; }

        /// <summary>
        ///     This property is exclusive for international equity
        /// </summary>
        public double? OneYearRevenueGrowth { get; set; }

        /// <summary>
        ///     This property is exclusive for international equity, and none will be used for other asset types
        /// </summary>
        public CreditRating CreditRating { get; set; }

        /// <summary>
        ///     This property is exclusive for international equity
        /// </summary>
        public double? FairValueVariation { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearReturn { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearAlpha { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearBeta { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearInformationRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearTrackingError { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? OneYearSharpRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? MaxManagementExpenseRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? PerformanceFee { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? YearsSinceInception { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public MorningStarAnalyst MorningStarAnalyst { get; set; }
    }
}