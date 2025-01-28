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

        [HttpGet]
        public async Task<IActionResult> GetAllBudgets()
        {
            var budgets = await _budgetService.GetAllBudgetsAsync();

            if (budgets == null || !budgets.Any())
            {
                return NotFound(new { Message = "No budgets found." });
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
    }
}
