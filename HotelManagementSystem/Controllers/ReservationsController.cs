using HotelManagementSystem.Models.Reservations;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace HotelManagementSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationsService resService;

        public ReservationsController(IReservationsService rService)
        {
            this.resService = rService;
        }

        public IActionResult All()
        {
            var allReservations = resService.All();

            return this.View(allReservations);
        }

        public IActionResult Add()
        {
            var reservation = new AddReservationFormModel();

            reservation.StartDate = DateTime.UtcNow;
            reservation.EndDate = DateTime.UtcNow.AddDays(1);

            return this.View(reservation);
        }

        [HttpPost]
        public IActionResult Add(AddReservationFormModel reservation)
        {

            if (reservation.StartDate < DateTime.Now.Date)
            {
                reservation.StartDate = DateTime.UtcNow;
                reservation.EndDate = DateTime.UtcNow.AddDays(1);
            }

            if(!string.IsNullOrWhiteSpace(reservation.LoadRoomsButton))
            {
                reservation = this.resService.ListFreeRooms(reservation);
            }

            if(!string.IsNullOrWhiteSpace(reservation.AddRoomsButton))
            {
                reservation = this.resService.ListFreeRooms(reservation);
                reservation = this.resService.AddToReserveRooms(reservation);
            }

            if (!string.IsNullOrWhiteSpace(reservation.AddReservationButton))
            {
                if (!ModelState.IsValid)
                {
                    return this.View(reservation);
                }

                return this.RedirectToAction("All", "Reservations");
            }        

            return this.View(reservation);
        }
    }
}
