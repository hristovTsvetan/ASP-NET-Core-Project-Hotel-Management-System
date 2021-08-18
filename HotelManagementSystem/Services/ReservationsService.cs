﻿using HotelManagementSystem.Data;
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

            var curReservation = new Reservation
            {
                Duration = reservation.EndDate.Subtract(reservation.StartDate).Days,
                EndDate = reservation.EndDate,
                Name = reservation.Name,
                RoomReserveds = allReservedRooms.Select(r => new RoomReserved 
                    {
                        RoomId = r.Id
                    }).ToList(),
                StartDate = reservation.StartDate,
                Status = ReservationStatus.Pending,
                CreatedOn = DateTime.UtcNow,
                Price = allReservedRooms.Select(r => r.Price).Sum() * reservation.EndDate.Subtract(reservation.StartDate).Days
            };

            this.db.Reservations.Add(curReservation);
            this.db.SaveChanges();
        }

        public ReservationsQueryModel All(ReservationsQueryModel res)
        {
            var activeHotel = GetActiveHotel();

            //Get distincted reservation ids
            var reservations = this.db
                .RoomReserveds
                .Where(r => r.Room.HotelId == activeHotel.Id && r.Reservation.Status != ReservationStatus.Canceled)
                .Select(r => r.ReservationId)
                .Distinct();


            var resDb = this.db
               .RoomReserveds
               .Where(r => r.Room.HotelId == activeHotel.Id && r.Reservation.Status != ReservationStatus.Canceled)
               .Select(r => r.Reservation)
               .AsQueryable();

            var reservCol = new List<ReservationsViewModel>();

            foreach (var rs in reservations)
            {
                var tempRes = resDb
                .Where(r => r.Id == rs)
                .FirstOrDefault();

                reservCol.Add(new ReservationsViewModel
                {
                    Name = tempRes.Name,
                    EndDate = tempRes.EndDate.ToString("dd-MM-yyyy"),
                    Id = tempRes.Id,
                    StartDate = tempRes.StartDate.ToString("dd-MM-yyyy"),
                    Status = tempRes.Status.ToString(),
                    CreatedOn = tempRes.CreatedOn
                });

            }

            if (!string.IsNullOrWhiteSpace(res.Search))
            {
                reservCol = reservCol
                    .Where(r => r.Name.ToLower().Contains(res.Search.ToLower()))
                    .ToList();
            }

            var allReservations = reservCol
                .OrderBy(r => r.CreatedOn)
                .Skip((res.CurrentPage - 1) * res.ItemsPerPage)
                .Take(res.ItemsPerPage)
                .ToList();

            var reservetionsQueryModel = new ReservationsQueryModel
            {
                Reservations = allReservations,
                TotalPages = (int)Math.Ceiling((double)reservCol.Count() / res.ItemsPerPage),
                CurrentPage = res.CurrentPage,
                PreviousPage = res.PreviousPage,
                NextPage = res.NextPage
            };

            return reservetionsQueryModel;
        }

        public void AssignGuestToReservation(AssignGuestFormModel guest)
        {
            var currentReservation = this.db
                .Reservations
                .FirstOrDefault(r => r.Id == guest.ReservationId);

            var currentGuest = this.db
                .Guests
                .FirstOrDefault(g => g.IdentityCardId == guest.IdentityId);

            



            currentReservation.Guest = currentGuest;

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

        public void Delete(string id)
        {
            var reservation = this.db.Reservations.FirstOrDefault(r => r.Id == id);

            reservation.Status = ReservationStatus.Canceled;

            this.db.Reservations.Update(reservation);
            this.db.SaveChanges();
        }

        public DetailsReservationViewModel GetDetails(string id)
        {
            return this.db
                .Reservations
                .Where(r => r.Id == id)
                .Select(r => new DetailsReservationViewModel
                {
                    CreatedOn = r.CreatedOn.ToString("dd-MM-yyyy"),
                    Duration = r.Duration,
                    EndDate = r.EndDate.ToString("dd-MM-yyyy"),
                    Id = r.Id,
                    Name = r.Name,
                    Price = r.Price,
                    StartDate = r.StartDate.ToString("dd-MM-yyyy"),
                    Status = r.Status.ToString(),
                    VoucherName = r.Voucher.Name,
                    GuestIdentityCard = r.Guest.IdentityCardId,
                    GuestName = r.Guest.FirstName + " " + r.Guest.LastName,
                    InvoicePaid = r.Invoice.Paid ? "Yes" : "No",
                    RoomReserveds = string.Join(", ", r.RoomReserveds
                        .Select(rr => rr.Room.Number)
                        .ToList())
                }).FirstOrDefault();
        }

        public IEnumerable<SelectListItem> GetVouchers()
        {
            return this.db
                .Vouchers
                .Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id
                })
                .ToList();

        }

        public AddReservationFormModel ListFreeRooms(AddReservationFormModel reservation)
        {
            if (reservation.StartDate < reservation.EndDate &&
                reservation.StartDate >= DateTime.Now.Date && reservation.EndDate > DateTime.Now.Date)

            {
                var currentHotel = this.GetActiveHotel();

                var freeRooms = this.db
                    .Rooms
                    .Where(r => r.Deleted == false && r.Hotel == currentHotel)
                    .Select(r => new
                    {
                        RoomName = r.Number,
                        RoomId = r.Id,
                        RoomType = r.RoomType.Name.ToString(),
                        HasReservation = r.RoomReserveds
                            .Where(rr => rr.Reservation.Status != ReservationStatus.Canceled)
                            .Any(rr => reservation.StartDate < rr.Reservation.EndDate && reservation.EndDate > rr.Reservation.StartDate)
                    })
                    .Where(r => r.HasReservation == false)
                    .ToList();


                var allAvailRooms = freeRooms.Select(i => new SelectListItem
                {
                    Text = i.RoomName + " " + i.RoomType,
                    Value = i.RoomId
                });

                reservation.AvailableRooms = allAvailRooms
                    .OrderBy(r => r.Text)
                    .ToList();
            }

            return reservation;
        }

        public AssignGuestFormModel LoadGuest(string identityId)
        {
            return this.db
                .Guests
                .Where(g => g.IdentityCardId == identityId && g.Deleted == false)
                .Select(g => new AssignGuestFormModel
                {
                    GuestAddress = g.Address,
                    GuestCity = g.City.Name,
                    GuestCountry = g.City.Country.Name,
                    GuestId = g.Id,
                    GuestName = g.FirstName + " " + g.LastName,
                    GuestPhone = g.Phone,
                    IdentityId = g.IdentityCardId,
                })
                .FirstOrDefault();
        }

        private Hotel GetActiveHotel()
        {
            return this.db
                .Hotels
                .FirstOrDefault(h => h.Active == true);
        }

        private IEnumerable<RoomsServiceModel> GetReservedRooms(AddReservationFormModel reservation)
        {
            var allReservedRooms = new List<RoomsServiceModel>();

            foreach (var roomId in reservation.SelectedRooms)
            {
                var curRoom = this.db
                    .Rooms
                    .Where(r => r.Id == roomId)
                    .Select(r => new RoomsServiceModel
                    {
                        Id = r.Id,
                        Number = r.Number,
                        Price = r.RoomType.Price
                    })
                    .FirstOrDefault();

                allReservedRooms.Add(curRoom);
            }

            return allReservedRooms;
        }
    }
}
