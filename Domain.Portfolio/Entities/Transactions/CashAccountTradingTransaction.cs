namespace Domain.Portfolio.Entities.Transactions
{
    public class CashAccountTradingTransaction : TransactionBase
    {
        public string CashAccountNumber { get; set; }
        public string Bsb { get; set; }
        public string CashAccountName { get; set; }
    }
}