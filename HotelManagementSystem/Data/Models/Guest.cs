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
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string IdentityCardId { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        public string Details { get; set; }

        public bool Deleted { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public string CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public string RankId { get; set; }

        public virtual Rank Rank { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
