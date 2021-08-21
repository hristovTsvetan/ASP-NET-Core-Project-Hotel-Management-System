using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Users
{
    public class ChangePasswordUserFormModel
    {
        public string Id { get; set; }

        [Display(Name = "Current user: ")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Password { get; set; }
    }
}
