using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Company
    {
        public Company()
        {
            this.Hotels = new HashSet<Hotel>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(30)]
        public string Phone { get; set; }

        [Required]
        public string CityId { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
