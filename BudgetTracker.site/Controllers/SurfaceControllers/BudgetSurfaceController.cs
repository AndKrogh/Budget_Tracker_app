using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

public class BudgetSurfaceController : SurfaceController
{
    private readonly BudgetService _budgetService;

    public BudgetSurfaceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory umbracoDatabaseFactory,
        ServiceContext serviceContext,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        BudgetService budgetService)
        : base(umbracoContextAccessor, umbracoDatabaseFactory, serviceContext, appCaches, profilingLogger, publishedUrlProvider)
    {
        _budgetService = budgetService;
    }

    [HttpGet]
    public async Task<IActionResult> RenderBudgetData()
    {
        var budgets = await _budgetService.GetAllBudgetsAsync();

        ViewData["budgets"] = budgets;

        return View("BudgetView");
    }
}
