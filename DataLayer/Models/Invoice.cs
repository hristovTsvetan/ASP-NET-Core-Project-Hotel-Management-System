using DataLayer.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
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

        public string ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Amount { get; set; }

        public DateTime IssuedDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public bool Paid { get; set; }
    }
}
