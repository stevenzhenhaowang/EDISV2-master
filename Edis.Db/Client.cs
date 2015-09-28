using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Edis.Db
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        public string ClientGroupId { get; set; }
        [Required]
        public string ClientType { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [ForeignKey("ClientGroupId")]
        public virtual ClientGroup ClientGroup { get; set; }

        //Person
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }


        //Entity
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public string ABN { get; set; }
        public string ACN { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}