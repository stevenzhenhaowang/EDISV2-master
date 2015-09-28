using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db.Liabilities
{
    public class LoanValueRatio
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public double Ratio { get; set; }
        [Required]
        public string AssetId { get; set; }
        [Required]
        public AssetTypes AssetTypes { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public DateTime? ActiveDate { get; set; }


    }
}
