using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class City
    {
        public City()
        {
            this.Companies = new HashSet<Company>();
            this.Guests = new HashSet<Guest>();
            this.Hotels = new HashSet<Hotel>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(30)]
        [Required]
        public string PostalCode { get; set; }
        
        [Required]
        public string CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
