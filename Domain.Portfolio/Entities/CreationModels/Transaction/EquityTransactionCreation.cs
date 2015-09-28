using System;
using System.Collections.Generic;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class EquityTransactionCreation : TransactionCreationBase
    {

        public string Ticker{ get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public EquityTypes EquityType{ get; set; }
        /// <summary>
        /// Negative value denotes sell
        /// </summary>
        public int NumberOfUnits { get; set; }
        public double Price { get; set; }
        public List<TransactionFeeRecordCreation> FeesRecords { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
