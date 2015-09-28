using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Db.Assets;
using Edis.Db.ExpenseRecords;
using Edis.Db.IncomeRecords;
using Edis.Db.Liabilities;
using Edis.Db.Transactions;
using Shared;


namespace Edis.Db
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
        public DbSet<ConsultancyExpense> ConsultancyExpenses { get; set; }
        public DbSet<CouponPayment> CouponPayments { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BondTransaction> BondTransactions { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<EquityTransaction> EquityTransactions { get; set; }
        public DbSet<PropertyTransaction> PropertyTransactions { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<BondType> BondTypes { get; set; }
        public DbSet<AustralianState> AustralianStates { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<IndexedEquity> IndexedEquities { get; set; }
        public DbSet<MortgageHomeLoanTransaction> MortgageHomeLoanTransactions { get; set; }
        public DbSet<MarginLendingTransaction> MarginLendingTransactions { get; set; }
        public DbSet<InsuranceTransaction> InsuranceTransactions { get; set; }
        public DbSet<RepaymentRecord> RepaymentRecords { get; set; }





        public EdisContext() : base("EdisDb")
        {            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionExpense>().HasOptional(e=>e.Adviser)
                .WithMany(a=>a.TransactionExpenses).WillCascadeOnDelete(false);
            modelBuilder.Entity<ConsultancyExpense>().HasRequired(c=>c.Adviser)
                .WithMany(c=>c.ConsultancyExpenses).WillCascadeOnDelete(true);

            modelBuilder.Entity<MortgageHomeLoanTransaction>().HasMany(m=>m.InterestRate)
                .WithOptional(i=>i.MortgageHomeLoan).WillCascadeOnDelete(false);
            //modelBuilder.Entity<MarginLendingTransaction>().HasMany(m=>m.Equities)
            //    .WithOptional(e=>e.MarginLending).WillCascadeOnDelete(false);
            //modelBuilder.Entity<MarginLendingTransaction>().HasMany(m=>m.Bonds)
            //    .WithOptional(b=>b.MarginLending).WillCascadeOnDelete(false);
            modelBuilder.Entity<MarginLendingTransaction>().HasMany(m=>m.LiabilityRates)
                .WithOptional(r=>r.MarginLending).WillCascadeOnDelete(false);



            modelBuilder.Entity<MarginLendingTransaction>().HasMany(m => m.LoanValueRatios);
            modelBuilder.Entity<Account>().HasMany(ac=>ac.Insurances)
                .WithRequired(c=>c.Account).WillCascadeOnDelete(true);

            modelBuilder.Entity<Account>().HasMany(a=>a.MarginLendings)
                .WithRequired(m=>m.Account).WillCascadeOnDelete(true);
            modelBuilder.Entity<Account>().HasMany(ac=>ac.MortgageHomeLoans)
                .WithRequired(b=>b.Account).WillCascadeOnDelete(true);
            modelBuilder.Entity<Account>().HasMany(ac=>ac.RepaymentRecords)
                .WithRequired(c=>c.Account).WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            try
            {
                return await base.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }


    }
}
