using System.Net.Http;
using ASP.NETTask.Models;
using ASP.NETTask.Repositories;
using ASP.NETTask.Service;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETTask.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiService _apiService;
        private readonly UserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        public UserController(ApiService apiService,UserRepository userRepository, ILogger<HomeController> logger, UserService userService)
        {
            _apiService = apiService;
            _userRepository = userRepository;
            _logger = logger;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult All(List<UserViewModel> users)
        {
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> Load()
        {
            try
            {
                var usersList = await _apiService.GetUsersAsync();
                var model = _userService.MapUsersToListViewModel(usersList);
                return View("All", model);
            }
            catch(HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching users from external API.");
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return StatusCode(500, "Error fetching users from external API.");
            }
        }
        public async Task<IActionResult> Save(List<User> users)
        {
            await _userRepository.InsertUsers(users);
            return Ok();
        }
    }
}
