
using System.Collections.Generic;
namespace Domain.Portfolio.Entities.Transactions
{
    public class MarginlendingTransaction : LiabilityTransaction
    {
        public Dictionary<string,double> AssetLvRs { get; set; }

    }
}
