using Domain.Portfolio.Base;

namespace Domain.Portfolio.Values.Income
{
    /// <summary>
    ///     Income value as per holding/asset, hence ID-less.
    /// </summary>
    public abstract class Income : ValueBase
    {
        public double TotalAmount { get; set; }
    }
}