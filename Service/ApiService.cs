using ASP.NETTask.Models;

namespace ASP.NETTask.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<User>> GetUsersAsync()
        {
            string apiUrl = "https://jsonplaceholder.typicode.com/users";
            var users = await _httpClient.GetFromJsonAsync<List<User>>(apiUrl);
            return users ?? new List<User>();
        }
    }
}
