using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Reservations
{
    public class DetailsReservationViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string CreatedOn { get; set; }

        public int Duration { get; set; }

        public decimal? Price { get; set; }

        public string Status { get; set; }

        public string GuestName { get; set; }

        public string GuestIdentityCard { get; set; }

        public string VoucherName { get; set; }

        public string RoomReserveds { get; set; }

        public string InvoicePaid { get; set; }
    }
}
