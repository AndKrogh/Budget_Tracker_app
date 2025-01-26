using BudgetTracker.core.Models;
using BudgetTracker.core.Repositories;

namespace BudgetTracker.core.Services
{
    public class ReportService
    {
        private readonly BudgetRepository _budgetRepository;

        public ReportService(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<ExpenseReport> GenerateReportAsync(int budgetId)
        {
            var budget = await _budgetRepository.GetByIdAsync(budgetId);
            var entries = await _budgetRepository.GetBudgetEntriesAsync(budgetId);

            if (budget == null || entries == null)
            {
                return null; // Handle error
            }

            return new ExpenseReport
            {
                Budget = budget,
                Entries = entries,
                TotalExpenses = entries.Sum(e => e.Amount),
                RemainingBudget = budget.TotalAmount - entries.Sum(e => e.Amount)
            };
        }

        public async Task<ExpenseForecast> GetExpenseForecastAsync(int budgetId)
        {
            var entries = await _budgetRepository.GetBudgetEntriesAsync(budgetId);

            if (entries == null)
            {
                return null; // Handle error
            }

            // Forecast logic
            var totalExpenses = entries.Sum(e => e.Amount);
            var averageExpense = entries.Any() ? totalExpenses / entries.Count() : 0;

            return new ExpenseForecast
            {
                BudgetId = budgetId,
                AverageExpense = averageExpense,
                EstimatedTotalExpenses = averageExpense * 30 // Example: Monthly estimate
            };
        }
    }
}
