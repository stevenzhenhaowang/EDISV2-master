using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.Base;
using Domain.Portfolio.Interfaces;
using Shared;

namespace Domain.Portfolio.AggregateRoots
{
    public class ClientGroup : AggregateRootBase
    {
        public ClientGroup(IRepository repo) : base(repo)
        {
        }

        public DateTime? CreatedOn { get; set; }
        public string MainClientId { get; set; }
        public string GroupName { get; set; }
        public string GroupAlias { get; set; }
        public string ClientGroupNumber { get; set; }


        public List<GroupAccount> GetAccounts(DateTime? beforeDate=null, AccountType accountType = AccountType.GenenralPurpose)
        {
            return this._repository.GetAccountsForClientGroup(this.ClientGroupNumber, beforeDate??DateTime.Now, accountType).Result;
        }
        public List<GroupAccount> GetAccountsSync(DateTime? beforeDate = null, AccountType accountType = AccountType.GenenralPurpose)       //added
        {
            return this._repository.GetAccountsForClientGroupSync(this.ClientGroupNumber, beforeDate ?? DateTime.Now, accountType);
        } 


        public async Task<GroupAccount> AddAccount(string notes, AccountType accountType)
        {
            return await this._repository
                .CreateNewClientGroupAccount(this.ClientGroupNumber,notes, accountType);
        }
        public async Task<List<Client>> GetClients(DateTime? beforeDate=null)
        {
            return await this._repository.GetClientsForGroup(this.ClientGroupNumber, beforeDate??DateTime.Now);
        }
        public List<Client> GetClientsSync(DateTime? beforeDate = null)         //added
        {
            return this._repository.GetClientsForGroupSync(this.ClientGroupNumber, beforeDate ?? DateTime.Now);
        } 
    }
}