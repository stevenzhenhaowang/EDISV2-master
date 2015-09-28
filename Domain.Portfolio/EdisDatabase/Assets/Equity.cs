using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.IncomeRecords;
using Database.Transactions;
using Shared;

namespace Database.Assets
{
    public class Equity
    {
        [Key]
        public string AssetId { get; set; }

        [Required]
        public string Ticker { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Sector { get; set; }


        public EquityTypes EquityType { get; set; }
        public virtual ICollection<Dividend> Dividends { get; set; }
        public virtual ICollection<EquityTransaction> EquityTransactions { get; set; }
    }
}