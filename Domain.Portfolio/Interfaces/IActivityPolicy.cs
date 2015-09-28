using Domain.Portfolio.Entities.Activity;

namespace Domain.Portfolio.Interfaces
{
    public interface IActivityPolicy
    {
        bool ActivityIsAllowed(ActivityBase activity);
    }
}