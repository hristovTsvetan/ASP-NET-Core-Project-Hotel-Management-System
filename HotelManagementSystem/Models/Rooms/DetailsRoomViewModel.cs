using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Rooms
{
    public class DetailsRoomViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Room name")]
        public string Number { get; set; }

        public int Floor { get; set; }

        public string Description { get; set; }

        [Display(Name = "Has air condition")]
        public string HasAirCondition { get; set; }

        [Display(Name = "Hotel name")]
        public string HotelName { get; set; }

        [Display(Name = "Room type")]
        public string RoomType { get; set; }
    }
}
