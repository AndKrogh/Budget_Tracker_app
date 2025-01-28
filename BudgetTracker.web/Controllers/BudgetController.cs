using BudgetTracker.core.Models;
using BudgetTracker.core.Services;
using BudgetTracker.web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetById(int id)
        {
            var budget = await _budgetService.GetBudgetByIdAsync(id);
            if (budget == null)
            {
                return NotFound(new { Message = $"Budget with ID {id} not found." });
            }

            var budgetDto = new BudgetDto
            {
                Id = budget.Id,
                UserId = budget.UserId,
                Name = budget.Name,
                TotalIncome = budget.BudgetEntries.Where(e => e.Amount > 0).Sum(e => e.Amount),
                TotalExpenses = budget.BudgetEntries.Where(e => e.Amount < 0).Sum(e => Math.Abs(e.Amount))
            };

            return Ok(budgetDto);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBudgetsForUser(int userId)
        {
            var budgets = await _budgetService.GetBudgetsForUserAsync(userId);

            if (budgets == null)
            {
                return NotFound(new { Message = $"No budgets found for user with ID {userId}." });
            }

            var budgetDtos = budgets.Select(b => new BudgetDto
            {
                Id = b.Id,
                UserId = b.UserId,
                Name = b.Name,
                TotalIncome = b.BudgetEntries?.Where(e => e.Amount > 0).Sum(e => e.Amount) ?? 0,
                TotalExpenses = b.BudgetEntries?.Where(e => e.Amount < 0).Sum(e => Math.Abs(e.Amount)) ?? 0
            }).ToList();

            return Ok(budgetDtos);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] Budget budget)
        {
            if (budget == null || budget.UserId <= 0 || string.IsNullOrWhiteSpace(budget.Name))
                return BadRequest(new { Message = "Invalid budget data." });

            var success = await _budgetService.CreateBudgetAsync(budget);
            if (!success)
                return StatusCode(500, new { Message = "Failed to create budget." });

            return Ok(new { Message = "Budget created successfully.", BudgetId = budget.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] Budget budget)
        {
            if (budget == null || budget.Id != id)
                return BadRequest(new { Message = "Budget ID mismatch or invalid data." });

            var success = await _budgetService.UpdateBudgetAsync(budget);
            if (!success)
                return StatusCode(500, new { Message = "Failed to update budget." });

            return Ok(new { Message = "Budget updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var success = await _budgetService.DeleteBudgetAsync(id);
            if (!success)
                return StatusCode(500, new { Message = "Failed to delete budget." });

            return Ok(new { Message = "Budget deleted successfully." });
        }
    }
}
