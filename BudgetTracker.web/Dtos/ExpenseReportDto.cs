namespace BudgetTracker.web.Dtos
{
    public class ExpenseReportDto
    {
        public int BudgetId { get; set; }
        public int UserId { get; set; }
        public string ReportName { get; set; }
        public DateTime GeneratedDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetSavings { get; set; }
    }
}
