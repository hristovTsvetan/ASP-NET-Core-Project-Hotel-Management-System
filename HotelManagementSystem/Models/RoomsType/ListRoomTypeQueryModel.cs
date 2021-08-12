using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.RoomsType
{
    public class ListRoomTypeQueryModel
    {
        public ListRoomTypeQueryModel()
        {
            this.RoomTypes = new List<ListRoomTypeViewModel>();
            this.ItemsPerPage = 3;
            this.CurrentPage = 1;
        }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<ListRoomTypeViewModel> RoomTypes { get; set; }
    }
}
