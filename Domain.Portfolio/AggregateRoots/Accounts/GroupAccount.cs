using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.AggregateRoots.Accounts
{
    public class GroupAccount : AccountBase
    {
        public GroupAccount(IRepository repo) : base(repo)
        {
        }
    }
}