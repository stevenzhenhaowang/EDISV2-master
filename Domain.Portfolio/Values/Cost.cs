using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values
{
    //cost= expense + asset. Expense may increase as Asset values decreases.
    public class Cost : ValueBase
    {
        public double Total { get; set; }

        /// <summary>
        ///     Asset Cost can be: brokerage, entry fee, etc.
        /// </summary>
        public double AssetCost { get; set; }

        public double Expense { get; set; }
    }
}