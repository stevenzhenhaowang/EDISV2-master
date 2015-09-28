
namespace Domain.Portfolio.Entities.Transactions
{
    public class MortgageTransaction : LiabilityTransaction
    {
        public string FullAddress { get; set; }
        public string PlaceId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
