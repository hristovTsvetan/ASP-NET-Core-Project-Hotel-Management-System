using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.LoginUsers;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface ILoginUsersService
    {
        Task Login(User user);

        Task<User> IsUserExist(LoginUsersFormModel user);

        Task<bool> IsPasswordCorrect(User user, LoginUsersFormModel userFormModel);

        Task LogOut();
    }
}
