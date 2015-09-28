namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.String(nullable: false, maxLength: 128),
                        AccountNumber = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        AccountType = c.Int(nullable: false),
                        AccountInfo = c.String(),
                        ClientGroup_ClientGroupId = c.String(maxLength: 128),
                        Client_ClientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.ClientGroups", t => t.ClientGroup_ClientGroupId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .Index(t => t.ClientGroup_ClientGroupId)
                .Index(t => t.Client_ClientId);
            
            CreateTable(
                "dbo.BondTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        NumberOfUnits = c.Int(nullable: false),
                        UnitPriceAtPurchase = c.Double(nullable: false),
                        BondId = c.String(nullable: false, maxLength: 128),
                        TransactionDate = c.DateTime(nullable: false),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.BondId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.BondId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Bonds",
                c => new
                    {
                        BondId = c.String(nullable: false, maxLength: 128),
                        Ticker = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Frequency = c.Int(nullable: false),
                        BondType = c.String(nullable: false),
                        Issuer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BondId);
            
            CreateTable(
                "dbo.CouponPayments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Bond_BondId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_BondId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.Bond_BondId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.AssetPrices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Price = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        AssetType = c.Int(nullable: false),
                        CorrespondingAssetKey = c.String(nullable: false),
                        Bond_BondId = c.String(maxLength: 128),
                        Property_PropertyId = c.String(maxLength: 128),
                        Equity_AssetId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_BondId)
                .ForeignKey("dbo.Properties", t => t.Property_PropertyId)
                .ForeignKey("dbo.Equities", t => t.Equity_AssetId)
                .Index(t => t.Bond_BondId)
                .Index(t => t.Property_PropertyId)
                .Index(t => t.Equity_AssetId);
            
            CreateTable(
                "dbo.ResearchValues",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Key = c.String(nullable: false),
                        Value = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Issuer = c.String(),
                        Bond_BondId = c.String(maxLength: 128),
                        Property_PropertyId = c.String(maxLength: 128),
                        Equity_AssetId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_BondId)
                .ForeignKey("dbo.Properties", t => t.Property_PropertyId)
                .ForeignKey("dbo.Equities", t => t.Equity_AssetId)
                .Index(t => t.Bond_BondId)
                .Index(t => t.Property_PropertyId)
                .Index(t => t.Equity_AssetId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CashAccount_Id = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CashAccounts", t => t.CashAccount_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.CashAccount_Id)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.CashAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Bsb = c.String(nullable: false),
                        AccountName = c.String(nullable: false),
                        AccountNumber = c.String(nullable: false),
                        CashAccountType = c.Int(nullable: false),
                        MaturityDate = c.DateTime(),
                        Frequency = c.Int(nullable: false),
                        CurrencyType = c.Int(nullable: false),
                        TermsInMonths = c.Int(),
                        InterestRate = c.Double(),
                        AnnualInterest = c.Double(),
                        FaceValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CashTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        CashAccount_Id = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CashAccounts", t => t.CashAccount_Id, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.CashAccount_Id)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        PropertyAddress_PropertyId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyAddress_PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.PropertyAddress_PropertyId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        PropertyId = c.String(nullable: false, maxLength: 128),
                        GooglePlaceId = c.String(),
                        FullAddress = c.String(nullable: false),
                        PropertyType = c.String(nullable: false),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Postcode = c.String(),
                        Longitude = c.Double(),
                        Latitude = c.Double(),
                    })
                .PrimaryKey(t => t.PropertyId);
            
            CreateTable(
                "dbo.PropertyTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        IsBuy = c.Boolean(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        PropertyAddress_PropertyId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyAddress_PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.PropertyAddress_PropertyId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Dividends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        FrankingCredit = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Equity_AssetId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equities", t => t.Equity_AssetId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.Equity_AssetId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Equities",
                c => new
                    {
                        AssetId = c.String(nullable: false, maxLength: 128),
                        Ticker = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Sector = c.String(nullable: false),
                        EquityType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssetId);
            
            CreateTable(
                "dbo.EquityTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        NumberOfUnits = c.Int(nullable: false),
                        UnitPriceAtPurchase = c.Double(nullable: false),
                        EquityId = c.String(nullable: false, maxLength: 128),
                        TransactionDate = c.DateTime(nullable: false),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equities", t => t.EquityId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.EquityId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.InsuranceTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        PurchasedOn = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        EntitiesInsured = c.String(),
                        IsAcquire = c.Boolean(nullable: false),
                        Issuer = c.String(nullable: false),
                        InsuranceType = c.Int(nullable: false),
                        PolicyType = c.Int(nullable: false),
                        PolicyNumber = c.String(nullable: false),
                        NameOfPolicy = c.String(nullable: false),
                        PolicyAddress = c.String(nullable: false),
                        AmountInsured = c.Double(nullable: false),
                        Premium = c.Double(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.MarginLendingTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        GrantedOn = c.DateTime(nullable: false),
                        LoanAmount = c.Double(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        IsAcquire = c.Boolean(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.LiabilityRates",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Rate = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        EffectiveFrom = c.DateTime(nullable: false),
                        MortgageId = c.String(maxLength: 128),
                        MarginLendingId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MortgageHomeLoanTransactions", t => t.MortgageId)
                .ForeignKey("dbo.MarginLendingTransactions", t => t.MarginLendingId)
                .Index(t => t.MortgageId)
                .Index(t => t.MarginLendingId);
            
            CreateTable(
                "dbo.MortgageHomeLoanTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        LoanAquiredOn = c.DateTime(nullable: false),
                        PropertyId = c.String(nullable: false, maxLength: 128),
                        LoanAmount = c.Double(nullable: false),
                        LoanExpiryDate = c.DateTime(nullable: false),
                        LoanRepaymentType = c.Int(nullable: false),
                        Institution = c.String(nullable: false),
                        TypeOfMortgageRates = c.Int(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 128),
                        IsAcquire = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Properties", t => t.PropertyId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.PropertyId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.LoanValueRatios",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ratio = c.Double(nullable: false),
                        AssetId = c.String(nullable: false),
                        AssetTypes = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ActiveDate = c.DateTime(nullable: false),
                        MarginLendingTransaction_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarginLendingTransactions", t => t.MarginLendingTransaction_Id)
                .Index(t => t.MarginLendingTransaction_Id);
            
            CreateTable(
                "dbo.RepaymentRecords",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CorrespondingLiabilityGroupingKey = c.String(nullable: false),
                        LiabilityType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        PaymentOn = c.DateTime(nullable: false),
                        PrincipleAmount = c.Double(nullable: false),
                        InterestAmount = c.Double(nullable: false),
                        AccountId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Advisers",
                c => new
                    {
                        AdviserId = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        AdviserNumber = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        VerifiedId = c.Int(nullable: false),
                        ABNACN = c.String(),
                        AddressLn1 = c.String(),
                        AddressLn2 = c.String(),
                        AddressLn3 = c.String(),
                        State = c.String(),
                        Suburb = c.String(),
                        Country = c.String(),
                        PostCode = c.String(),
                        Fax = c.String(),
                        Mobile = c.String(),
                        Phone = c.String(),
                        CompanyName = c.String(),
                        CurrentTitle = c.String(),
                        Gender = c.String(),
                        ExperienceStartDate = c.DateTime(),
                        MiddleName = c.String(),
                        Title = c.String(),
                        Lng = c.Double(),
                        Lat = c.Double(),
                        LastUpdate = c.DateTime(),
                        Image = c.Binary(),
                        ImageMimeType = c.String(),
                    })
                .PrimaryKey(t => t.AdviserId);
            
            CreateTable(
                "dbo.ConsultancyExpenses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        IncurredOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        ConsultancyExpenseType = c.Int(nullable: false),
                        Notes = c.String(),
                        AdviserId = c.String(nullable: false, maxLength: 128),
                        AccountId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Advisers", t => t.AdviserId, cascadeDelete: true)
                .Index(t => t.AdviserId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.TransactionExpenses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IncurredOn = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        ExpenseType = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        CorrespondingTransactionId = c.String(nullable: false),
                        AdviserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advisers", t => t.AdviserId)
                .Index(t => t.AdviserId);
            
            CreateTable(
                "dbo.AustralianStates",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        State = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BondTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientGroups",
                c => new
                    {
                        ClientGroupId = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        MainClientId = c.String(nullable: false),
                        GroupNumber = c.String(nullable: false),
                        Adviser_AdviserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClientGroupId)
                .ForeignKey("dbo.Advisers", t => t.Adviser_AdviserId)
                .Index(t => t.Adviser_AdviserId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false),
                        ClientNumber = c.String(nullable: false),
                        ClientGroupId = c.String(nullable: false, maxLength: 128),
                        Address = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.ClientGroups", t => t.ClientGroupId, cascadeDelete: true)
                .Index(t => t.ClientGroupId);
            
            CreateTable(
                "dbo.IndexedEquities",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ticker = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Weighting = c.Double(nullable: false),
                        AsxIndexTypes = c.Int(nullable: false),
                        EquityType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyTypes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sectors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SectorName = c.String(nullable: false),
                        SectorGroup = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Clients", "ClientGroupId", "dbo.ClientGroups");
            DropForeignKey("dbo.Accounts", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Accounts", "ClientGroup_ClientGroupId", "dbo.ClientGroups");
            DropForeignKey("dbo.ClientGroups", "Adviser_AdviserId", "dbo.Advisers");
            DropForeignKey("dbo.TransactionExpenses", "AdviserId", "dbo.Advisers");
            DropForeignKey("dbo.ConsultancyExpenses", "AdviserId", "dbo.Advisers");
            DropForeignKey("dbo.ConsultancyExpenses", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.RepaymentRecords", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.PropertyTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.MortgageHomeLoanTransactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.MarginLendingTransactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.LoanValueRatios", "MarginLendingTransaction_Id", "dbo.MarginLendingTransactions");
            DropForeignKey("dbo.LiabilityRates", "MarginLendingId", "dbo.MarginLendingTransactions");
            DropForeignKey("dbo.LiabilityRates", "MortgageId", "dbo.MortgageHomeLoanTransactions");
            DropForeignKey("dbo.MortgageHomeLoanTransactions", "PropertyId", "dbo.Properties");
            DropForeignKey("dbo.InsuranceTransactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.CouponPayments", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.EquityTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Dividends", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Dividends", "Equity_AssetId", "dbo.Equities");
            DropForeignKey("dbo.ResearchValues", "Equity_AssetId", "dbo.Equities");
            DropForeignKey("dbo.AssetPrices", "Equity_AssetId", "dbo.Equities");
            DropForeignKey("dbo.EquityTransactions", "EquityId", "dbo.Equities");
            DropForeignKey("dbo.Rentals", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Rentals", "PropertyAddress_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.ResearchValues", "Property_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyTransactions", "PropertyAddress_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.AssetPrices", "Property_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.CashTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Interests", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Interests", "CashAccount_Id", "dbo.CashAccounts");
            DropForeignKey("dbo.CashTransactions", "CashAccount_Id", "dbo.CashAccounts");
            DropForeignKey("dbo.BondTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.BondTransactions", "BondId", "dbo.Bonds");
            DropForeignKey("dbo.ResearchValues", "Bond_BondId", "dbo.Bonds");
            DropForeignKey("dbo.AssetPrices", "Bond_BondId", "dbo.Bonds");
            DropForeignKey("dbo.CouponPayments", "Bond_BondId", "dbo.Bonds");
            DropIndex("dbo.Clients", new[] { "ClientGroupId" });
            DropIndex("dbo.ClientGroups", new[] { "Adviser_AdviserId" });
            DropIndex("dbo.TransactionExpenses", new[] { "AdviserId" });
            DropIndex("dbo.ConsultancyExpenses", new[] { "AccountId" });
            DropIndex("dbo.ConsultancyExpenses", new[] { "AdviserId" });
            DropIndex("dbo.RepaymentRecords", new[] { "AccountId" });
            DropIndex("dbo.LoanValueRatios", new[] { "MarginLendingTransaction_Id" });
            DropIndex("dbo.MortgageHomeLoanTransactions", new[] { "AccountId" });
            DropIndex("dbo.MortgageHomeLoanTransactions", new[] { "PropertyId" });
            DropIndex("dbo.LiabilityRates", new[] { "MarginLendingId" });
            DropIndex("dbo.LiabilityRates", new[] { "MortgageId" });
            DropIndex("dbo.MarginLendingTransactions", new[] { "AccountId" });
            DropIndex("dbo.InsuranceTransactions", new[] { "AccountId" });
            DropIndex("dbo.EquityTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.EquityTransactions", new[] { "EquityId" });
            DropIndex("dbo.Dividends", new[] { "Account_AccountId" });
            DropIndex("dbo.Dividends", new[] { "Equity_AssetId" });
            DropIndex("dbo.PropertyTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.PropertyTransactions", new[] { "PropertyAddress_PropertyId" });
            DropIndex("dbo.Rentals", new[] { "Account_AccountId" });
            DropIndex("dbo.Rentals", new[] { "PropertyAddress_PropertyId" });
            DropIndex("dbo.CashTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.CashTransactions", new[] { "CashAccount_Id" });
            DropIndex("dbo.Interests", new[] { "Account_AccountId" });
            DropIndex("dbo.Interests", new[] { "CashAccount_Id" });
            DropIndex("dbo.ResearchValues", new[] { "Equity_AssetId" });
            DropIndex("dbo.ResearchValues", new[] { "Property_PropertyId" });
            DropIndex("dbo.ResearchValues", new[] { "Bond_BondId" });
            DropIndex("dbo.AssetPrices", new[] { "Equity_AssetId" });
            DropIndex("dbo.AssetPrices", new[] { "Property_PropertyId" });
            DropIndex("dbo.AssetPrices", new[] { "Bond_BondId" });
            DropIndex("dbo.CouponPayments", new[] { "Account_AccountId" });
            DropIndex("dbo.CouponPayments", new[] { "Bond_BondId" });
            DropIndex("dbo.BondTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.BondTransactions", new[] { "BondId" });
            DropIndex("dbo.Accounts", new[] { "Client_ClientId" });
            DropIndex("dbo.Accounts", new[] { "ClientGroup_ClientGroupId" });
            DropTable("dbo.Sectors");
            DropTable("dbo.PropertyTypes");
            DropTable("dbo.IndexedEquities");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientGroups");
            DropTable("dbo.BondTypes");
            DropTable("dbo.AustralianStates");
            DropTable("dbo.TransactionExpenses");
            DropTable("dbo.ConsultancyExpenses");
            DropTable("dbo.Advisers");
            DropTable("dbo.RepaymentRecords");
            DropTable("dbo.LoanValueRatios");
            DropTable("dbo.MortgageHomeLoanTransactions");
            DropTable("dbo.LiabilityRates");
            DropTable("dbo.MarginLendingTransactions");
            DropTable("dbo.InsuranceTransactions");
            DropTable("dbo.EquityTransactions");
            DropTable("dbo.Equities");
            DropTable("dbo.Dividends");
            DropTable("dbo.PropertyTransactions");
            DropTable("dbo.Properties");
            DropTable("dbo.Rentals");
            DropTable("dbo.CashTransactions");
            DropTable("dbo.CashAccounts");
            DropTable("dbo.Interests");
            DropTable("dbo.ResearchValues");
            DropTable("dbo.AssetPrices");
            DropTable("dbo.CouponPayments");
            DropTable("dbo.Bonds");
            DropTable("dbo.BondTransactions");
            DropTable("dbo.Accounts");
        }
    }
}
