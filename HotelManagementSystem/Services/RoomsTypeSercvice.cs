using HotelManagementSystem.Data;
using HotelManagementSystem.Models.RoomsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class RoomsTypeSercvice : IRoomsTypeSercvice
    {
        private readonly HotelManagementDbContext db;

        public RoomsTypeSercvice(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public EditRoomTypeFormModel GetRoomType(string id)
        {
            var a = this.db
                .RoomTypes
                .Where(t => t.Id == id)
                .Select(t => new EditRoomTypeFormModel
                {
                    Id = t.Id,
                    Image = t.Image,
                    Name = t.Name,
                    NumberOfBeds = t.NumberOfBeds,
                    Price = t.Price,
                }).FirstOrDefault();

            return a;
        }

        public IEnumerable<ListRoomTypeViewModel> ListTypes()
        {
            return this.db
                .RoomTypes
                .Where(t => t.Deleted == false)
                .Select(t => new ListRoomTypeViewModel
                {
                    Id = t.Id,
                    Image = t.Image,
                    Name = t.Name,
                    NumberOfBeds = t.NumberOfBeds,
                    Price = t.Price,
                    RoomsCount = t.Rooms.Count
                })
                .ToList();
        }

        public void Update(EditRoomTypeFormModel roomType)
        {
            var currentRoomType = this.db
                .RoomTypes
                .Where(r => r.Id == roomType.Id)
                .FirstOrDefault();

            currentRoomType.Image = roomType.Image;
            currentRoomType.Name = roomType.Name;
            currentRoomType.NumberOfBeds = roomType.NumberOfBeds;
            currentRoomType.Price = roomType.Price;

            this.db.RoomTypes.Update(currentRoomType);
            this.db.SaveChanges();
        }
    }
}
