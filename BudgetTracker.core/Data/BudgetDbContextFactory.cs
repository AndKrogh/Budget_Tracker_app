using BudgetTracker.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class BudgetDbContextFactory : IDesignTimeDbContextFactory<BudgetDbContext>
{
    public BudgetDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BudgetDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=BudgetTrackerDb;Trusted_Connection=True;");

        return new BudgetDbContext(optionsBuilder.Options);
    }
}