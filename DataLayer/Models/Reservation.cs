using DataLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class Reservation
    {
        public Reservation()
        {
            this.RoomReserveds = new HashSet<RoomReserved>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Duration { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        public string Creator { get; set; }

        public ReservationStatus Status { get; set; }

        public string GuestId { get; set; }

        public virtual Guest Guest { get; set; }

        public string VoucherId { get; set; }

        public virtual Voucher Voucher { get; set; }

        public virtual ICollection<RoomReserved> RoomReserveds { get; set; }

        //public string InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
