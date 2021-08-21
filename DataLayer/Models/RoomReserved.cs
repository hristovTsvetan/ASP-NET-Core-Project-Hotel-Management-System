using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class RoomReserved
    {
        [Required]
        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        [Required]
        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }

    }
}
