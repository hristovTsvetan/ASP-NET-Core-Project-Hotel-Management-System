using DataLayer;
using DataLayer.Models;
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
        private readonly IReservationsService resService;

        public RoomsService(HotelManagementDbContext dBase, IReservationsService reservationServ)
        {
            this.db = dBase;
            this.resService = reservationServ;
        }

        public ListRoomsQueryModel All(ListRoomsQueryModel rooms)
        {
            var activeHotel = this.GetActiveHotel();

            var allRoomsDb = this.db.Rooms
                .Where(r => r.Deleted == false && r.Hotel == activeHotel)
                .OrderBy(r => r.Floor)
                .ThenBy(r => r.Number)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(rooms.Search))
            {
                allRoomsDb = allRoomsDb
                    .Where(r => r.Number.ToLower().Contains(rooms.Search.ToLower()) ||
                    r.Description.ToLower().Contains(rooms.Search.ToLower()) ||
                    r.Hotel.Name.ToLower().Contains(rooms.Search.ToLower()) ||
                    r.RoomType.Name.ToLower().Contains(rooms.Search.ToLower()));
            }

            var tPages = (int)Math.Ceiling((double)allRoomsDb.Count() / rooms.ItemsOnPage);

            if(rooms.CurrentPage > tPages)
            {
                rooms.CurrentPage = tPages;
            }

            if(rooms.CurrentPage <= 0)
            {
                rooms.CurrentPage = 1;
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
                TotalItems = tPages,
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

        public EditRoomFormModel Edit(string id)
        {
            var allRoomTypes = GetRoomTypes();

            var currentRoom = GetRoom(id);

            var roomForEdit = new EditRoomFormModel
            {
                Description = currentRoom.Description,
                Floor = currentRoom.Floor,
                HasAirCondition = currentRoom.HasAirCondition ? "Yes" : "No",
                Id = currentRoom.Id,
                Number = currentRoom.Number,
                RoomTypes = allRoomTypes,
                CurrentRoomTypeId = currentRoom.RoomTypeId
            };

            return roomForEdit;
        }

        private Room GetRoom(string id)
        {
            return this.db
                .Rooms
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<RoomTypeViewModel> GetRoomTypes()
        {
            return this.db
                .RoomTypes
                .Where(rt => rt.Deleted == false)
                .Select(rt => new RoomTypeViewModel
                {
                    Id = rt.Id,
                    Name = rt.Name
                })
                .ToList();
        }

        public bool GetRoomNameForEdit(string name, string id)
        {
            var activeHotel = GetActiveHotel();

            return this.db
                .Rooms
                .Where(r => r.Id != id && r.Deleted == false && r.HotelId == activeHotel.Id)
                .Any(r => r.Number == name);
        }

        public bool GetRoomNameForAdd(string name)
        {
            var activeHotel = GetActiveHotel();

            return this.db
                .Rooms
                .Where(r => r.Deleted == false && r.HotelId == activeHotel.Id)
                .Any(r => r.Number == name);
        }

        Hotel GetActiveHotel()
        {
            return this.db
                .Hotels
                .Where(h => h.Active == true)
                .FirstOrDefault();
        }

        public async Task Update(EditRoomFormModel room)
        {
            var currentRoom = this.GetRoom(room.Id);
            var currentHotel = this.GetActiveHotel();

            currentRoom.Description = room.Description;
            currentRoom.Floor = room.Floor;
            currentRoom.HasAirCondition = room.HasAirCondition == "Yes" ? true : false;
            currentRoom.Number = room.Number;
            currentRoom.RoomTypeId = room.CurrentRoomTypeId;
            currentRoom.Hotel = currentHotel;

            this.db.Rooms.Update(currentRoom);
            await this.db.SaveChangesAsync();
         
        }

        public async Task Add(AddRoomFormModel room)
        {
            var newRoom = new Room
            {
                Deleted = false,
                Description = room.Description,
                Floor = room.Floor,
                HasAirCondition = room.HasAirCondition == "Yes" ? true : false,
                HotelId = room.HotelId,
                Number = room.Number,
                RoomTypeId = room.RoomTypeId,
            };

            await this.db.Rooms.AddAsync(newRoom);
            await this.db.SaveChangesAsync();
        }

        public AddRoomFormModel FillRoomAddForm()
        {
            var currentHotel = this.GetActiveHotel();
            var roomTypes = this.GetRoomTypes();

            var room = new AddRoomFormModel
            {
                HotelName = currentHotel.Name,
                HotelId = currentHotel.Id,
                RoomTypes = roomTypes
            };

            return room;
        }

        public async Task Delete(string id)
        {
            var room = this.db
                .Rooms
                .FirstOrDefault(r => r.Id == id);

            room.Deleted = true;

            await this.resService.CancelReservation(id);

            this.db.Rooms.Update(room);
            await this.db.SaveChangesAsync();
        }
    }
}
