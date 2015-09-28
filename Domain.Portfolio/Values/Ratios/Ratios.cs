using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.Ratios
{
    public class Ratios : ValueBase
    {
        public double PriceEarningRatio { get; set; }
        public double ReturnOnAsset { get; set; }
        public double ReturnOnEquity { get; set; }
        public double OneYearReturn { get; set; }
        public double ThreeYearReturn { get; set; }
        public double FiveYearReturn { get; set; }
        public double Beta { get; set; }
        public double BetaFiveYears { get; set; }
        public double CurrentRatio { get; set; }
        public double QuickRatio { get; set; }
        public double DebtEquityRatio { get; set; }
        public double InterestCover { get; set; }
        public double PayoutRatio { get; set; }
        public double EarningsStability { get; set; }
        public double EpsGrowth { get; set; }
        public double Capitalisation { get; set; }
        public double DividendYield { get; set; }
        public double Frank { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearAlphaRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearInformation { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearStandardDeviation { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearSkewnessRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearTrackingErrorRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FiveYearSharpRatio { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? GlobalCategory { get; set; }

        /// <summary>
        ///     This property is exclusive for managed fund
        /// </summary>
        public double? FundSize { get; set; }
    }
}