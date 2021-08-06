using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Validators.Messages;

namespace HotelManagementSystem.Models.Cities
{
    public class CitiesViewModel
    {
        [Required]
        [MinLength(5)]
        public string Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }
    }
}
