using BudgetTracker.core.Models;
using BudgetTracker.core.Services;
using BudgetTracker.web.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly BackofficeUserService _backofficeUserService;

        public UserController(UserService userService, BackofficeUserService backofficeUserService)
        {
            _userService = userService;
            _backofficeUserService = backofficeUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.PasswordHash))
                return BadRequest(new { Message = "Username and password are required." });

            try
            {
                var existingUsers = await _userService.GetAllUsersAsync();
                if (existingUsers.Any(u => u.Username == user.Username))
                    return Conflict(new { Message = "Username already exists." });

                var success = await _userService.CreateUserAsync(user);
                if (!success)
                    return StatusCode(500, new { Message = "Failed to register user. Please try again." });

                return Ok(new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during registration.", Details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var loggedInUser = await _userService.LoginAsync(user.Username, user.PasswordHash);
                if (loggedInUser == null)
                    return Unauthorized(new { Message = "Invalid username or password." });

                return Ok(new { Message = "Login successful.", UserId = loggedInUser.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during login.", Details = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var success = await _userService.LogoutAsync();
                if (!success)
                    return BadRequest(new { Message = "Logout failed." });

                return Ok(new { Message = "Logout successful." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during logout.", Details = ex.Message });
            }
        }

        [HttpPost("import-all-from-backoffice")]
        public async Task<IActionResult> ImportAllFromBackoffice()
        {
            try
            {
                var backofficeUsers = await _backofficeUserService.GetAllBackofficeUsersAsync();

                if (backofficeUsers == null || !backofficeUsers.Any())
                    return NotFound(new { Message = "No users found in the backoffice." });

                var existingUsers = await _userService.GetAllUsersAsync();

                var importedCount = 0;
                foreach (var backofficeUser in backofficeUsers)
                {
                    // Skip if user already exists
                    if (existingUsers.Any(u => u.Username == backofficeUser.Username))
                        continue;

                    // Create a new user
                    var user = new User
                    {
                        Username = backofficeUser.Username,
                        Email = backofficeUser.Email,
                        PasswordHash = "default-hash",
                    };

                    // Save the user
                    var success = await _userService.CreateUserAsync(user);
                    if (success)
                        importedCount++;
                    else
                        Console.WriteLine($"Failed to import user {backofficeUser.Username}");
                }

                return Ok(new { Message = $"{importedCount} users imported successfully from backoffice." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while importing users.", Details = ex.Message });
            }
        }



        //[HttpPost("import-from-backoffice/{username}")]
        //public async Task<IActionResult> ImportFromBackoffice(string username)
        //{
        //    if (string.IsNullOrWhiteSpace(username))
        //        return BadRequest(new { Message = "Username is required." });

        //    try
        //    {
        //        var backofficeUser = await _backofficeUserService.GetBackofficeUserAsync(username);
        //        if (backofficeUser == null)
        //            return NotFound(new { Message = $"No user found with username '{username}' in backoffice." });

        //        var user = new User
        //        {
        //            Username = backofficeUser.Username,
        //            Email = backofficeUser.Email,
        //            PasswordHash = "default-hash",
        //        };

        //        var existingUsers = await _userService.GetAllUsersAsync();
        //        if (existingUsers.Any(u => u.Username == user.Username))
        //            return Conflict(new { Message = "User already exists in the database." });

        //        // Save the user to the database
        //        var success = await _userService.CreateUserAsync(user);
        //        if (!success)
        //            return StatusCode(500, new { Message = "Failed to save the user to the database." });

        //        return Ok(new { Message = "User imported successfully from backoffice." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = "An error occurred while importing the user.", Details = ex.Message });
        //    }
        //}
    }
}
