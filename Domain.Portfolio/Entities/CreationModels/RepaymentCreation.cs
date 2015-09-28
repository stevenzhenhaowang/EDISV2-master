using System;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels
{
    public class RepaymentCreation
    {

        public string AccountNumber { get; internal set; }
        public double InterestAmount { get; set; }
        public double PrincipleAmount { get; set; }
        public DateTime PaymentOn { get; set; }
        public LiabilityTypes LiabilityTypes { get; set; }
        /// <summary>
        /// Used for homeloan repayment
        /// </summary>
        public string PropertyId { get; set; }

        /// <summary>
        /// used for insurance repayment
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// Used for insurance repayment
        /// </summary>
        public string PolicyName { get; set; }

    }
}
