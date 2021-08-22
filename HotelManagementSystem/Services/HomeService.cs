using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Enums;
using HotelManagementSystem.Models.Home;
using System;
using System.Linq;

namespace HotelManagementSystem.Services
{
    public class HomeService : IHomeService
    {
        private readonly HotelManagementDbContext db;
        private readonly IReservationsService rService;

        public HomeService(HotelManagementDbContext dBase, IReservationsService resService)
        {
            this.db = dBase;
            this.rService = resService;
        }

        public HomeViewModel GetDashboardInfo()
        {
            var currentHotel = this.rService.GetActiveHotel();

            var totalGuests = this.GetTotalGuests();

            var totalVouchers = this.GetTotalVouchers();

            var totalActiveReservations = GetTotalActiveReservations(currentHotel);

            var totalUnpaidVoices = this.GetTotalUnpaidInvoices(currentHotel);

            var checkIns = this.GetCheckIns(currentHotel);

            var checkOuts = GetCheckOuts(currentHotel);

            var currentDashboard = new HomeViewModel
            {
                TotalCheckIn = checkIns,
                TotalCheckOut = checkOuts,
                TotalGuests = totalGuests,
                TotalReservations = totalActiveReservations,
                TotalUnpaidInvoices = totalUnpaidVoices,
                TotalVouchers = totalVouchers
            };

            return currentDashboard;
        }

        private int GetCheckOuts(Hotel currentHotel)
        {
            return this.db
                .RoomReserveds
                .Where(r => r.Reservation.Status != ReservationStatus.Canceled &&
                r.Reservation.EndDate == DateTime.Now.Date && r.Room.Hotel == currentHotel)
                .Count();
        }

        private int GetCheckIns(Hotel currentHotel)
        {
            return this.db
                .RoomReserveds
                .Where(r => r.Reservation.Status != ReservationStatus.Canceled &&
                r.Reservation.StartDate == DateTime.Now.Date && r.Room.Hotel == currentHotel)
                .Count();
        }

        private int GetTotalUnpaidInvoices(Hotel currentHotel)
        {
            return this.db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled &&
                i.Paid == false &&
                i.Reservation.RoomReserveds.All(r => r.Room.Hotel == currentHotel))
                .Count();
        }

        private int GetTotalActiveReservations(Hotel currentHotel)
        {
            return this.db
                .Reservations
                .Where(r => r.Status != ReservationStatus.Canceled &&
                r.RoomReserveds.All(r => r.Room.Hotel == currentHotel) &&
                r.StartDate <= DateTime.Now.Date && r.EndDate >= DateTime.Now.Date)
                .Count();
        }

        private int GetTotalVouchers()
        {
            return this.db
                .Vouchers
                .Where(v => v.Deleted == false)
                .Count();
        }

        private int GetTotalGuests()
        {
            return this.db
                .Guests
                .Where(g => g.Deleted == false)
                .Count();
        }
    }
}
