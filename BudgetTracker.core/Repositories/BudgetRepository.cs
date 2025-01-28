using BudgetTracker.core.Models;
using BudgetTracker.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.core.Repositories
{
    public class BudgetRepository
    {
        private readonly BudgetDbContext _context;

        public BudgetRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<Budget> GetByIdAsync(int budgetId)
        {
            return await _context.Budgets
                .Include(b => b.BudgetEntries)
                .FirstOrDefaultAsync(b => b.Id == budgetId);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsByUserIdAsync(int userId)
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> AddAsync(Budget budget)
        {
            _context.Budgets.Add(budget);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Budget budget)
        {
            _context.Budgets.Update(budget);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int budgetId)
        {
            var budget = await GetByIdAsync(budgetId);
            if (budget == null)
            {
                return false;
            }

            _context.Budgets.Remove(budget);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<BudgetEntry>> GetBudgetEntriesAsync(int budgetId)
        {
            return await _context.BudgetEntries
                .Where(be => be.BudgetId == budgetId)
                .ToListAsync();
        }

        public async Task<bool> AddBudgetEntryAsync(BudgetEntry budgetEntry)
        {
            _context.BudgetEntries.Add(budgetEntry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateBudgetEntryAsync(BudgetEntry budgetEntry)
        {
            _context.BudgetEntries.Update(budgetEntry);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBudgetEntryAsync(int budgetEntryId)
        {
            var budgetEntry = await _context.BudgetEntries.FindAsync(budgetEntryId);
            if (budgetEntry == null)
            {
                return false;
            }

            _context.BudgetEntries.Remove(budgetEntry);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}