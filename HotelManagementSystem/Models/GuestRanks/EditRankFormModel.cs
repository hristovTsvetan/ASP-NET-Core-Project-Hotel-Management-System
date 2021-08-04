using HotelManagementSystem.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.GuestRanks
{
    public class EditRankFormModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length is 3.")]
        [MaxLength(30)]
        [RankNameForEdit]
        public string Name { get; set; }

        [Range(1, 50)]
        public int Discount { get; set; }
    }
}
