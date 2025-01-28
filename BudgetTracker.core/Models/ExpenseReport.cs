namespace BudgetTracker.core.Models
{
    public class ExpenseReport
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ReportName { get; set; }
        public DateTime GeneratedDate { get; set; }
        public int BudgetId { get; set; }
        public string BudgetName { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetSavings => TotalIncome - TotalExpenses;

        public User User { get; set; }
    }
}
