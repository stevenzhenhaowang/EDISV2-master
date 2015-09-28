using Shared;

namespace Domain.Portfolio.Values
{
    public class BondDetails
    {
        public CreditRating BondRating { get; set; }
        public string RatingAgency { get; set; }
        public double Priority { get; set; }
        public double RedemptionFeatures { get; set; }
    }
}