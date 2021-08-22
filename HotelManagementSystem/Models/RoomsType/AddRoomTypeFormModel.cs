using HotelManagementSystem.Validators;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models.RoomsType
{
    public class AddRoomTypeFormModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [RoomTypeNameForAdd]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(10, 500)]
        public decimal Price { get; set; }

        [Range(1, 10)]
        [Display(Name ="Number of beds")]
        public int NumberOfBeds { get; set; }

        [Url]
        public string Image { get; set; }

    }
}
