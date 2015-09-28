using System;
using Shared;


namespace Domain.Portfolio.SuitabilityLookupTables
{
    public class ScoreRatingConverter
    {
        public static SuitabilityRating Convert(double score)
        {
            if (score >= 0 && score <= 70)
            {
                return SuitabilityRating.Defensive;
            }
            if (score > 70 && score <= 90)
            {
                return SuitabilityRating.Conservative;
            }
            if (score > 90 && score <= 110)
            {
                return SuitabilityRating.Balance;
            }
            if (score > 110 && score <= 130)
            {
                return SuitabilityRating.Assertive;
            }
            if (score > 130 && score <= 200)
            {
                return SuitabilityRating.Aggresive;
            }
            if (score > 200)
            {
                return SuitabilityRating.Danger;
            }
            throw new NotSupportedException("Score needs to be positive");
        }
    }
}