using BudgetTracker.core.Models;
using Microsoft.EntityFrameworkCore;

public class BudgetDbContext : DbContext
{
    public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=BudgetTrackerDb;Trusted_Connection=True;");
        }
    }

    public DbSet<Budget> Budgets { get; set; }
}
