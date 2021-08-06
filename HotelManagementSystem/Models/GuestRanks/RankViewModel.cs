using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Validators.Messages;

namespace HotelManagementSystem.Models.GuestRanks
{
    public class RankViewModel
    {
        [Required]
        [MinLength(5)]
        public string Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }
    }
}
