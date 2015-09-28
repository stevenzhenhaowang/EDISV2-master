using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Database.IncomeRecords;
using Database.Transactions;

namespace Database.Assets
{
    public class Property
    {
        [Key]
        public string PropertyId { get; set; }

        [Required]
        public string GooglePlaceId { get; set; }

        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string PropertyType { get; set; }



        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
        public virtual ICollection<PropertyTransaction> PropertyTransactions { get; set; }
    }
}