namespace Database.Migrations
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
                        CreatedOn = c.DateTime(nullable: false),
                        Client_ClientId = c.String(maxLength: 128),
                        ClientGroup_ClientGroupId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Clients", t => t.Client_ClientId)
                .ForeignKey("dbo.ClientGroups", t => t.ClientGroup_ClientGroupId)
                .Index(t => t.Client_ClientId)
                .Index(t => t.ClientGroup_ClientGroupId);
            
            CreateTable(
                "dbo.BondTransactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        NumberOfUnits = c.Int(nullable: false),
                        UnitPriceAtPurchase = c.Double(nullable: false),
                        Bond_BondId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_BondId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.Bond_BondId)
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
                        Bond_BondId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bonds", t => t.Bond_BondId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.Bond_BondId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PaymentOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
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
                        MaturityDate = c.DateTime(nullable: false),
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
                        GooglePlaceId = c.String(nullable: false),
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
                        Equity_AssetId = c.String(nullable: false, maxLength: 128),
                        Account_AccountId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equities", t => t.Equity_AssetId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .Index(t => t.Equity_AssetId)
                .Index(t => t.Account_AccountId);
            
            CreateTable(
                "dbo.Advisers",
                c => new
                    {
                        AdviserId = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        AdviserNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdviserId);
            
            CreateTable(
                "dbo.ClientGroups",
                c => new
                    {
                        ClientGroupId = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        MainClientId = c.String(nullable: false, maxLength: 128),
                        GroupNumber = c.String(nullable: false),
                        Adviser_AdviserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ClientGroupId)
                .ForeignKey("dbo.Advisers", t => t.Adviser_AdviserId)
                .ForeignKey("dbo.Clients", t => t.MainClientId, cascadeDelete: true)
                .Index(t => t.MainClientId)
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
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.ClientGroups", t => t.ClientGroupId)
                .Index(t => t.ClientGroupId);
            
            CreateTable(
                "dbo.TransactionExpenses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        ExpenseType = c.Int(nullable: false),
                        TransactionType = c.Int(nullable: false),
                        CorrespondingTransactionId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientGroups", "MainClientId", "dbo.Clients");
            DropForeignKey("dbo.Accounts", "ClientGroup_ClientGroupId", "dbo.ClientGroups");
            DropForeignKey("dbo.Clients", "ClientGroupId", "dbo.ClientGroups");
            DropForeignKey("dbo.Accounts", "Client_ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientGroups", "Adviser_AdviserId", "dbo.Advisers");
            DropForeignKey("dbo.PropertyTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.CouponPayments", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.EquityTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Dividends", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Dividends", "Equity_AssetId", "dbo.Equities");
            DropForeignKey("dbo.EquityTransactions", "Equity_AssetId", "dbo.Equities");
            DropForeignKey("dbo.Rentals", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Rentals", "PropertyAddress_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.PropertyTransactions", "PropertyAddress_PropertyId", "dbo.Properties");
            DropForeignKey("dbo.CashTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Interests", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Interests", "CashAccount_Id", "dbo.CashAccounts");
            DropForeignKey("dbo.CashTransactions", "CashAccount_Id", "dbo.CashAccounts");
            DropForeignKey("dbo.BondTransactions", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.BondTransactions", "Bond_BondId", "dbo.Bonds");
            DropForeignKey("dbo.CouponPayments", "Bond_BondId", "dbo.Bonds");
            DropIndex("dbo.Clients", new[] { "ClientGroupId" });
            DropIndex("dbo.ClientGroups", new[] { "Adviser_AdviserId" });
            DropIndex("dbo.ClientGroups", new[] { "MainClientId" });
            DropIndex("dbo.EquityTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.EquityTransactions", new[] { "Equity_AssetId" });
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
            DropIndex("dbo.CouponPayments", new[] { "Account_AccountId" });
            DropIndex("dbo.CouponPayments", new[] { "Bond_BondId" });
            DropIndex("dbo.BondTransactions", new[] { "Account_AccountId" });
            DropIndex("dbo.BondTransactions", new[] { "Bond_BondId" });
            DropIndex("dbo.Accounts", new[] { "ClientGroup_ClientGroupId" });
            DropIndex("dbo.Accounts", new[] { "Client_ClientId" });
            DropTable("dbo.TransactionExpenses");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientGroups");
            DropTable("dbo.Advisers");
            DropTable("dbo.EquityTransactions");
            DropTable("dbo.Equities");
            DropTable("dbo.Dividends");
            DropTable("dbo.PropertyTransactions");
            DropTable("dbo.Properties");
            DropTable("dbo.Rentals");
            DropTable("dbo.CashTransactions");
            DropTable("dbo.CashAccounts");
            DropTable("dbo.Interests");
            DropTable("dbo.CouponPayments");
            DropTable("dbo.Bonds");
            DropTable("dbo.BondTransactions");
            DropTable("dbo.Accounts");
        }
    }
}
