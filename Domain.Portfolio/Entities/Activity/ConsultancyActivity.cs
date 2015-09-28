using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Entities.Activity
{
    /// <summary>
    /// Consultancy activity will NOT trigger any transactions, hence non-financial.
    /// Consultancy will always incur cost and/or incomes(refund?)
    /// Cost/Incomes incurred during consultancy activities will not be calculated 
    /// for cost/income statistics of different asset types as they are not part of transactions
    /// </summary>
    public class ConsultancyActivity : ActivityBase
    {
    }
}
