using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db
{
    public class IndexedEquity
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Ticker { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double? Weighting { get; set; }
        [Required]
        public ASXIndexTypes AsxIndexTypes { get; set; }
        [Required]
        public EquityTypes EquityType { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }


    }
}
