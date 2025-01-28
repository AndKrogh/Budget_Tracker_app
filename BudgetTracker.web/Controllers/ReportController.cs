using BudgetTracker.core.Services;
using BudgetTracker.web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("report/{budgetId}/{userId}")]
        public async Task<IActionResult> GenerateReportAsync(int budgetId, int userId)
        {
            var report = await _reportService.GenerateReportAsync(budgetId, userId);
            if (report == null)
            {
                return NotFound(new { Message = $"Report with budgetId {budgetId} and userId {userId} not able to be generated" });
            }

            var reportDto = new ExpenseReportDto
            {
                BudgetId = report.BudgetId,
                UserId = report.UserId,
                ReportName = report.ReportName,
                TotalIncome = report.TotalIncome,
                TotalExpenses = report.TotalExpenses,
                NetSavings = report.TotalIncome - report.TotalExpenses
            };

            return Ok(reportDto);
        }

        [HttpGet("forecast/{budgetId}/{userId}")]
        public async Task<IActionResult> GetExpenseForecastAsync(int budgetId, int userId)
        {
            var forecast = await _reportService.GetExpenseForecastAsync(budgetId, userId);
            if (forecast == null)
            {
                return NotFound(new { Message = $"Expense forecast with budgetId {budgetId} and userId {userId} not found." });
            }

            var forecastDto = new ExpenseForecastDto
            {
                ForecastName = forecast.ForecastName,
                ForecastDate = forecast.ForecastDate,
                PredictedAmount = forecast.PredictedAmount,
                Notes = forecast.Notes,
                BudgetId = forecast.BudgetId,
                UserId = forecast.UserId
            };

            return Ok(forecastDto);
        }
    }
}
