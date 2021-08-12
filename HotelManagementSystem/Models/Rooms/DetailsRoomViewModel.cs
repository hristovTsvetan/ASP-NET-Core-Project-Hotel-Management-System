using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Rooms
{
    public class DetailsRoomViewModel
    {
        public string Id { get; set; }

        public string Number { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        public string HasAirCondition { get; set; }

        public string HotelName { get; set; }

        public string RoomType { get; set; }
    }
}
