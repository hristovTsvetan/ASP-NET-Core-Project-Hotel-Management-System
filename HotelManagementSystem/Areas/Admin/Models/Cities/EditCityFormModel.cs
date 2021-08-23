using HotelManagementSystem.Areas.Admin.Validators;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Cities
{
    public class EditCityFormModel
    {
        [Required]
        [IsCityNameExistWhenEdit]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        public string Id { get; set; }
    }
}
