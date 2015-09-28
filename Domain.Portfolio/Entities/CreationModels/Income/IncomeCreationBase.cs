using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Entities.CreationModels.Income
{
    public abstract class IncomeCreationBase
    {
        public string AccountNumber { get; internal set; }
    }
}
