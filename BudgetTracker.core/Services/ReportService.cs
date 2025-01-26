using BudgetTracker.core.Models;
using BudgetTracker.core.Repositories;

namespace BudgetTracker.core.Services
{
    public class ReportService
    {
        private readonly BudgetRepository _budgetRepository;
        private readonly UserRepository _userRepository;

        public ReportService(BudgetRepository budgetRepository, UserRepository userRepository)
        {
            _budgetRepository = budgetRepository;
            _userRepository = userRepository;
        }

        public async Task<ExpenseReport> GenerateReportAsync(int budgetId, int userId)
        {
            var budget = await _budgetRepository.GetByIdAsync(budgetId);
            var entries = await _budgetRepository.GetBudgetEntriesAsync(budgetId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (budget == null || entries == null)
            {
                return null;
            }

            return new ExpenseReport
            {
                Id = 0,
                UserId = userId,
                ReportName = $"Expense Report for {budget.Name} - {DateTime.Now.Year}",
                GeneratedDate = DateTime.Now,
                User = user
            };
        }

        public async Task<ExpenseForecast> GetExpenseForecastAsync(int budgetId, int userId)
        {
            var entries = await _budgetRepository.GetBudgetEntriesAsync(budgetId);
            var budget = await _budgetRepository.GetByIdAsync(budgetId);
            var user = await _userRepository.GetByIdAsync(userId);

            if (entries == null || budget == null || user == null)
            {
                return null;
            }

            var totalExpenses = entries.Sum(e => e.Amount);
            var averageExpense = entries.Any() ? totalExpenses / entries.Count() : 0;
            var estimatedTotalExpenses = averageExpense * 30;

            return new ExpenseForecast
            {
                Id = 0,
                UserId = userId,
                ForecastName = $"Expense Forecast for {budget.Name} - {DateTime.Now.Year}",
                ForecastDate = DateTime.Now,
                PredictedAmount = estimatedTotalExpenses,
                Notes = "Estimated total expenses for the next month.",
                BudgetId = budgetId,
                User = user,
                Budget = budget
            };
        }
    }
}
