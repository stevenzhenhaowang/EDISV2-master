using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class Client
    {
        [Key]
        public string ClientId { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? Dob { get; set; }
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        [ForeignKey("ClientGroupId")]
        public virtual ClientGroup ClientGroup { get; set; }
        [Required]
        public string ClientGroupId { get; set; }


        public virtual ICollection<Account> Accounts { get; set; }
    }
}