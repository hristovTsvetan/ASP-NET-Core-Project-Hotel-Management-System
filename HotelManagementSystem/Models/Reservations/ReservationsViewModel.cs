using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationsViewModel
    {
        public ReservationsViewModel()
        {
            this.RoomName = new List<string>();
        }

        public IEnumerable<string> RoomName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string GuestName { get; set; }

        public string Id { get; set; }
    }
}
