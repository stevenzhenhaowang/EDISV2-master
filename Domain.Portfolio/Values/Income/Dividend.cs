namespace Domain.Portfolio.Values.Income
{
    /// <summary>
    ///     For Dividend, total income = received amount + franking amount
    /// </summary>
    public class Dividend : Income
    {
        public string Ticker { get; set; }
        public double ReceivedAmount { get; set; }
        public double FrankingAmount { get; set; }
        public double Yield { get; set; }
    }
}