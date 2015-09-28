using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Entities.Transactions;
using Domain.Portfolio.Interfaces;
using Domain.Portfolio.SuitabilityLookupTables.Tables;
using Domain.Portfolio.SuitabilityLookupTables.Tables.ParameterModel;
using Domain.Portfolio.Values;
using Domain.Portfolio.Values.Income;
using Shared;



namespace Domain.Portfolio.AggregateRoots.Asset
{
    public class DirectProperty : AssetBase, IEvaluable
    {
        public DirectProperty(IRepository repo) : base(repo)
        {
        }
        public string PlaceId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string PropertyType { get; set; }
        public double ClientAverageAge { get; set; }
        public double YearsToRetirement { get; set; }
        public double PropertyLeverage { get; set; }
        public double AbilityToPayAboveCurrentInterestRate { get; set; }
        public AssetSuitability GetRating()
        {
            var table = new PropertySuitabilityParameters();
            var score = new PParameter();

            SetClientAverageAgeScore(table, score);
            SetYearsToRetirementScore(table, score);
            SetPropertyLeverageScore(table, score);
            SetAbilityToPayInterestScore(table, score);
            score.Total = (score.CurrentAverageAgeOfClient + score.YearsToRetirement + score.PropertyLeverage +
                           score.AbilityToPayAboveCurrentInterestRate)*2.5;
            return new AssetSuitability
            {
                F1Parameters = score,
                SuitabilityRating = GetRatingScore(score.Total*2),
                TotalScore = score.Total*2,
                F0Parameters = score
            };
        }
        public override List<ActivityBase> GetActivities(DateTime? beforeDate=null)
        {
            return _repository.GetPropertyActivitiesForAccount(ClientAccountId, PlaceId, beforeDate??DateTime.Now).Result;
        }


        public override List<ActivityBase> GetActivitiesSync(DateTime? beforeDate = null)
        {
            return _repository.GetPropertyActivitiesForAccountSync(ClientAccountId, PlaceId, beforeDate ?? DateTime.Now);
        }
        public override Cost GetCost()
        {
            return GetCostForTransactionType<PropertyTradingTransaction>();
        }
        public override Income GetIncome()
        {
            return new PropertyRent
            {
                PlaceId = PlaceId,
                TotalAmount = GetActivitiesSync().Sum(a => a.Incomes.Where(i => i is PropertyRentalRecord)
                    .Sum(i => ((PropertyRentalRecord) i).Amount))
            };
        }
        private SuitabilityRating GetRatingScore(double score)
        {
            if (score < 0)
            {
                throw new Exception("Score must be greater or equal to 0");
            }

            if (score >= 200)
            {
                return SuitabilityRating.Danger;
            }
            if (score >= 131)
            {
                return SuitabilityRating.Aggresive;
            }
            if (score >= 111)
            {
                return SuitabilityRating.Assertive;
            }
            if (score >= 91)
            {
                return SuitabilityRating.Assertive;
            }
            if (score >= 70)
            {
                return SuitabilityRating.Conservative;
            }
            return SuitabilityRating.Defensive;
        }
        private void SetAbilityToPayInterestScore(PropertySuitabilityParameters table, PParameter f0Score)
        {
            if (AbilityToPayAboveCurrentInterestRate >= table.Defensive.AbilityToPayAboveCurrentInterestRate)
            {
                f0Score.AbilityToPayAboveCurrentInterestRate = table.Defensive.ScoreRanking;
            }
            else if (AbilityToPayAboveCurrentInterestRate >= table.Conservative.AbilityToPayAboveCurrentInterestRate)
            {
                f0Score.AbilityToPayAboveCurrentInterestRate = table.Conservative.ScoreRanking;
            }
            else if (AbilityToPayAboveCurrentInterestRate >= table.Balance.AbilityToPayAboveCurrentInterestRate)
            {
                f0Score.AbilityToPayAboveCurrentInterestRate = table.Balance.ScoreRanking;
            }
            else if (AbilityToPayAboveCurrentInterestRate >= table.Assertive.AbilityToPayAboveCurrentInterestRate)
            {
                f0Score.AbilityToPayAboveCurrentInterestRate = table.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.AbilityToPayAboveCurrentInterestRate = table.Aggressive.ScoreRanking;
            }
        }
        private void SetPropertyLeverageScore(PropertySuitabilityParameters table, PParameter f0Score)
        {
            if (PropertyLeverage <= table.Defensive.PropertyLeverage)
            {
                f0Score.PropertyLeverage = table.Defensive.ScoreRanking;
            }
            else if (PropertyLeverage <= table.Conservative.PropertyLeverage)
            {
                f0Score.PropertyLeverage = table.Conservative.ScoreRanking;
            }
            else if (PropertyLeverage <= table.Balance.PropertyLeverage)
            {
                f0Score.PropertyLeverage = table.Balance.ScoreRanking;
            }
            else if (PropertyLeverage <= table.Assertive.PropertyLeverage)
            {
                f0Score.PropertyLeverage = table.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.PropertyLeverage = table.Aggressive.ScoreRanking;
            }
        }
        private void SetYearsToRetirementScore(PropertySuitabilityParameters table, PParameter f0Score)
        {
            if (YearsToRetirement >= table.Defensive.YearsToRetirement)
            {
                f0Score.YearsToRetirement = table.Defensive.ScoreRanking;
            }
            else if (YearsToRetirement >= table.Conservative.YearsToRetirement)
            {
                f0Score.YearsToRetirement = table.Conservative.ScoreRanking;
            }
            else if (YearsToRetirement >= table.Balance.YearsToRetirement)
            {
                f0Score.YearsToRetirement = table.Balance.ScoreRanking;
            }
            else if (YearsToRetirement >= table.Assertive.YearsToRetirement)
            {
                f0Score.YearsToRetirement = table.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.YearsToRetirement = table.Aggressive.ScoreRanking;
            }
        }
        private void SetClientAverageAgeScore(PropertySuitabilityParameters table, PParameter f0Score)
        {
            if (ClientAverageAge <= table.Defensive.CurrentAverageAgeOfClient)
            {
                f0Score.CurrentAverageAgeOfClient = table.Defensive.ScoreRanking;
            }
            else if (ClientAverageAge <= table.Conservative.CurrentAverageAgeOfClient)
            {
                f0Score.CurrentAverageAgeOfClient = table.Conservative.ScoreRanking;
            }
            else if (ClientAverageAge <= table.Balance.CurrentAverageAgeOfClient)
            {
                f0Score.CurrentAverageAgeOfClient = table.Balance.ScoreRanking;
            }
            else if (ClientAverageAge <= table.Assertive.CurrentAverageAgeOfClient)
            {
                f0Score.CurrentAverageAgeOfClient = table.Assertive.ScoreRanking;
            }
            else
            {
                f0Score.CurrentAverageAgeOfClient = table.Aggressive.ScoreRanking;
            }
        }
    }
}