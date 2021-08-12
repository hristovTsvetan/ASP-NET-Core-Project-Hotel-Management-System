using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class RoomsService : IRoomsService
    {
        private readonly HotelManagementDbContext db;

        public RoomsService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public IEnumerable<ListRoomsViewModel> All()
        {
            var activeHotel = this.GetActiveHotel();

            var allRooms = this.db
                .Rooms
                .Where(r => r.Deleted == false && r.Hotel == activeHotel)
                .Select(r => new ListRoomsViewModel
                {
                    RoomNumber = r.Number,
                    Id = r.Id,
                    RoomType = r.RoomType.Name
                });

            return allRooms;
        }

        public DetailsRoomViewModel Details(string id)
        {
            var activeHotel = this.GetActiveHotel();

            var currentRoom = this.db
                .Rooms
                .Where(r => r.Deleted == false && r.Id == id)
                .Select(r => new DetailsRoomViewModel
                {
                    Description = r.Description,
                    Floor = r.Floor,
                    HasAirCondition = r.HasAirCondition ? "Yes" : "No",
                    HotelName = activeHotel.Name,
                    Id = r.Id,
                    Number = r.Number,
                    RoomType = r.RoomType.Name
                })
                .FirstOrDefault();

            return currentRoom;
        }

        Hotel GetActiveHotel()
        {
            return this.db
                .Hotels
                .Where(h => h.Active == true)
                .FirstOrDefault();
        }
    }
}
