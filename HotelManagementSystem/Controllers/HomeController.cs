using DataLayer.Models;
using HotelManagementSystem.Models;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<User> user;
        private readonly IHomeService hService;

        public HomeController(IHomeService homeService, UserManager<User> uManager)
        {
            this.user = uManager;
            this.hService = homeService;
        }

        public IActionResult Home()
        {
            var users = user.Users;

            var currentDashboard = this.hService.GetDashboardInfo();

            return View(currentDashboard);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
