using BudgetTracker.core.Repositories;
using BudgetTracker.core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetTracker.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<BudgetRepository>();
            services.AddScoped<UserRepository>();

            services.AddScoped<BudgetService>();
            services.AddScoped<ExpenseService>();
            services.AddScoped<ReportService>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}
