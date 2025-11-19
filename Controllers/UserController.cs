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
        [HttpPost]
        public async Task<IActionResult> All()
        {
            try
            {
                var usersList = await _apiService.GetUsersAsync();
                var model = _userService.MapUsersToListViewModel(usersList);
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error fetching users from external API.");
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return StatusCode(500, "Error fetching users from external API.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Save(List<User> users)
        {
            try
            {
                await _userRepository.InsertUsers(users);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error saving users to the database.");
                Console.WriteLine($"Error saving users: {ex.Message}");
                return StatusCode(500, "Error saving users to the database.");
            }
        }
    }
}
