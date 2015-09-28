using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.AggregateRoots.Accounts
{
    public class ClientAccount : AccountBase
    {
        public ClientAccount(IRepository repo) : base(repo)
        {
        }
    }
}