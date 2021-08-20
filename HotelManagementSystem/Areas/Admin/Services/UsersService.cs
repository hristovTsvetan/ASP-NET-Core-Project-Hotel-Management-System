using HotelManagementSystem.Areas.Admin.Models.Users;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;

        public UsersService(UserManager<User> uManager)
        {
            this.userManager = uManager;
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

            return result;
        }
    }
}
