namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Gender", c => c.String());
            AddColumn("dbo.Clients", "Mobile", c => c.String());
            AddColumn("dbo.Clients", "Fax", c => c.String());
            AddColumn("dbo.Clients", "MiddleName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "MiddleName");
            DropColumn("dbo.Clients", "Fax");
            DropColumn("dbo.Clients", "Mobile");
            DropColumn("dbo.Clients", "Gender");
        }
    }
}
