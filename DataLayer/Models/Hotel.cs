using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Hotel
    {
        public Hotel()
        {
            this.Rooms = new HashSet<Room>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public string Description { get; set; }

        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        public string Image { get; set; }

        [Required]
        public string CityId { get; set; }

        public virtual City City { get; set; }

        public bool Deleted { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
