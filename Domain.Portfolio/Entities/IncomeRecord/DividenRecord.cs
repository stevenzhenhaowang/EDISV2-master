namespace Domain.Portfolio.Entities.IncomeRecord
{
    public class DividenRecord : IncomeRecordBase
    {
        public string Ticker { get; set; }
        public double Franking { get; set; }
    }
}