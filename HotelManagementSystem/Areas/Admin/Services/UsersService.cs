using HotelManagementSystem.Areas.Admin.Models.Users;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class UsersService : IUsersService
    {

        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UsersService(UserManager<User> uManager, RoleManager<IdentityRole> rManager)
        {
            this.userManager = uManager;
            this.roleManager = rManager;
        }

        public async Task<IEnumerable<ListUserViewModel>> All()
        {
            var allUsers = userManager
                .Users
                .Select(u => new ListUserViewModel
                {
                    Email = u.Email,
                    Id = u.Id
                })
                .ToList();

            foreach (var user in allUsers)
            {
                var curUser = await userManager.FindByIdAsync(user.Id);
                var curRole = await userManager.GetRolesAsync(curUser);

                user.Role = curRole.FirstOrDefault();
            }

            return allUsers;
        }

        public async Task ChangePassword(ChangePasswordUserFormModel user)
        {
            var curUser = await userManager.FindByIdAsync(user.Id);

            if(curUser.PasswordHash != null)
            {
                await userManager.RemovePasswordAsync(curUser);
            }

            await userManager.AddPasswordAsync(curUser, user.Password);
        }

        public async Task ChangeRole(ChangeUserRoleFormModel user)
        {
            var currentUser = await userManager.FindByIdAsync(user.Id);

            var currentRolesOnUser = await userManager.GetRolesAsync(currentUser);

            var newRole = await roleManager.FindByNameAsync(user.RoleName);

            if(newRole == null)
            {
                return;
            }

            await userManager.RemoveFromRolesAsync(currentUser, currentRolesOnUser);
            await userManager.AddToRoleAsync(currentUser, newRole.Name);
        }

        public async Task Delete(string id)
        {
            var curUser = await userManager.FindByIdAsync(id);

            if(curUser.Email == "admin@hotel.bg")
            {
                return;
            }

            await this.userManager.DeleteAsync(curUser);
        }

        public async Task<ChangePasswordUserFormModel> GetUser(string id)
        {
            var curUser = await userManager.FindByIdAsync(id);

            var uFormModel = new ChangePasswordUserFormModel
            {
                Email = curUser.Email,
                Id = curUser.Id,
            };

            return uFormModel;
        }

        public async Task<ChangeUserRoleFormModel> LoadRoles(string id)
        {
            var allRoles = this
                .roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            var currentUser = await userManager.FindByIdAsync(id);
            var allUSerRoles = await userManager.GetRolesAsync(currentUser);
            var userRole = allUSerRoles.FirstOrDefault();

            var uFormModel =  new ChangeUserRoleFormModel
            {
                Roles = allRoles,
                Email = currentUser.Email,
                Id = id,
                RoleName = userRole
            };

            return uFormModel;
        }

        public async Task<IdentityResult> Register(RegisterUserFormModel newUser)
        {
            var nUser = new User
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                UserName = newUser.Email
            };

            var result = await this.userManager.CreateAsync(nUser, newUser.Password);

            var currentUser = await this.userManager.FindByEmailAsync(newUser.Email);

            await this.userManager.AddToRoleAsync(currentUser, "user");

            return result;
        }

    }
}
