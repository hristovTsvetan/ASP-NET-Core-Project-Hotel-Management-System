using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Rooms
{
    public class ListRoomsQueryModel
    {
        public ListRoomsQueryModel()
        {
            this.Rooms = new List<ListRoomsViewModel>();
            this.CurrentPage = 1;
            this.ItemsOnPage = 10;
        }

        public int CurrentPage { get; set; }

        public int NextPage { get; set; }

        public int PreviousPage { get; set; }

        public int TotalItems { get; set; }

        public int ItemsOnPage { get; set; }

        public string Search { get; set; }

        public IEnumerable<ListRoomsViewModel> Rooms { get; set; }
    }
}
