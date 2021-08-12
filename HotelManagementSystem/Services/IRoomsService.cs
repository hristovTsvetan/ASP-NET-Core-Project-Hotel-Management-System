using HotelManagementSystem.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IRoomsService
    {
        IEnumerable<ListRoomsViewModel> All();

        DetailsRoomViewModel Details(string id);
    }
}
