using BudgetTracker.core.Services;
using Microsoft.AspNetCore.Mvc;


namespace BudgetTracker.web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpensesByBudgetIdAsync(int id)
        {
            var expense = await _expenseService.GetExpensesByBudgetIdAsync(id);
            if (expense == null)
            {
                return NotFound(new { Message = $"expense with id {id} not found" });
            }

            return Ok(expense);
        }
    }
}
