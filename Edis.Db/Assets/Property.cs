using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Edis.Db.IncomeRecords;
using Edis.Db.Transactions;


namespace Edis.Db.Assets
{
    public class Property
    {
        [Key]
        public string PropertyId { get; set; }
        public string GooglePlaceId { get; set; }

        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string PropertyType { get; set; }



        public virtual ICollection<AssetPrice> Prices { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactions { get; set; }
        public virtual ICollection<ResearchValue> ResearchValues { get; set; }
    }
}