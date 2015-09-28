using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class ResearchValue
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }

        public string Issuer { get; set; }
    }
}
