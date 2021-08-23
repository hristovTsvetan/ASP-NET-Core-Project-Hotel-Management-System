using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    [Authorize]
    public class AboutUsController : Controller
    {
        private readonly IAboutUs aboutService;

        public AboutUsController(IAboutUs abService)
        {
            this.aboutService = abService;
        }

        public IActionResult AboutUs()
        {
            var aboutUs = this.aboutService.AboutUsData();

            return this.View(aboutUs);
        }
    }
}
