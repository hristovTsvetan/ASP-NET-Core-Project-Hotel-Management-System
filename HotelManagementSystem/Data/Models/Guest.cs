using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Guest
    {
        public Guest()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public string Details { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int RankId { get; set; }

        public virtual Rank Rank { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
