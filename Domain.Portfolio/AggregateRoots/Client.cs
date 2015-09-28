using System;
using System.Collections.Generic;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.Base;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.AggregateRoots
{
    public class Client : AggregateRootBase
    {
        public Client(IRepository repo) : base(repo)
        {
        }
        public DateTime? CreatedOn { get; set; }
        public string ClientNumber { get; set; }
        public string ClientGroupId { get; set; }
        public string ClientType { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual string ClientGroupNumber { get; set; }

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





        public ClientAccount AddAccount(string notes, AccountType accountType)
        {
            return this._repository.CreateNewClientAccount(this.ClientNumber, notes, accountType).Result;
            //todo refresh current object
        }
        public List<ClientAccount> GetAccounts(DateTime? beforeDate=null, AccountType accountType=AccountType.GenenralPurpose)
        {
            return this._repository.GetAccountsForClient(this.ClientNumber, beforeDate??DateTime.Now, accountType).Result;
        }

        public List<ClientAccount> GetAccountsSync(DateTime? beforeDate = null, AccountType accountType = AccountType.GenenralPurpose)
        {
            return this._repository.GetAccountsForClientSync(this.ClientNumber, beforeDate ?? DateTime.Now, accountType);
        } 
    }
}