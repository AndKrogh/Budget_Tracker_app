using BudgetTracker.core.Models;
using BudgetTracker.core.Repositories;

namespace BudgetTracker.core.Services
{
    public class BudgetService
    {
        private readonly BudgetRepository _budgetRepository;

        public BudgetService(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            return await _budgetRepository.GetAllBudgetsAsync();
        }

        public async Task<Budget> GetBudgetByIdAsync(int budgetId)
        {
            return await _budgetRepository.GetByIdAsync(budgetId);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsForUserAsync(int userId)
        {
            return await _budgetRepository.GetBudgetsByUserIdAsync(userId);
        }

        public async Task<bool> CreateBudgetAsync(Budget budget)
        {
            return await _budgetRepository.AddAsync(budget);
        }

        public async Task<bool> UpdateBudgetAsync(Budget budget)
        {
            return await _budgetRepository.UpdateAsync(budget);
        }

        public async Task<bool> DeleteBudgetAsync(int budgetId)
        {
            return await _budgetRepository.DeleteAsync(budgetId);
        }
    }
}