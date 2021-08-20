using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.LoginUsers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class LoginUsersService : ILoginUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public LoginUsersService(UserManager<User> uManager, SignInManager<User> sInManager)
        {
            this.userManager = uManager;
            this.signInManager = sInManager;
        }

        public async Task Login(User user)
        {
            await this.signInManager.SignInAsync(user, true);
        }

        public async Task<User> IsUserExist(LoginUsersFormModel user)
        {
            return await this.userManager.FindByEmailAsync(user.Email);
        }

        public async Task<bool> IsPasswordCorrect(User user, LoginUsersFormModel userFormModel)
        {
            return await this.userManager.CheckPasswordAsync(user, userFormModel.Password);
        }

        public async Task LogOut()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
