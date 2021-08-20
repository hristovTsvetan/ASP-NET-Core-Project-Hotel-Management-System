using HotelManagementSystem.Areas.Admin.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IUsersService
    {
        Task<IdentityResult> Register(RegisterUserFormModel user);

    }
}
