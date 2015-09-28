using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Edis.Db.IncomeRecords;
using Edis.Db.Liabilities;
using Edis.Db.Markers;
using Edis.Db.Transactions;

using Shared;

namespace Edis.Db.Assets
{
    public class Bond : IAsset
    {
        [Key]
        public string BondId { get; set; }
        [Required]
        public string Ticker { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Frequency Frequency { get; set; }
        [Required]
        public string BondType { get; set; }
        [Required]
        public string Issuer { get; set; }
        public virtual ICollection<AssetPrice> Prices { get; set; }
        public virtual ICollection<CouponPayment> CouponPayments { get; set; }
        public virtual ICollection<BondTransaction> BondTransactions { get; set; }
        public virtual ICollection<ResearchValue> ResearchValues { get; set; }









    }
}