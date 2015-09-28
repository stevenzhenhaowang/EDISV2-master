using System;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Cost
{
    public class ConsultancyFeeRecordCreation
    {
        public string AccountNumber { get; internal set; }
        public string AdviserNumber { get; set; }
        public double Amount { get; set; }
        public DateTime IncurredOn { get; set; }
        public ConsultancyExpenseType ConsultancyExpenseType { get; set; }
        public string Notes { get; set; }
    }
}
