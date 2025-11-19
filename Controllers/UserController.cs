using System.Net.Http;
using ASP.NETTask.Models;
using ASP.NETTask.Service;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETTask.Controllers
{
    public class UserController : Controller
    {
        private readonly ApiService _apiService;
        public UserController(ApiService apiService)
        {
            _apiService = apiService;
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
    }
}
