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
    }
}
