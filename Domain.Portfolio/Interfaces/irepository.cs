using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Entities.CreationModels;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Entities.CreationModels.Income;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using Domain.Portfolio.Values.Ratios;
using Shared;


namespace Domain.Portfolio.Interfaces
{
    public interface IRepository
    {
        Task<List<ActivityBase>> GetEquityActivitiesForAccount(string accountId, string ticker, DateTime to);
        List<ActivityBase> GetEquityActivitiesForAccountSync(string accountId, string ticker, DateTime to);       //added
        Task<List<ActivityBase>> GetPropertyActivitiesForAccount(string accountId, string placeId, DateTime to);
        List<ActivityBase> GetPropertyActivitiesForAccountSync(string accountId, string placeId, DateTime to);      //added
        Task<List<ActivityBase>> GetFixedIncomeActivitiesForAccount(string accountId, string ticker, DateTime toDate);
        List<ActivityBase> GetFixedIncomeActivitiesForAccountSync(string accountId, string ticker, DateTime toDate);            //added
        Task<List<ActivityBase>> GetCashActivitiesForAccount(string accountId, string cashAccountNumber, DateTime toDate);
        List<ActivityBase> GetCashActivitiesForAccountSync(string accountId, string cashAccountNumber, DateTime toDate);            //added
        Task<List<string>> GetAllSectors();
        List<string> GetAllSectorsSync();       //added
        Task<List<string>> GetAllBondTypes();
        List<string> GetAllBondTypesSync();       //added
        Task<List<string>> GetAllAustralianStates();
        List<string> GetAllAustralianStatesSync();    //added
        Task<List<string>> GetAllPropertyTypes();
        List<string> GetAllPropertyTypesSync();       //added
        Task<List<CashAccountType>> GetAllCashAccountTypes();
        List<CashAccountType> GetAllCashAccountTypesSync();   //added
        Task<TAggregateRoot> Get<TAggregateRoot>(string id, DateTime todate) where TAggregateRoot : AggregateRootBase, new();
        TAggregateRoot GetSync<TAggregateRoot>(string id, DateTime todate) where TAggregateRoot : AggregateRootBase, new();       //added
        Task<Client> GetClient(string clientNumber, DateTime todate);
        Client GetClientSync(string clientNumber, DateTime todate);   //added
        Task<ClientGroup> GetClientGroup(string clientGroupNumber, DateTime todate);
        ClientGroup GetClientGroupSync(string clientGroupNumber, DateTime todate);        //added
        Task<Adviser> GetAdviser(string adviserNumber, DateTime todate);
        Adviser GetAdviserSync(string adviserNumber, DateTime todate);              //added
        Task<ClientAccount> GetClientAccount(string accountNumber, DateTime todate);
        ClientAccount GetClientAccountSync(string accountNumber, DateTime todate); //added
        Task<GroupAccount> GetClientGroupAccount(string accountNumber, DateTime todate);
        GroupAccount GetClientGroupAccountSync(string accountNumber, DateTime todate);        //added
        Task<Adviser> CreateAdviser(Adviser newAdviser);
        Adviser CreateAdviserSync(Adviser newAdviser);        //added
        Task CreateNewClient(ClientRegistration client);
        void CreateNewClientSync(ClientRegistration client);            //added
        Task CreateNewClientGroup(ClientGroupRegistration clientGroup);
        void CreateNewClientGroupSync(ClientGroupRegistration clientGroup); //added
        Task RecordTransaction(TransactionCreationBase transaction);
        void RecordTransactionSync(TransactionCreationBase transaction);    //added
        Task<ClientAccount> CreateNewClientAccount(string clientNumber, string notes, AccountType accountType);
        ClientAccount CreateNewClientAccountSync(string clientNumber, string notes, AccountType accountType);       //added
        Task<GroupAccount> CreateNewClientGroupAccount(string clientGroupNumber, string notes, AccountType accountType);
        GroupAccount CreateNewClientGroupAccountSync(string clientGroupNumber, string notes, AccountType accountType);    //added
        Task RecordConsultancyFee(ConsultancyFeeRecordCreation fee);
        void RecordConsultancyFeeSync(ConsultancyFeeRecordCreation fee);        //added
        Task RecordIncome(IncomeCreationBase income);
        void RecordIncomeSync(IncomeCreationBase income);       //added
        Task<List<ClientGroup>> GetAllClientGroupsForAdviser(string adviserNumber, DateTime todate);
        List<ClientGroup> GetAllClientGroupsForAdviserSync(string adviserNumber, DateTime todate);            // added


