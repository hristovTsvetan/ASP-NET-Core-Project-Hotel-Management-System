﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
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
