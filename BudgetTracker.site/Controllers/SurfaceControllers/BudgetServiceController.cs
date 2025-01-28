using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

public class BudgetServiceController : SurfaceController
{
    private readonly BudgetService _budgetService;

    public BudgetServiceController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        BudgetService budgetService)
        : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _budgetService = budgetService;
    }

    public async Task<IActionResult> RenderBudgetData()
    {
        var budgets = await _budgetService.GetBudgetsAsync();
        return PartialView("BudgetPartial", budgets);
    }
}
