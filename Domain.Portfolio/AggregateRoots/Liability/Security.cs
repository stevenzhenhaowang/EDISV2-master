
namespace Domain.Portfolio.AggregateRoots.Liability
{
    public class Security
    {
        public AssetBase Asset { get; set; }
        public double LoanValueRatio { get; set; }
    }
}
