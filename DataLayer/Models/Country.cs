using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models
{
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
