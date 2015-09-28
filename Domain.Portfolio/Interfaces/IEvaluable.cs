using Domain.Portfolio.Values;

namespace Domain.Portfolio.Interfaces
{
    public interface IEvaluable
    {
        AssetSuitability GetRating();
    }
}