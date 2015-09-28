using System;
using System.Collections.Generic;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class PropertyTransactionCreation : TransactionCreationBase
    {

        public string FullAddress { get; set; }
        public string PropertyType { get; set; }
        public double Price { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsBuy { get; set; }
        public List<TransactionFeeRecordCreation> FeesRecords { get; set; }
    }
}
