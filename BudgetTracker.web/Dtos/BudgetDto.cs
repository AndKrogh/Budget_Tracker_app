namespace BudgetTracker.web.Dtos
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetSavings => TotalIncome - TotalExpenses;
    }
}

