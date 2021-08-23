using HotelManagementSystem.Validators.Messages;
using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Vouchers
{
    public class EditVoucherFormModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(2, ErrorMessage = ValidatorConstants.minLength)]
        public string Name { get; set; }

        [Range(1, 50)]
        public int Discount { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
