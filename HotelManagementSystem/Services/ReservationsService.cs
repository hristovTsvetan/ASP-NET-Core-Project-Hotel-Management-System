using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models.Enums;
using HotelManagementSystem.Models.Reservations;
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

        public ReservationsQueryModel All()
        {
            var reservetionsQueryModel = new ReservationsQueryModel
            {
                Reservations = this.db
                .Reservations
                .Where(r => r.Status == ReservationStatus.Active)
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

            if(allReservations.Count > 0)
            {
                foreach (var roomReserved in allReservations)
                {
                    roomReserved.Reservation.Status = ReservationStatus.Canceled;
                }

                this.db.UpdateRange(allReservations);
                this.db.SaveChanges();
            } 
        }
    }
}
