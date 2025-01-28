namespace BudgetTracker.web.Dtos
{
    public class ExpenseForecastDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BudgetId { get; set; }
        public string ForecastName { get; set; } = string.Empty;
        public DateTime ForecastDate { get; set; }
        public decimal PredictedAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
