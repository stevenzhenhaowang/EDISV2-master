using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Portfolio.Entities.CreationModels
{
    public class ClientGroupRegistration
    {
        public ClientRegistration client { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        //public string Phone { get; set; }
        //public string Email { get; set; }
        //public DateTime? Dob { get; set; }
        //public string MainClientID { get; set; }
        public DateTime? CreateOn { get; set; }
        public string GroupName { get; set; }
        public string GroupAlias { get; set; }
        public string AdviserNumber { get; set; }
    }
}
