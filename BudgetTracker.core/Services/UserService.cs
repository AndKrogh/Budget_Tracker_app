using BudgetTracker.core.Models;
using BudgetTracker.core.Repositories;

namespace BudgetTracker.core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteAsync(userId);
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            var user = (await _userRepository.GetAllAsync())
                         .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
            return user;
        }

        public async Task<bool> LogoutAsync()
        {
            await Task.CompletedTask;
            return true;
        }
    }
}
