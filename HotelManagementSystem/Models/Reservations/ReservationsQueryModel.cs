using System.Collections.Generic;

namespace HotelManagementSystem.Models.Reservations
{
    public class ReservationsQueryModel
    {
        public ReservationsQueryModel()
        {
            this.Reservations = new List<ReservationsViewModel>();
            this.ItemsPerPage = 10;
            this.CurrentPage = 1;
        }

        public string Search { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<ReservationsViewModel> Reservations;
    }
}
