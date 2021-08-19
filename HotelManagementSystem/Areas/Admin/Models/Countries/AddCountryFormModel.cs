using HotelManagementSystem.Areas.Admin.Validators;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Countries
{
    public class AddCountryFormModel
    {
        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [CountryNameAdd]
        public string Name { get; set; }
    }
}
