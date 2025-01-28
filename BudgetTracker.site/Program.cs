WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add Umbraco services
builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddComposers()
    .Build();

// Add controllers
builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<BudgetService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7151/");
});

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

// Map API controllers
app.MapControllers();

await app.RunAsync();
