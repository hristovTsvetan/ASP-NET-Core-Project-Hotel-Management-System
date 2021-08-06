using System;
using System.ComponentModel.DataAnnotations;
using HotelManagementSystem.Validators;
using HotelManagementSystem.Validators.Messages;

namespace HotelManagementSystem.Models.GuestRanks
{
    public class AddRankFormModel
    {
        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [RankNameForAdd]
        public string Name { get; set; }

        [Range(1,50)]
        public int Discount { get; set; }
    }
}
