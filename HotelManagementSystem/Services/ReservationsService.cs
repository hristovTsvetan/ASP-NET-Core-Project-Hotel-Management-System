using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
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

        public void AddReservation(AddReservationFormModel reservation)
        {
            var allReservedRooms = this.GetReservedRooms(reservation);

          //  var invoice = this.CreateInvoice(reservation);

            var curReservation = new Reservation
            {
                Duration = reservation.EndDate.Subtract(reservation.StartDate).Days,
                EndDate = reservation.EndDate,
                Name = reservation.Name,
                RoomReserveds = allReservedRooms.ToList(),
                StartDate = reservation.StartDate,
                Status = ReservationStatus.Pending
            };

            this.db.Reservations.Add(curReservation);
            this.db.SaveChanges();
        }

        private Invoice CreateInvoice(AddReservationFormModel reservation)
        {
            return new Invoice
            {
                IssuedDate = DateTime.UtcNow,
                Paid = false,
                Status = InvoiceStatus.Pending
            };
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
                var currentHotel = this.GetActiveHotel();

                var roomsWithoutRes = this.db
                .Rooms
                .Where(r => r.RoomReserveds.Count == 0 && r.Deleted == false && r.Hotel == currentHotel)
                .Select(r => new RoomViewModel
                {
                    Id = r.Id,
                    Name = r.Number,
                    Type = r.RoomType.Name
                })
                .ToList();

                var roomsWithOneReservation = this.db
                    .Rooms
                    .Where(r => r.RoomReserveds.Count == 1 && r.Deleted == false && r.Hotel == currentHotel &&
                    r.RoomReserveds
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

        private Hotel GetActiveHotel()
        {
            return this.db
                .Hotels
                .FirstOrDefault(h => h.Active == true);
        }

        private IEnumerable<RoomReserved> GetReservedRooms(AddReservationFormModel reservation)
        {
            var allReservedRooms = new List<RoomReserved>();

            foreach (var roomId in reservation.SelectedRooms)
            {
                var curRoom = this.db
                    .Rooms
                    .FirstOrDefault(r => r.Id == roomId);

                allReservedRooms.Add(new RoomReserved { Room = curRoom });
            }

            return allReservedRooms;
        }
    }
}
