using HotelManagementSystem.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class Rank
    {
        public Rank()
        {
            this.Guests = new HashSet<Guest>();
        }

        public int Id { get; set; }

        public Ranks Name { get; set; }

        public int Discount { get; set; }

        public ICollection<Guest> Guests { get; set; }
    }
}
