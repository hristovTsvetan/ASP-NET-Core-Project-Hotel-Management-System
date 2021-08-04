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
        [MaxLength(50, ErrorMessage = "Max length should be maximum {1} symbols!")]
        [MinLength(5, ErrorMessage ="Min length should be minimum {1} symbols!")]
        public string Name { get; init; }

        [Range(1, 50)]
        public int Discount { get; init; }
    }
}
