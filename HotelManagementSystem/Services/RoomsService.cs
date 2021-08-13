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

        public ListRoomsQueryModel All(ListRoomsQueryModel rooms)
        {
            var activeHotel = this.GetActiveHotel();

            var allRoomsDb = this.db.Rooms
                .Where(r => r.Deleted == false && r.Hotel == activeHotel)
                .OrderBy(r => r.Number)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(rooms.Search))
            {
                allRoomsDb = allRoomsDb
                    .Where(r => r.Number.Contains(rooms.Search) ||
                    r.Description.Contains(rooms.Search) ||
                    r.Hotel.Name.Contains(rooms.Search) ||
                    r.RoomType.Name.Contains(rooms.Search));
            }

            var allRooms = allRoomsDb
                .Skip((rooms.CurrentPage - 1) * rooms.ItemsOnPage)
                .Take(rooms.ItemsOnPage)
                .Select(r => new ListRoomsViewModel
                {
                    RoomNumber = r.Number,
                    Id = r.Id,
                    RoomType = r.RoomType.Name
                })
                .ToList();

            var roomQueryModel = new ListRoomsQueryModel
            {
                Rooms = allRooms,
                TotalItems = (int)Math.Ceiling((double)allRoomsDb.Count() / rooms.ItemsOnPage),
                CurrentPage = rooms.CurrentPage,
                PreviousPage = rooms.PreviousPage,
                NextPage = rooms.NextPage
            };

            

            return roomQueryModel;
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
