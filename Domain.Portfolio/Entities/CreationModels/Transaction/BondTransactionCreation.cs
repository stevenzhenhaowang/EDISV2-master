using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.Entities.CreationModels.Transaction
{
    public class BondTransactionCreation : TransactionCreationBase
    {

        public string Ticker { get; set; }
        public string BondName { get; set; }
        public Frequency Frequency { get; set; }
        public string BondType { get; set; }
        public string Issuer { get; set; }
        /// <summary>
        /// Negative value denotes sell
        /// </summary>
        public int NumberOfUnits { get; set; }
        public double UnitPrice { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<TransactionFeeRecordCreation> TransactionFeeRecords { get; set; }

    }
}
