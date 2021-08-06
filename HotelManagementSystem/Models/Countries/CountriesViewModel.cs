using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Validators.Messages;

namespace HotelManagementSystem.Models.Countries
{
    public class CountriesViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        public string Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }
    }
}
