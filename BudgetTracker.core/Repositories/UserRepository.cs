using BudgetTracker.core.Models;
using BudgetTracker.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.core.Repositories
{
    public class UserRepository
    {
        private readonly BudgetDbContext _context;

        public UserRepository(BudgetDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> AddAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}