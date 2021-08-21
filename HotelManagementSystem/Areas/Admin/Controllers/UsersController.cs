using HotelManagementSystem.Areas.Admin.Models.Users;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> All()
        {
            var users = await this.uService.All();

            return this.View(users);
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var queryUser = await this.uService.GetUser(id);

            return this.View(queryUser);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordUserFormModel user)
        {
            if(!ModelState.IsValid)
            {
                return this.View(user);
            }

            await this.uService.ChangePassword(user);

            return this.RedirectToAction("All", "Users"); 
        }

        public async Task<IActionResult> ChangeRole(string id)
        {
            var allRoles = await this.uService.LoadRoles(id);

            return this.View(allRoles);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(ChangeUserRoleFormModel user)
        {
            if(!ModelState.IsValid)
            {
                return this.View(user);
            }

            await this.uService.ChangeRole(user);

            return RedirectToAction("All", "Users");
        }


        public async Task<IActionResult> Delete(string id)
        {
            await this.uService.Delete(id);

            return this.RedirectToAction("All", "Users");
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

            return this.RedirectToAction("All", "Users");
        }
    }
}
