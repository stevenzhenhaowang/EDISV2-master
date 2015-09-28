using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Assets;
using Database.ExpenseRecords;
using Database.IncomeRecords;
using Database.Transactions;

namespace Database
{
    public class EdisContext : DbContext
    {
        public DbSet<Adviser> Advisers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientGroup> ClientGroups { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Bond> Bonds { get; set; }
        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<Equity> Equities { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<TransactionExpense> TransactionExpenses { get; set; }

        public DbSet<CouponPayment> CouponPayments { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<BondTransaction> BondTransactions { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<EquityTransaction> EquityTransactions { get; set; }
        public DbSet<PropertyTransaction> PropertyTransactions { get; set; }

        public EdisContext() : base("EdisDb")
        {            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientGroup>().HasMany(c => c.Clients)
                .WithRequired(c => c.ClientGroup).WillCascadeOnDelete(false);
            modelBuilder.Entity<Client>().HasRequired(c => c.ClientGroup)
                .WithMany(g=>g.Clients).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
