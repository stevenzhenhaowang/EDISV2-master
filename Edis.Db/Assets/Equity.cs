using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.IncomeRecords;
using Edis.Db.Liabilities;
using Edis.Db.Transactions;
using Shared;

namespace Edis.Db.Assets
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






        public virtual ICollection<AssetPrice> Prices { get; set; }
        public EquityTypes EquityType { get; set; }
        public virtual ICollection<Dividend> Dividends { get; set; }
        public virtual ICollection<EquityTransaction> EquityTransactions { get; set; }
        public virtual ICollection<ResearchValue> ResearchValues { get; set; }
    }
}