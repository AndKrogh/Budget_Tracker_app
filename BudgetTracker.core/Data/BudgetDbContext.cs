using BudgetTracker.core.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Core.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            : base(options)
        {
        }

        public DbSet<BudgetEntry> BudgetEntries { get; set; }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBudgetSummary> UserBudgetSummaries { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<ExpenseForecast> ExpenseForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // BudgetEntry -> User (Many-to-One)
            modelBuilder.Entity<BudgetEntry>()
                .HasOne(be => be.User)
                .WithMany(u => u.BudgetEntries)
                .HasForeignKey(be => be.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ExpenseReport -> User (Many-to-One)
            modelBuilder.Entity<ExpenseReport>()
                .HasOne(er => er.User)
                .WithMany(u => u.ExpenseReports)
                .HasForeignKey(er => er.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserBudgetSummary -> User (One-to-One)
            modelBuilder.Entity<UserBudgetSummary>()
                .HasOne(ubs => ubs.User)
                .WithOne(u => u.UserBudgetSummary)
                .HasForeignKey<UserBudgetSummary>(ubs => ubs.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Budget -> User (Many-to-One)
            modelBuilder.Entity<Budget>()
                .HasOne(b => b.User)
                .WithMany(u => u.Budgets)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // BudgetEntry -> Budget (Many-to-One)
            modelBuilder.Entity<BudgetEntry>()
                .HasOne(be => be.Budget)
                .WithMany(b => b.BudgetEntries)
                .HasForeignKey(be => be.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            // ExpenseForecast -> Budget (Many-to-One)
            modelBuilder.Entity<ExpenseForecast>()
                .HasOne(ef => ef.Budget)
                .WithMany(b => b.ExpenseForecasts)
                .HasForeignKey(ef => ef.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
