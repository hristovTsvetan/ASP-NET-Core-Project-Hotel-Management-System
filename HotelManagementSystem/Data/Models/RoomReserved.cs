using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class RoomReserved
    {
        [Required]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        [Required]
        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

    }
}
