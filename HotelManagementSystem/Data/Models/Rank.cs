using HotelManagementSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Rank
    {
        public Rank()
        {
            this.Guests = new HashSet<Guest>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public int Discount { get; set; }

        public bool Deleted { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
