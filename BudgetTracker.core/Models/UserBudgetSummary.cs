namespace BudgetTracker.core.Models
{
    public class UserBudgetSummary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal NetBalance { get; set; }

        public User User { get; set; }
    }
}
