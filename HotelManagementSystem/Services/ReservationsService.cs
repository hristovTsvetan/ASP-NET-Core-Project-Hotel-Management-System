using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models.Enums;
using HotelManagementSystem.Models.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly HotelManagementDbContext db;

        public ReservationsService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public AddReservationFormModel AddToReserveRooms(AddReservationFormModel reservation)
        {
            var allRoons = this.db
            .Rooms
            .Where(r => r.Deleted == false)
            .Select(r => new
            {
                Number = r.Number,
                Id = r.Id,
                rType = r.RoomType.Name
            })
            .ToList();

            foreach (var roomId in reservation.SelectedRooms)
            {
                var currentRoom = allRoons.FirstOrDefault(r => r.Id == roomId);

                reservation.AddedForReservationRoom.Add(new SelectListItem
                {
                    Text = currentRoom.Number + " " + currentRoom.rType,
                    Value = currentRoom.Id,
                    Selected = true
                });

                reservation.AvailableRooms.Remove(reservation.AvailableRooms.FirstOrDefault(r => r.Value == roomId));
            }

            //Remove from available rooms selected rooms
            foreach (var rm in reservation.ReservedRooms)
            {
                if (reservation.AvailableRooms.Any(r => r.Value == rm))
                {
                    reservation.AvailableRooms.Remove(reservation.AvailableRooms.FirstOrDefault(r => r.Value == rm));
                }

                var selRoom = allRoons.FirstOrDefault(r => r.Id == rm);

                //add old selections
                reservation.AddedForReservationRoom.Add(new SelectListItem
                {
                    Text = selRoom.Number + " " + selRoom.rType,
                    Value = selRoom.Id,
                });
            }

            foreach (var rm in reservation.SelectedRooms)
            {
                reservation.ReservedRooms.Add(rm);
            }

            return reservation;
        }

        public ReservationsQueryModel All()
        {
            var reservetionsQueryModel = new ReservationsQueryModel
            {
                Reservations = this.db
                .Reservations
                .Where(r => r.Status != ReservationStatus.Canceled)
                .Select(r => new ReservationsViewModel
                {
                    Id = r.Id,
                    EndDate = r.EndDate.ToShortDateString(),
                    StartDate = r.StartDate.ToShortDateString(),
                    GuestName = r.Guest.FirstName + " " + r.Guest.LastName,
                    RoomName = r
                        .RoomReserveds
                        .Select(rm => rm.Room.Number)
                        .ToList()
                })
            };

            return reservetionsQueryModel;
        }

        public void CancelReservation(string roomId)
        {
            var today = DateTime.UtcNow;

            var allReservations = this.db
                .RoomReserveds
                .Where(r => r.RoomId == roomId && r.Reservation.StartDate >= today)
                .ToList();

            if (allReservations.Count > 0)
            {
                foreach (var roomReserved in allReservations)
                {
                    roomReserved.Reservation.Status = ReservationStatus.Canceled;
                }

                this.db.UpdateRange(allReservations);
                this.db.SaveChanges();
            }
        }

        public AddReservationFormModel ListFreeRooms(AddReservationFormModel reservation)
        {
            if (reservation.StartDate < reservation.EndDate &&
                reservation.StartDate >= DateTime.Now.Date && reservation.EndDate > DateTime.Now.Date)

            {
                var roomsWithoutRes = this.db
                .Rooms
                .Where(r => r.RoomReserveds.Count == 0 && r.Deleted == false)
                .Select(r => new RoomViewModel
                {
                    Id = r.Id,
                    Name = r.Number,
                    Type = r.RoomType.Name
                })
                .ToList();

                var roomsWithOneReservation = this.db
                    .Rooms
                    .Where(r => r.RoomReserveds.Count == 1 && r.Deleted == false && r.RoomReserveds
                    .Any(res => res.Reservation.EndDate == reservation.StartDate))
                    .Select(r => new RoomViewModel
                    {
                        Id = r.Id,
                        Name = r.Number,
                        Type = r.RoomType.Name
                    })
                    .ToList();

                roomsWithoutRes.AddRange(roomsWithOneReservation);

                var allAvailRooms = roomsWithoutRes.Select(i => new SelectListItem
                {
                    Text = i.Name + " " + i.Type,
                    Value = i.Id
                });

                reservation.AvailableRooms = allAvailRooms
                    .OrderBy(r => r.Text)
                    .ToList();
            }

            return reservation;
        }
    }
}
