﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Hotel
    {
        public Hotel()
        {
            this.Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }

        public int CityId { get; set; }

        public virtual City City { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}