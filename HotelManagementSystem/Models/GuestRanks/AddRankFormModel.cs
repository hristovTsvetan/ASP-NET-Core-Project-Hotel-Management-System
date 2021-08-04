using HotelManagementSystem.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.GuestRanks
{
    public class AddRankFormModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length is 3.")]
        [MaxLength(30)]
        [RankNameForAdd]
        public string Name { get; set; }

        [Range(1,50)]
        public int Discount { get; set; }
    }
}
