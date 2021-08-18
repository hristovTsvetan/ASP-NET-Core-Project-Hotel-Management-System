using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationsViewModel
    {
        public DateTime CreatedOn { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Status { get; set; }

        public string Id { get; set; }
    }
}
