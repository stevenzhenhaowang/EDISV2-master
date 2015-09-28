namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CashTransactions", name: "CashAccount_Id", newName: "CashAccountId");
            RenameIndex(table: "dbo.CashTransactions", name: "IX_CashAccount_Id", newName: "IX_CashAccountId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CashTransactions", name: "IX_CashAccountId", newName: "IX_CashAccount_Id");
            RenameColumn(table: "dbo.CashTransactions", name: "CashAccountId", newName: "CashAccount_Id");
        }
    }
}
