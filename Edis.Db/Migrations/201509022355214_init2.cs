namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advisers", "AnnualIncomeLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "TotalAssetLevel", c => c.String());
            DropColumn("dbo.Advisers", "BAddressLine1");
            DropColumn("dbo.Advisers", "BAddressLine2");
            DropColumn("dbo.Advisers", "BAddressLine3");
            DropColumn("dbo.Advisers", "BSuburb");
            DropColumn("dbo.Advisers", "BState");
            DropColumn("dbo.Advisers", "BPostcode");
            DropColumn("dbo.Advisers", "BCountry");
            DropColumn("dbo.Advisers", "SnnualIncomeLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Advisers", "SnnualIncomeLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "BCountry", c => c.String());
            AddColumn("dbo.Advisers", "BPostcode", c => c.String());
            AddColumn("dbo.Advisers", "BState", c => c.String());
            AddColumn("dbo.Advisers", "BSuburb", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine3", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine2", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine1", c => c.String());
            DropColumn("dbo.Advisers", "TotalAssetLevel");
            DropColumn("dbo.Advisers", "AnnualIncomeLevelId");
        }
    }
}
