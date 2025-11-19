using ASP.NETTask.DTOs;
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
            var usersDTO = await _httpClient.GetFromJsonAsync<List<UserDTO>>(apiUrl);
            var users = usersDTO?.Select(dto => new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                Phone = dto.Phone,
                Website = dto.Website,
                Address =  new Address
                {
                    Street = dto.Address.Street,
                    Suite = dto.Address.Suite,
                    City = dto.Address.City,
                    Zipcode = dto.Address.Zipcode,
                    Lat = dto.Address.Geo.lat,
                    Lng = dto.Address.Geo.lng,
                },
                CreatedAt = DateTime.UtcNow
            }).ToList();

            return users ?? new List<User>();
        }
    }
}
