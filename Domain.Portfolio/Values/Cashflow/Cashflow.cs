using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.Cashflow
{
    public class Cashflow : ValueBase
    {
        public string Month { get; set; }
        public double Income { get; set; }
        public double Expenses { get; set; }
    }
}