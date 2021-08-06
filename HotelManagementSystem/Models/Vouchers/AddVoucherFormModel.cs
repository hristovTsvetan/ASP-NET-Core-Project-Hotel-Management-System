using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Vouchers
{
    public class AddVoucherFormModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(2, ErrorMessage = ValidatorConstants.minLength)]
        public string Name { get; init; }

        [Range(1, 50)]
        public int Discount { get; init; }
    }
}
