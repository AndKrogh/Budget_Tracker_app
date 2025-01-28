using BudgetTracker.core.Services;
using BudgetTracker.web.Dtos;
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
            var budgetEntries = await _expenseService.GetExpensesByBudgetIdAsync(id);

            if (budgetEntries == null || !budgetEntries.Any())
            {
                return NotFound(new { Message = $"Budget entries with ID {id} not found." });
            }

            var dtos = budgetEntries.Select(budgetEntry => new BudgetEntryDto
            {
                Id = budgetEntry.Id,
                UserId = budgetEntry.UserId,
                Date = budgetEntry.Date,
                Category = budgetEntry.Category,
                Amount = budgetEntry.Amount,
                Description = budgetEntry.Description,
                BudgetId = budgetEntry.BudgetId
            }).ToList();

            return Ok(dtos);
        }

    }
}
