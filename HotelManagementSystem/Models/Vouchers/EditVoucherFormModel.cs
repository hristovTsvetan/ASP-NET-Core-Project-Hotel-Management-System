using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Vouchers
{
    public class EditVoucherFormModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Max length should be maximum {1} symbols!")]
        [MinLength(5, ErrorMessage = "Min length should be minimum {1} symbols!")]
        public string Name { get; set; }

        [Range(1, 50)]
        public int Discount { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
