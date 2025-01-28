namespace BudgetTracker.web.Dtos
{
    public class BudgetEntryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int BudgetId { get; set; }
    }
}
