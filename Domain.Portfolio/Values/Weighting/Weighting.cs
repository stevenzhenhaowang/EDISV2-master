using Domain.Portfolio.Base;
using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.Values.Weighting
{
    public class Weighting : ValueBase
    {
        public double Percentage { get; set; }
        public IWeightable Weightable { get; set; }
    }
}