using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Models.Hotels
{
    public class HotelViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string CityName { get; set; }

        public string Address { get; set; }

        public string Image { get; set; }

        public string IsActive { get; set; }

        public int RoomCount { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
    }
}
