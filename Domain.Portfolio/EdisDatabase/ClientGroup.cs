using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class ClientGroup
    {
        [Key]
        public string ClientGroupId { get; set; }

        [Required]
        public DateTime? CreatedOn { get; set; }
        [Required]
        public string MainClientId { get; set; }

        [Required]
        [ForeignKey("MainClientId")]
        public virtual Client MainClient { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
        [Required]
        public string GroupNumber { get; set; }



        public virtual ICollection<Account> GroupAccounts { get; set; }
        public virtual Adviser Adviser { get; set; }
    }
}