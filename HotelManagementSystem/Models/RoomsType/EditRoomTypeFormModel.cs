using HotelManagementSystem.Validators;
using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.RoomsType
{
    public class EditRoomTypeFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [RoomTypeNameForEdit]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(10, 500)]
        public decimal Price { get; set; }

        [Range(1,10)]
        [Display(Name = "Number of beds")]
        public int NumberOfBeds { get; set; }

        [Url]
        public string Image { get; set; }
    }
}
