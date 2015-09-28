using System;

namespace Domain.Portfolio.Entities.CreationModels
{
    public class ClientRegistration
    {
        public string clientId { get; set; }
        public DateTime? CreateOn { get; set; }
        public string ClientNumber { get; set; }
        public string ClientType { get; set; }
        public string GroupNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }

        //Person Client
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public DateTime? Dob { get; set; }

        //Entity Client
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string ABN { get; set; }
        public string ACN { get; set; }
    }
}