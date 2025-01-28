public class BudgetDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
}

public class BudgetService
{
    private readonly HttpClient _httpClient;

    public BudgetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BudgetDto>> GetBudgetsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<BudgetDto>>("https://localhost:7151/api/Budget")
               ?? new List<BudgetDto>();
    }
}
