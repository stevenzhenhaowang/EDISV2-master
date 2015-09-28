using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db.Enums
{
    public enum TransactionType
    {
        BondTransaction = 1,
        CashTransaction =2,
        EquityTransaction = 3,
        PropertyTransaction = 4
    }
}
