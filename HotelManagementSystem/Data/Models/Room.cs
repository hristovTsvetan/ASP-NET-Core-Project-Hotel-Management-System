using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Room
    {
        public Room()
        {
            this.RoomReserveds = new HashSet<RoomReserved>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool HasAirCondition { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        public int RoomTypeId { get; set; }

        public virtual RoomType RoomType { get; set; }

        public virtual ICollection<RoomReserved> RoomReserveds { get; set; }


    }
}
