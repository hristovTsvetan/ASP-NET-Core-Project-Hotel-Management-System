using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.RoomsType
{
    public class ListRoomTypeViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        [Range(10, 500)]
        public decimal Price { get; set; }

        public int NumberOfBeds { get; set; }

        public string Image { get; set; }

        public int RoomsCount { get; set; }
    }
}
