using System.Collections.Generic;

namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationsQueryModel
    {
        public ReservationsQueryModel()
        {
            this.Reservations = new List<ReservationsViewModel>();
        }

        public string Search { get; set; }

        public IEnumerable<ReservationsViewModel> Reservations;
    }
}
