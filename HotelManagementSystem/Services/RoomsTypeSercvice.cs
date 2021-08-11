using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
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

        public void Add(AddRoomTypeFormModel roomType)
        {
            var rType = new RoomType
            {
                Image = roomType.Image,
                Name = roomType.Name,
                NumberOfBeds = roomType.NumberOfBeds,
                Price = roomType.Price,
            };

            this.db.RoomTypes.Add(rType);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var rTypeForDelete = this.db.RoomTypes.FirstOrDefault(rt => rt.Id == id);

            this.db.RoomTypes.Remove(rTypeForDelete);
            this.db.SaveChanges();

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

        public bool IsRoomExist(string name)
        {
            return this.db.RoomTypes.Any(r => r.Name == name);
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
