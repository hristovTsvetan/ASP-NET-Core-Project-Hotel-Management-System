using HotelManagementSystem.Areas.Admin.Models.Users;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService uService;

        public UsersController(IUsersService userService)
        {
            this.uService = userService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return this.View(user);
            }

            var result = await this.uService.Register(user);

            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                foreach (var err in errors)
                {
                    ModelState.AddModelError(string.Empty, err);
                }

                return this.View(user);
            }

            return this.View();
        }

    }
}
