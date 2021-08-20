using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.LoginUsers
{
    public class LoginUsersFormModel
    {
        [EmailAddress]
        [Required]
        [MinLength(5, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Password { get; set; }
    }
}
