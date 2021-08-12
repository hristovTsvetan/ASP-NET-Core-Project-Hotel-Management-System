﻿using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.RoomsType;
using Microsoft.AspNetCore.Mvc;
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

        public bool IsRoomNameExistForAdd(string name)
        {
            return this.db.RoomTypes.Any(r => r.Name == name);
        }

        public bool IsRoomNameExistForEdit(string name, string id)
        {
            return this.db
                .RoomTypes
                .Where(rt => rt.Id != id)
                .Any(rt => rt.Name == name);
        }

        public ListRoomTypeQueryModel ListTypes(ListRoomTypeQueryModel rTQuery)
        {
            var rtDb = this.db
                .RoomTypes
                .Where(rt => rt.Deleted == false)
                .AsQueryable();



            var allRoomTypes = rtDb
                .Skip((rTQuery.CurrentPage - 1) * rTQuery.ItemsPerPage)
                .Take(rTQuery.ItemsPerPage)
                .Select(t => new ListRoomTypeViewModel
                {
                    Id = t.Id,
                    Image = t.Image,
                    Name = t.Name,
                    NumberOfBeds = t.NumberOfBeds,
                    Price = t.Price,
                    RoomsCount = t.Rooms.Count
                }).ToList();

            var roomTypeQModel = new ListRoomTypeQueryModel
            {
                CurrentPage = rTQuery.CurrentPage,
                TotalPages = (int)Math.Ceiling((double)rtDb.ToList().Count / rTQuery.ItemsPerPage),
                RoomTypes = allRoomTypes
            };

            return roomTypeQModel;
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