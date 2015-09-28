namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientGroups", "GroupName", c => c.String());
            AddColumn("dbo.ClientGroups", "GroupAlias", c => c.String());
            AddColumn("dbo.Clients", "ClientType", c => c.String(nullable: false));
            AddColumn("dbo.Clients", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Clients", "EntityName", c => c.String());
            AddColumn("dbo.Clients", "EntityType", c => c.String());
            AddColumn("dbo.Clients", "ABN", c => c.String());
            AddColumn("dbo.Clients", "ACN", c => c.String());
            AlterColumn("dbo.Clients", "FirstName", c => c.String());
            AlterColumn("dbo.Clients", "LastName", c => c.String());
            AlterColumn("dbo.Clients", "Dob", c => c.DateTime());
            AlterColumn("dbo.Clients", "Address", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Dob", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clients", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "FirstName", c => c.String(nullable: false));
            DropColumn("dbo.Clients", "ACN");
            DropColumn("dbo.Clients", "ABN");
            DropColumn("dbo.Clients", "EntityType");
            DropColumn("dbo.Clients", "EntityName");
            DropColumn("dbo.Clients", "Age");
            DropColumn("dbo.Clients", "ClientType");
            DropColumn("dbo.ClientGroups", "GroupAlias");
            DropColumn("dbo.ClientGroups", "GroupName");
        }
    }
}
