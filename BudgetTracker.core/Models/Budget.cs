namespace BudgetTracker.core.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public User User { get; set; } 
        public ICollection<BudgetEntry> BudgetEntries { get; set; } 
        public ICollection<ExpenseForecast> ExpenseForecasts { get; set; }
    }
}
