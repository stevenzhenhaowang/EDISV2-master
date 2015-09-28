namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Advisers", "IndustryExperienceStartDate", c => c.DateTime());
            AddColumn("dbo.Advisers", "BusinessPhone", c => c.String());
            AddColumn("dbo.Advisers", "BusinessMobile", c => c.String());
            AddColumn("dbo.Advisers", "BusinessFax", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine1", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine2", c => c.String());
            AddColumn("dbo.Advisers", "BAddressLine3", c => c.String());
            AddColumn("dbo.Advisers", "BSuburb", c => c.String());
            AddColumn("dbo.Advisers", "BState", c => c.String());
            AddColumn("dbo.Advisers", "BPostcode", c => c.String());
            AddColumn("dbo.Advisers", "BCountry", c => c.String());
            AddColumn("dbo.Advisers", "RoleAndServicesSummary", c => c.String());
            AddColumn("dbo.Advisers", "DealerGroupName", c => c.String());
            AddColumn("dbo.Advisers", "Asfl", c => c.String());
            AddColumn("dbo.Advisers", "DAddressLine1", c => c.String());
            AddColumn("dbo.Advisers", "DAddressLine2", c => c.String());
            AddColumn("dbo.Advisers", "DAddressLine3", c => c.String());
            AddColumn("dbo.Advisers", "DSuburb", c => c.String());
            AddColumn("dbo.Advisers", "DState", c => c.String());
            AddColumn("dbo.Advisers", "DPostcode", c => c.String());
            AddColumn("dbo.Advisers", "DCountry", c => c.String());
            AddColumn("dbo.Advisers", "DealerGroupHasDerivativesLicense", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "IsAuthorizedRepresentative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "AuthorizedRepresentativeNumber", c => c.String());
            AddColumn("dbo.Advisers", "TotalAssetUnderManagement", c => c.String());
            AddColumn("dbo.Advisers", "TotalInvestmentUndermanagement", c => c.String());
            AddColumn("dbo.Advisers", "TotalDirectAustralianEquitiesUnderManagement", c => c.String());
            AddColumn("dbo.Advisers", "TotalDirectInterantionalEquitiesUnderManagement", c => c.String());
            AddColumn("dbo.Advisers", "TotalDirectFixedInterestUnderManagement", c => c.String());
            AddColumn("dbo.Advisers", "TotalDirectLendingBookInterestUnderManagement", c => c.String());
            AddColumn("dbo.Advisers", "ApproximateNumberOfClients", c => c.String());
            AddColumn("dbo.Advisers", "Institution", c => c.String());
            AddColumn("dbo.Advisers", "EducationLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "CourseTitle", c => c.String());
            AddColumn("dbo.Advisers", "CourseStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "ProfessiontypeId", c => c.String());
            AddColumn("dbo.Advisers", "NewsLetterServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "NewsLetterServiceName", c => c.String());
            AddColumn("dbo.Advisers", "NewsLetterSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "GroupName", c => c.String());
            AddColumn("dbo.Advisers", "ServiceId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "ServiceName", c => c.String());
            AddColumn("dbo.Advisers", "Providing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "CAFId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "CAFSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "CAFDescription", c => c.String());
            AddColumn("dbo.Advisers", "RemunerationMethodSpecified", c => c.Boolean(nullable: false));
            AddColumn("dbo.Advisers", "RemunerationMethod", c => c.String());
            AddColumn("dbo.Advisers", "NumberOfClientsId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "SnnualIncomeLevelId", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "InvestibleAssetLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Advisers", "TotalAssetLevelId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Advisers", "TotalAssetLevelId");
            DropColumn("dbo.Advisers", "InvestibleAssetLevel");
            DropColumn("dbo.Advisers", "SnnualIncomeLevelId");
            DropColumn("dbo.Advisers", "NumberOfClientsId");
            DropColumn("dbo.Advisers", "RemunerationMethod");
            DropColumn("dbo.Advisers", "RemunerationMethodSpecified");
            DropColumn("dbo.Advisers", "CAFDescription");
            DropColumn("dbo.Advisers", "CAFSelected");
            DropColumn("dbo.Advisers", "CAFId");
            DropColumn("dbo.Advisers", "Providing");
            DropColumn("dbo.Advisers", "ServiceName");
            DropColumn("dbo.Advisers", "ServiceId");
            DropColumn("dbo.Advisers", "GroupName");
            DropColumn("dbo.Advisers", "NewsLetterSelected");
            DropColumn("dbo.Advisers", "NewsLetterServiceName");
            DropColumn("dbo.Advisers", "NewsLetterServiceId");
            DropColumn("dbo.Advisers", "ProfessiontypeId");
            DropColumn("dbo.Advisers", "CourseStatus");
            DropColumn("dbo.Advisers", "CourseTitle");
            DropColumn("dbo.Advisers", "EducationLevelId");
            DropColumn("dbo.Advisers", "Institution");
            DropColumn("dbo.Advisers", "ApproximateNumberOfClients");
            DropColumn("dbo.Advisers", "TotalDirectLendingBookInterestUnderManagement");
            DropColumn("dbo.Advisers", "TotalDirectFixedInterestUnderManagement");
            DropColumn("dbo.Advisers", "TotalDirectInterantionalEquitiesUnderManagement");
            DropColumn("dbo.Advisers", "TotalDirectAustralianEquitiesUnderManagement");
            DropColumn("dbo.Advisers", "TotalInvestmentUndermanagement");
            DropColumn("dbo.Advisers", "TotalAssetUnderManagement");
            DropColumn("dbo.Advisers", "AuthorizedRepresentativeNumber");
            DropColumn("dbo.Advisers", "IsAuthorizedRepresentative");
            DropColumn("dbo.Advisers", "DealerGroupHasDerivativesLicense");
            DropColumn("dbo.Advisers", "DCountry");
            DropColumn("dbo.Advisers", "DPostcode");
            DropColumn("dbo.Advisers", "DState");
            DropColumn("dbo.Advisers", "DSuburb");
            DropColumn("dbo.Advisers", "DAddressLine3");
            DropColumn("dbo.Advisers", "DAddressLine2");
            DropColumn("dbo.Advisers", "DAddressLine1");
            DropColumn("dbo.Advisers", "Asfl");
            DropColumn("dbo.Advisers", "DealerGroupName");
            DropColumn("dbo.Advisers", "RoleAndServicesSummary");
            DropColumn("dbo.Advisers", "BCountry");
            DropColumn("dbo.Advisers", "BPostcode");
            DropColumn("dbo.Advisers", "BState");
            DropColumn("dbo.Advisers", "BSuburb");
            DropColumn("dbo.Advisers", "BAddressLine3");
            DropColumn("dbo.Advisers", "BAddressLine2");
            DropColumn("dbo.Advisers", "BAddressLine1");
            DropColumn("dbo.Advisers", "BusinessFax");
            DropColumn("dbo.Advisers", "BusinessMobile");
            DropColumn("dbo.Advisers", "BusinessPhone");
            DropColumn("dbo.Advisers", "IndustryExperienceStartDate");
        }
    }
}