        Task<List<ClientAccount>> GetAccountsForClient(string clientNumber, DateTime toDate, AccountType accountType);
        List<ClientAccount> GetAccountsForClientSync(string clientNumber, DateTime toDate, AccountType accountType);        //added
        Task<List<GroupAccount>> GetAccountsForClientGroup(string clientGroupNumber, DateTime toDate, AccountType accountType);
        List<GroupAccount> GetAccountsForClientGroupSync(string clientGroupNumber, DateTime toDate, AccountType accountType);     //added


        Task<List<Client>>  GetClientsForGroup(string clientGroupNumber, DateTime toDate);
        List<Client> GetClientsForGroupSync(string clientGroupNumber, DateTime toDate);     //added
        Task<List<AssetBase>> GetAssetsForAccount(string accountNumber, DateTime dateTime);
        List<AssetBase> GetAssetsForAccountSync(string accountNumber, DateTime dateTime);       //added
        Task FeedResearchValueForBond(string key, double value, string ticker, string issuer);
        Task<double?> GetResearchValueForBond(string key, string ticker);
        Task FeedResearchValueForEquity(string key, double value, string propertyId, string issuer);
        Task<double?> GetResearchValueForEquity(string key, string ticker) ;
        Task FeedResearchValueForProperty(string key, double value, string ticker, string issuer);
        Task<double?> GetResearchValueForProperty(string key, string propertyId);
        Task<string> GetLatestIssuerForEquityResearchValue(string key, string ticker);
        Task<string> GetLatestIssuerForBondResearchValue(string key, string ticker);
        Task<Ratios> GetAsx200AverageRatios(EquityTypes type);
        Task<List<LiabilityBase>>  GetLiabilitiesForAccount(string accountNumber, DateTime dateTime);



        void FeedResearchValueForBondSync(string key, double value, string ticker, string issuer);           //added
        double? GetResearchValueForBondSync(string key, string ticker);           //added
        void FeedResearchValueForEquitySync(string key, double value, string propertyId, string issuer);           //added
        double? GetResearchValueForEquitySync(string key, string ticker);           //added
        void FeedResearchValueForPropertySync(string key, double value, string ticker, string issuer);           //added
        double? GetResearchValueForPropertySync(string key, string propertyId);           //added
        string GetLatestIssuerForEquityResearchValueSync(string key, string ticker);           //added
        string GetLatestIssuerForBondResearchValueSync(string key, string ticker);           //added
        Ratios GetAsx200AverageRatiosSync(EquityTypes type);           //added
        List<LiabilityBase> GetLiabilitiesForAccountSync(string accountNumber, DateTime dateTime);           //added



        Task<List<ActivityBase>> GetInsuranceActivities(string insuranceId, DateTime beforeDate);
        Task<List<ActivityBase>> GetMarginLendingActivities(string marginLendingId, DateTime beforeDate);
        Task<List<ActivityBase>> GetMortgageLoanActivities(string mortgageId, DateTime beforeDate);


        List<ActivityBase> GetInsuranceActivitiesSync(string insuranceId, DateTime beforeDate);       //added
        List<ActivityBase> GetMarginLendingActivitiesSync(string marginLendingId, DateTime beforeDate);       //added
        List<ActivityBase> GetMortgageLoanActivitiesSync(string mortgageId, DateTime beforeDate);       //added

        Task RecordRepayment(RepaymentCreation record);

        void RecordRepaymentSync(RepaymentCreation record); //added

        Task<Adviser> UpdateAdviser(Adviser adviser, DateTime dateTime);
    }
}