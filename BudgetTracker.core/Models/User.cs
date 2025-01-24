namespace BudgetTracker.core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<Budget> Budgets { get; set; }
        public ICollection<BudgetEntry> BudgetEntries { get; set; } 
        public ICollection<ExpenseReport> ExpenseReports { get; set; } 
        public UserBudgetSummary UserBudgetSummary { get; set; }
    }
}
