using BudgetTracker.web.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

public class BudgetService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<BudgetService> _logger;

    public BudgetService(HttpClient httpClient, ILogger<BudgetService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<List<BudgetDto>> GetAllBudgetsAsync()
    {
        var apiUrl = "https://localhost:7151/api/Budget";

        try
        {
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var budgets = JsonSerializer.Deserialize<List<BudgetDto>>(jsonData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                });

                return budgets ?? new List<BudgetDto>();
            }
            else
            {
                _logger.LogWarning($"API request failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching budget data");
        }

        return new List<BudgetDto>();
    }
}
