using System.Text.Json;

namespace YourWebProject.Services
{
    public class BackofficeUserService
    {
        private readonly HttpClient _httpClient;

        public BackofficeUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BackofficeUserDto> GetBackofficeUserAsync(string username)
        {
            var response = await _httpClient.GetAsync($"https://your-site-project.com/api/backoffice-users/{username}");
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch user: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BackofficeUserDto>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public class BackofficeUserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<string> UserGroups { get; set; }
    }
}
