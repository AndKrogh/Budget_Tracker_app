namespace BudgetTracker.core.Models
{
    public class BudgetEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
