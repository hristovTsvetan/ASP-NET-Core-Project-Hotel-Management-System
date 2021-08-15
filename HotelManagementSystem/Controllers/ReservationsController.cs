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

        public IActionResult Add([FromQuery]AddReservationFormModel reservation)
        {
            if(reservation.StartDate < DateTime.UtcNow)
            {
                reservation.StartDate = DateTime.UtcNow;
                reservation.EndDate = DateTime.UtcNow.AddDays(1);
            }

            return this.View(reservation);
        }
    }
}
