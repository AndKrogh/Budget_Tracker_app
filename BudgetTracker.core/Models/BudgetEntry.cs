namespace BudgetTracker.core.Models
{
    public class BudgetEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int BudgetId { get; set; }

        public User User { get; set; }
        public Budget Budget { get; set; }
    }
}
