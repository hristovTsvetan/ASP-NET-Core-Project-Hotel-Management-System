using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Guests
{
    public class ListGuestsQueryModel
    {
        public ListGuestsQueryModel()
        {
            this.AllGuests = new List<ListGuestsViewModel>();
            this.CurrentPage = 1;
            this.ItemsPerPage = 10;
        }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int ItemsPerPage { get; set; }

        public SortBy SortBy { get; set; }

        public int AscOrDesc { get; set; }

        public string Search { get; set; }

        public IEnumerable<ListGuestsViewModel> AllGuests { get; set; }
    }
}
