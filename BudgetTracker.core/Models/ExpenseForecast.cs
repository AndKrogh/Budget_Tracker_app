namespace BudgetTracker.core.Models
{
    public class ExpenseForecast
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ForecastName { get; set; }
        public DateTime ForecastDate { get; set; }
        public decimal PredictedAmount { get; set; }
        public string Notes { get; set; }
        public int BudgetId { get; set; }

        public User User { get; set; }
        public Budget Budget { get; set; }
    }
}
