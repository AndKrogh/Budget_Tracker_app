using BudgetTracker.core.Models;
using BudgetTracker.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
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
    }
}
