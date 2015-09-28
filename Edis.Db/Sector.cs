using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Db
{
    public class Sector
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string SectorName { get; set; }

        public string SectorGroup { get; set; }
    }
}
