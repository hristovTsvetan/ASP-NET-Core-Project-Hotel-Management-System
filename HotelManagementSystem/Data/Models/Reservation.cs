using HotelManagementSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Reservation
    {
        public Reservation()
        {
            this.RoomReserveds = new HashSet<RoomReserved>();
        }

        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Duration { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MaxLength(50)]
        public string Creator { get; set; }

        public ReservationStatus Status { get; set; }

        public int GuestId { get; set; }

        public virtual Guest Guest { get; set; }

        public int? VoucherId { get; set; }

        public virtual Voucher Voucher { get; set; }

        public virtual ICollection<RoomReserved> RoomReserveds { get; set; }

        public int InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
