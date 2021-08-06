using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Room
    {
        public Room()
        {
            this.RoomReserveds = new HashSet<RoomReserved>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Number { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public bool HasAirCondition { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        [Required]
        public string RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual ICollection<RoomReserved> RoomReserveds { get; set; }


    }
}
