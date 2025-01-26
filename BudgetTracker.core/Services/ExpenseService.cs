using BudgetTracker.core.Models;
using BudgetTracker.core.Repositories;

namespace BudgetTracker.core.Services
{
    public class ExpenseService
    {
        private readonly BudgetRepository _budgetRepository;

        public ExpenseService(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<BudgetEntry>> GetExpensesByBudgetIdAsync(int budgetId)
        {
            return await _budgetRepository.GetBudgetEntriesAsync(budgetId);
        }

        public async Task<bool> AddExpenseAsync(BudgetEntry expense)
        {
            return await _budgetRepository.AddBudgetEntryAsync(expense);
        }

        public async Task<bool> UpdateExpenseAsync(BudgetEntry expense)
        {
            return await _budgetRepository.UpdateBudgetEntryAsync(expense);
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId)
        {
            return await _budgetRepository.DeleteBudgetEntryAsync(expenseId);
        }
    }
}
