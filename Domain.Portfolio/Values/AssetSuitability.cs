using Domain.Portfolio.Base;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.Values
{
    public class AssetSuitability : ValueBase
    {
        public SuitabilityRating SuitabilityRating { get; set; }
        public double TotalScore { get; set; }

        /// <summary>
        ///     All f0 parameters, please type cast to either AE/IE/MI equivalent for further usage
        /// </summary>
        public ISuitabilityCurrentParameter F0Parameters { get; set; }

        /// <summary>
        ///     All f1 parameters, please type cast to either AE/IE/MI equivalent for further usage
        /// </summary>
        public ISuitabilityForecastParameter F1Parameters { get; set; }
    }
}