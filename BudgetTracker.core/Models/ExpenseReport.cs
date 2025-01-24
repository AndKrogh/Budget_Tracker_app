namespace BudgetTracker.core.Models
{
    public class ExpenseReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReportName { get; set; }
        public DateTime GeneratedDate { get; set; }

        public User User { get; set; }
    }
}
