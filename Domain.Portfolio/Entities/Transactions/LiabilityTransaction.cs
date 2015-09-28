using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Domain.Portfolio.Entities.Transactions
{
    public abstract class LiabilityTransaction : TransactionBase
    {
        public LiabilityTransactionType LiabilityTransactionType { get; set; }

    }
}
