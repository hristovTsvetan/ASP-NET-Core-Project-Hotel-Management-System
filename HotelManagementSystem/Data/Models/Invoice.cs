using HotelManagementSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Invoice
    {
        public Invoice()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        public InvoiceStatus Status { get; set; }

        [Required]
        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        public decimal Amount { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool Paid { get; set; }
    }
}
