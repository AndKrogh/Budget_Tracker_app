using Newtonsoft.Json;

namespace BudgetTracker.web.Services
{
    public class BackofficeUserService
    {
        private readonly HttpClient _httpClient;

        public BackofficeUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Method to fetch all backoffice members
        public async Task<IEnumerable<MemberDto>> GetAllBackofficeUsersAsync()
        {
            var response = await _httpClient.GetAsync("http://site-url/api/members");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<MemberDto>>(content);
        }


        // Method to fetch a backoffice member by username
        //public async Task<MemberDto?> GetBackofficeUserAsync(string username)
        //{
        //    var response = await _httpClient.GetAsync($"api/members/{username}");
        //    if (!response.IsSuccessStatusCode)
        //        return null;

        //    var content = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<MemberDto>(content);
        //}
    }

    // DTO for member data
    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
