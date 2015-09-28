using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;

namespace Domain.Portfolio.SuitabilityLookupTables.Tables
{
    public class PropertySuitabilityParameters
    {
        public PropertySuitabilityParameters()
        {
            Defensive = new PParameter
            {
                ScoreRanking = 2,
                AbilityToPayAboveCurrentInterestRate = 0.07,
                CurrentAverageAgeOfClient = 25,
                PropertyLeverage = 0.2,
                YearsToRetirement = 40
            };
            Conservative = new PParameter
            {
                ScoreRanking = 4,
                PropertyLeverage = 0.3,
                AbilityToPayAboveCurrentInterestRate = 0.06,
                CurrentAverageAgeOfClient = 35,
                YearsToRetirement = 30
            };
            Balance = new PParameter
            {
                ScoreRanking = 6,
                PropertyLeverage = 0.4,
                AbilityToPayAboveCurrentInterestRate = 0.05,
                CurrentAverageAgeOfClient = 45,
                YearsToRetirement = 20
            };
            Assertive = new PParameter
            {
                ScoreRanking = 8,
                PropertyLeverage = 0.5,
                AbilityToPayAboveCurrentInterestRate = 0.04,
                CurrentAverageAgeOfClient = 55,
                YearsToRetirement = 10
            };
            Aggressive = new PParameter
            {
                ScoreRanking = 10,
                PropertyLeverage = 0.6,
                AbilityToPayAboveCurrentInterestRate = 0.03,
                CurrentAverageAgeOfClient = 65,
                YearsToRetirement = 0
            };
            MaxScore = new PParameter
            {
                ScoreRanking = 100,
                PropertyLeverage = 50,
                AbilityToPayAboveCurrentInterestRate = 50,
                CurrentAverageAgeOfClient = 10,
                YearsToRetirement = 10
            };
        }

        public PParameter Defensive { get; set; }
        public PParameter Conservative { get; set; }
        public PParameter Balance { get; set; }
        public PParameter Assertive { get; set; }
        public PParameter Aggressive { get; set; }
        public PParameter MaxScore { get; set; }
    }
}