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
        public UserController(ApiService apiService,UserRepository userRepository)
        {
            _apiService = apiService;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Load()
        {
            var Users = await _apiService.GetUsersAsync();
            return Json(Users);
        }
        public async Task<IActionResult> Save(List<User> users)
        {
            await _userRepository.InsertUsers(users);
            return Ok();
        }
    }
}
