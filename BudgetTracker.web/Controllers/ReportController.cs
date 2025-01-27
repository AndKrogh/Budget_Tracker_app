using BudgetTracker.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private ReportService _reportService;

        public ReportController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GenerateReportAsync(int budgetId, int userId)
        {
            var report = await _reportService.GenerateReportAsync(budgetId, userId);
            if (report == null)
            {
                return NotFound(new { Message = $"Report with budgetId {budgetId} and userId {userId} not able to be generated");

            }
            return Ok(report);
        }
    }
}
