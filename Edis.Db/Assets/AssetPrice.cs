using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Edis.Db.Assets
{
    public class AssetPrice
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public AssetTypes AssetType { get; set; }
        [Required]
        public string CorrespondingAssetKey { get; set; }


    }
}
