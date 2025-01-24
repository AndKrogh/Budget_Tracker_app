namespace BudgetTracker.core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<BudgetEntry> BudgetEntries { get; set; } = new List<BudgetEntry>();
        public ICollection<ExpenseReport> ExpenseReports { get; set; } = new List<ExpenseReport>();
        public UserBudgetSummary UserBudgetSummary { get; set; }
    }
}
