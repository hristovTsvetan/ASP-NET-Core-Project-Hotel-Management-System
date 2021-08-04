using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Voucher
    {
        public Voucher()
        {
            this.Reservations = new HashSet<Reservation>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int Discount { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
