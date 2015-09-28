using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.AggregateRoots.Liability;
using Domain.Portfolio.Entities.CreationModels;
using Domain.Portfolio.Entities.CreationModels.Cost;
using Domain.Portfolio.Entities.CreationModels.Income;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using Domain.Portfolio.Entities.IncomeRecord;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values.Ratios;
using Edis.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared;
using SqlRepository;
using SqlRepository.Extensions;
using Adviser = Domain.Portfolio.AggregateRoots.Adviser;
using PropertyType = Edis.Db.PropertyType;
using RepaymentCreation = Domain.Portfolio.Entities.CreationModels.RepaymentCreation;

namespace Domain.Portfolio.Test
{
    [TestClass]
    public class BasicClientCrud
    {
        [TestInitialize()]
        public void Initialize()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EdisContext>());
            using (EdisContext db = new EdisContext())
            {
                db.Sectors.Add(new Sector()
                {
                    Id = Guid.NewGuid().ToString(),
                    SectorName = "Sector 1",
                });
                db.Sectors.Add(new Sector()
                {
                    Id = Guid.NewGuid().ToString(),
                    SectorName = "Sector 2"
                });

                db.BondTypes.Add(new BondType()
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeName = "Bond Type 1"
                });
                db.BondTypes.Add(new BondType()
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeName = "Bond Type 2"
                });
                db.AustralianStates.Add(new AustralianState()
                {
                    Id = Guid.NewGuid().ToString(),
                    State = "Victoria"
                });
                db.AustralianStates.Add(new AustralianState()
                {
                    Id = Guid.NewGuid().ToString(),
                    State = "Queensland"
                });
                db.PropertyTypes.Add(new PropertyType()
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeName = "Home"
                });

                db.PropertyTypes.Add(new PropertyType()
                {
                    Id = Guid.NewGuid().ToString(),
                    TypeName = "Office"
                });



                db.SaveChanges();
            }
        }

        [TestMethod]
        public void CanUpdateAdviser()
        {

            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Steven", "Wang").Result;

            adviser.FirstName = "Zhenhao";

            var adviser2 = repo.UpdateAdviser(adviser, DateTime.Now).Result;

            Console.WriteLine(adviser2.FirstName);

            Assert.IsTrue(adviser.FirstName == "Zhenhao");
            repo.Dispose();
        }

        [TestMethod]
        public void CanCreateAdviser()
        {

            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            Assert.IsTrue(adviser.FirstName == "Test");
            repo.Dispose();
        }
        [TestMethod]
        public void AdviserCanCreateClientGroup()
        {

            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            Assert.IsTrue(groups.Count == 1);
            Assert.IsTrue(groups[0].GetClients().Result.Count == 1);
            repo.Dispose();
        }
        [TestMethod]
        public void AdviserCanCreateClientForGroup()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            //adviser.SetProfileEndDate(DateTime.Now);
            var groups = adviser.GetAllClientGroups().Result;
            adviser.CreateNewClient(new ClientRegistration()
            {
                GroupNumber = groups[0].ClientGroupNumber,
                FirstName = "second first name",
                LastName = "second last name",
                Address = "address",
                Dob = DateTime.Now,
                Phone = "039999999",
                Email = "Email@mail.com"
            }).Wait();
            //adviser.SetProfileEndDate(DateTime.Now);
            groups = adviser.GetAllClientGroups().Result;
            Assert.IsTrue(groups.Count == 1);
            Assert.IsTrue(groups[0].GetClients().Result.Count == 2);
            repo.Dispose();

        }
        [TestMethod]
        public void AdviserCanCreateGroupAccount()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("Testing", AccountType.GenenralPurpose).Wait();
            Assert.IsTrue(groups[0].GetAccounts().Count == 1);
            repo.Dispose();
        }
        [TestMethod]
        public void AdviserCanCreateClientAccount()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            var clients = groups[0].GetClients().Result;
            clients[0].AddAccount("Testing", AccountType.GenenralPurpose);
            Assert.IsTrue(clients[0].GetAccounts().Count == 1);
            repo.Dispose();
        }
        [TestMethod]
        public void AdviserCanCreateClientWithProperAgeCalculation()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now.AddYears(-12),
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            Assert.IsTrue(groups[0].GetClients().Result[0].Age == 12);
            repo.Dispose();
        }
        [TestMethod]
        public void AccountCanMakeStockTransaction()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();
            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            Assert.IsTrue(account.GetAssets().Count == 1);
            var asset = account.GetAssets()[0];
            Assert.IsTrue(asset.LatestPrice == 20);
            Assert.IsTrue(asset.TotalNumberOfUnits == 100);
            var assets = account.GetAssets();
            var marketvalue = assets.GetTotalMarketValue();
            var cost = assets.GetTotalCost();
            Assert.IsTrue(marketvalue == 2000);
            Assert.IsTrue(cost.AssetCost == 2000);
            Assert.IsTrue(cost.Total == 2000);
            Assert.IsTrue(cost.Expense == 0);
            repo.Dispose();
        }
        [TestMethod]
        public void AccountCanMakeTransactionWithExpenses()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            Assert.IsTrue(account.GetAssets().Count == 1);
            var asset = account.GetAssets()[0];
            Assert.IsTrue(asset.LatestPrice == 20);
            Assert.IsTrue(asset.TotalNumberOfUnits == 100);
            var assets = account.GetAssets();
            var marketvalue = assets.GetTotalMarketValue();
            var cost = assets.GetTotalCost();
            Assert.IsTrue(marketvalue == 2000);
            Assert.IsTrue(cost.AssetCost == 2000);
            Assert.IsTrue(cost.Total == 2100);
            Assert.IsTrue(cost.Expense == 100);
            repo.Dispose();
        }
        [TestMethod]
        public void AccountCanRecordIncomeForAsset()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            Assert.IsTrue(account.GetAssets().Count == 1);
            var asset = account.GetAssets()[0];
            Assert.IsTrue(asset.LatestPrice == 20);
            Assert.IsTrue(asset.TotalNumberOfUnits == 100);
            account.RecordIncome(new DividendPaymentCreation()
            {
                Amount = 200,
                Franking = 100,
                PaymentOn = DateTime.Now,
                Ticker = ((AustralianEquity)asset).Ticker
            }).Wait();

            var assets = account.GetAssets();
            var marketvalue = assets.GetTotalMarketValue();
            var cost = assets.GetTotalCost();
            var income = assets.GetTotalIncome();




            Assert.IsTrue(marketvalue == 2000);
            Assert.IsTrue(cost.AssetCost == 2000);
            Assert.IsTrue(cost.Total == 2100);
            Assert.IsTrue(cost.Expense == 100);
            Assert.IsTrue(income == 300);
            repo.Dispose();

        }
        [TestMethod]
        public void AccountCanCalculateProfitAndLoss()
        {

            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            Assert.IsTrue(account.GetAssets().Count == 1);
            var asset = account.GetAssets()[0];
            Assert.IsTrue(asset.LatestPrice == 20);
            Assert.IsTrue(asset.TotalNumberOfUnits == 100);
            account.RecordIncome(new DividendPaymentCreation()
            {
                Amount = 200,
                Franking = 100,
                PaymentOn = DateTime.Now,
                Ticker = ((AustralianEquity)asset).Ticker
            }).Wait();

            var assets = account.GetAssets();
            var marketvalue = assets.GetTotalMarketValue();
            var cost = assets.GetTotalCost();
            var income = assets.GetTotalIncome();
            var profitLoss = assets.GetProfitAndLoss();



            Assert.IsTrue(marketvalue == 2000);
            Assert.IsTrue(cost.AssetCost == 2000);
            Assert.IsTrue(cost.Total == 2100);
            Assert.IsTrue(cost.Expense == 100);
            Assert.IsTrue(income == 300);
            Assert.IsTrue(profitLoss == 200);
            repo.Dispose();

        }
        [TestMethod]
        public void CanCalculateAssetSectorialDiversification()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Sector 1",
                Ticker = Guid.NewGuid().ToString(),
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            Assert.IsTrue(account.GetAssets().Count == 1);
            var asset = account.GetAssets()[0];
            Assert.IsTrue(asset.LatestPrice == 20);
            Assert.IsTrue(asset.TotalNumberOfUnits == 100);
            account.RecordIncome(new DividendPaymentCreation()
            {
                Amount = 200,
                Franking = 100,
                PaymentOn = DateTime.Now,
                Ticker = ((AustralianEquity)asset).Ticker
            }).Wait();

            var assets = account.GetAssets();
            var diversification = assets.GetAssetSectorialDiversification<AustralianEquity>(repo).Result;
           
            Assert.IsTrue(diversification.Count == 2);
            Assert.IsTrue(diversification["Sector 1"] == 2000);
            Assert.IsTrue(diversification["Sector 2"] == 0);
            repo.Dispose();
        }
        [TestMethod]
        public void CanCalculateAssetBondTypeDiversification()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new BondTransactionCreation()
            {
                BondName = "Test Bond",
                BondType = "Bond Type 1",
                Frequency = Frequency.Annually,
                Issuer = "Issuer",
                NumberOfUnits = 1000,
                Ticker = "Ticker 1",
                TransactionDate = DateTime.Now,
                UnitPrice = 27.99,
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 100,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                }
            }).Wait();

            var assets = account.GetAssets();

            var diversification = assets.GetFixedIncomeTypeDiversification(repo).Result;
            Assert.IsTrue(diversification.Count == 2);
            Assert.IsTrue(diversification["Bond Type 1"] == (27.99 * 1000));


        }
        [TestMethod]
        public void CanCalculateAssetPropertyStateDiversification()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new PropertyTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 888,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                FullAddress = "668 Bourke Street, Melbourne, 3000",
                IsBuy = true,
                Price = 1980000,
                PropertyType = "Home",
                TransactionDate = DateTime.Now
            }).Wait();

            var assets = account.GetAssets();
            var stateDiversification = assets.GetDirectPropertyStateDiversification(repo).Result;
            Assert.IsTrue(stateDiversification.Count == 2);
            Assert.IsTrue(stateDiversification["Victoria"] == 1980000);



        }
        [TestMethod]
        public void CanCalculateAssetPropertyTypeDiversification()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new PropertyTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 888,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                FullAddress = "668 Bourke Street, Melbourne, 3000",
                IsBuy = true,
                Price = 1980000,
                PropertyType = "Home",
                TransactionDate = DateTime.Now
            }).Wait();

            var assets = account.GetAssets();
            var propertyTypeDiversification = assets.GetDirectPropertyTypeDiversification(repo).Result;
            Assert.IsTrue(propertyTypeDiversification.Count == 2);
            Assert.IsTrue(propertyTypeDiversification["Home"] == 1980000);

        }
   

        [TestMethod]
        public void CanCalculateAssetCashAccountTypeDiversification()
        {
            var repo = new EdisRepository();

            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new CashAccountTransactionAccountCreation()
            {
                Amount = 150000,
                AnnualInterestSoFar = 0,
                Bsb = "123456",
                CashAccountName = "Account name",
                CashAccountNumber = "Number",
                CashAccountType = CashAccountType.TermDeposit,
                CurrencyType = CurrencyType.AustralianDollar,
                Frequency = Frequency.Annually,
                InterestRate = 0.9,
                MaturityDate = DateTime.Now.AddYears(1),
                TermsInMonths = 20,
                TransactionDate = DateTime.Now,
                TransactionFeeRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 1000,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                }
            }).Wait();

            var assets = account.GetAssets();

            var cashAccountDiversification = assets.GetCashAccountTypeDiversification(repo).Result;



            Assert.IsTrue(cashAccountDiversification.Count == 5);
            Assert.IsTrue(cashAccountDiversification["TermDeposit"] == 150000);

            repo.Dispose();
        }
        [TestMethod]
        public void CanRecordResearchValuesAndGetRatingForAsset()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 1000,
                Price = 29.64,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();


            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>(),
                Name = "Test Stock",
                NumberOfUnits = 1000,
                Price = 29.64,
                Sector = "Test Sector",
                Ticker = "Test Ticker 02",
                TransactionDate = DateTime.Now,
            }).Wait();

            #region set up research values (this portion is optional, default values will be used if no research values available. Absent of research values will always generate 'Danger' rating)
            //record Australian equity f0 values
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.Beta), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.EarningsStability), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearInformation), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearReturn), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.Frank), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.InterestCover), 0, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.OneYearReturn), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.PayoutRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.ThreeYearReturn), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.BetaFiveYears), 1.1036, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.Capitalisation), 57225467107, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.CurrentRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.DebtEquityRatio), 0, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.DividendYield), 0.0621, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.EpsGrowth), 0.0772, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearAlphaRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSharpRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearSkewnessRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearStandardDeviation), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FiveYearTrackingErrorRatio), 1.1036, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.FundSize), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.GlobalCategory), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.CurrentRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.PriceEarningRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.QuickRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnAsset), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Ratios>.Property(r => r.ReturnOnEquity), 1, "Test Ticker 01", "Test Issuer").Wait();

            //record Australian Equity f1 research values
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FinancialLeverage), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.Frank), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.InterestCover), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.IntrinsicValue), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearReturn), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearRevenueGrowth), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.PriceEarningRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.ReturnOnAsset), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.ReturnOnEquity), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.CreditRating), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.DebtEquityRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.DividendYield), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.EpsGrowth), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FairValueVariation), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.FiveYearTotalReturn), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MaxManagementExpenseRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MorningStarAnalyst), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.MorningstarRecommendation), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearAlpha), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearInformationRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearBeta), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearSharpRatio), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.OneYearTrackingError), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.PerformanceFee), 1, "Test Ticker 01", "Test Issuer").Wait();
            repo.FeedResearchValueForEquity(Nameof<Recommendation>.Property(r => r.YearsSinceInception), 1, "Test Ticker 01", "Test Issuer").Wait();
            #endregion
            var assets = account.GetAssets();
            var equityWithResearchValues = assets.OfType<Equity>().First(a => a.Ticker == "Test Ticker 01");
            var equityWithNoResearchValues = assets.OfType<Equity>().First(a => a.Ticker == "Test Ticker 02");
            var rating_withResearchValues = equityWithResearchValues.GetRating();
            var rating_withNoResearchValues = equityWithNoResearchValues.GetRating();
            Assert.IsTrue(rating_withNoResearchValues.SuitabilityRating == SuitabilityRating.Danger);
            Assert.IsTrue(rating_withResearchValues != null);
        }
        [TestMethod]
        public void CanGenerateMonthlyCashflowForAssets()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();
            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new EquityTransactionCreation()
            {
                EquityType = EquityTypes.AustralianEquity,
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 1000,
                        TransactionExpenseType = TransactionExpenseType.Brokerage
                    }
                },
                Name = "Test Stock",
                NumberOfUnits = 100,
                Price = 20,
                Sector = "Test Sector",
                Ticker = "Test Ticker 01",
                TransactionDate = DateTime.Now,
            }).Wait();

            account = groups[0].GetAccounts()[0];
            var assets = account.GetAssets();
            var cashflow = assets.GetMonthlyCashflows();


            Assert.IsTrue(cashflow != null);
            Assert.IsTrue(cashflow.Count == 12);
            Assert.IsTrue(cashflow.Last().Expenses == 1000);

        }

        [TestMethod]
        public void CanCreateInsuranceTransaction()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();
            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new InsuranceTransactionCreation()
            {
                ExpiryDate = DateTime.Now.AddYears(1),
                AmountInsured = 10000,
                EntitiesInsured = "something",
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.AssetInsurance,
                IsAcquire = true,
                Issuer = "Issuer",
                NameOfPolicy = "Name",
                PolicyAddress = "Address",
                PolicyNumber = "Number",
                PolicyType = PolicyType.Accident,
                Premium = 100
            }).Wait();

            var liabilities = account.GetLiabilities();
            Assert.IsTrue(liabilities.Count == 1);
        }

        [TestMethod]
        public void CanCreateInsuranceAndRecordPayment()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();
            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new InsuranceTransactionCreation()
            {
                ExpiryDate = DateTime.Now.AddYears(1),
                AmountInsured = 1000,
                EntitiesInsured = "something",
                GrantedOn = DateTime.Now,
                InsuranceType = InsuranceType.AssetInsurance,
                IsAcquire = true,
                Issuer = "Issuer",
                NameOfPolicy = "Name",
                PolicyAddress = "Address",
                PolicyNumber = "Number",
                PolicyType = PolicyType.Accident,
                Premium = 100
            }).Wait();


            account.RecordRepayment(new RepaymentCreation()
            {
                PaymentOn = DateTime.Now,
                PrincipleAmount = 300,
                InterestAmount = 200,
                PolicyName = "Name",
                PolicyNumber = "Number",
                PropertyId = null,
                LiabilityTypes = LiabilityTypes.Insurance
            }).Wait();

            var liabilities = account.GetLiabilities();
            Assert.IsTrue(liabilities[0].CurrentBalance == 700);

        }

        [TestMethod]
        public void CanCreateHomeLoan()
        {
            var repo = new EdisRepository();
            var adviser = repo.CreateAdviser("Test", "Adviser").Result;
            adviser.CreateNewClientGroup(new ClientGroupRegistration()
            {
                AdviserNumber = adviser.AdviserNumber,
                FirstName = "client first name",
                LastName = "client last name",
                Address = "668 Bourke Street, Melbourne, 3000, Victoria",
                Dob = DateTime.Now,
                Phone = "03699999",
                Email = "Email"
            }).Wait();

            var groups = adviser.GetAllClientGroups().Result;
            groups[0].AddAccount("group account", AccountType.GenenralPurpose).Wait();
            var account = groups[0].GetAccounts()[0];
            account.MakeTransaction(new PropertyTransactionCreation()
            {
                FeesRecords = new List<TransactionFeeRecordCreation>()
                {
                    new TransactionFeeRecordCreation()
                    {
                        Amount = 888,
                        TransactionExpenseType = TransactionExpenseType.AdviserTransactionFee
                    }
                },
                FullAddress = "668 Bourke Street, Melbourne, 3000",
                IsBuy = true,
                Price = 1980000,
                PropertyType = "Home",
                TransactionDate = DateTime.Now
            }).Wait();
            var assets = account.GetAssets();
            account.MakeTransaction(new HomeLoanTransactionCreation()
            {
                ExpiryDate = DateTime.Now.AddYears(2),
                GrantedOn = DateTime.Now,
                Institution = "ANZ",
                IsAcquire = true,
                LoanAmount = 300000,
                LoanRate = 0.07,
                LoanRepaymentType = LoanRepaymentType.DirectDebt,
                PropertyId = assets[0].Id,
                TypeOfMortgageRates = TypeOfMortgageRates.Combination
            }).Wait();
            var liabilities = account.GetLiabilities();

            Assert.IsTrue(liabilities.Count==1);
            Assert.IsTrue(liabilities[0] is MortgageAndHomeLiability);

        }




    }
}

