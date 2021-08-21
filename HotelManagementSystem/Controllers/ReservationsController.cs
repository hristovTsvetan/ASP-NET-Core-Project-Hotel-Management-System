using HotelManagementSystem.Models.Reservations;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationsService resService;

        public ReservationsController(IReservationsService rService)
        {
            this.resService = rService;
        }

        public IActionResult All([FromQuery]ReservationsQueryModel res)
        {
            var allReservations = resService.All(res);

            return this.View(allReservations);
        }

        public IActionResult Add()
        {
            var reservation = new AddReservationFormModel();

            reservation.StartDate = DateTime.Now.Date;
            reservation.EndDate = DateTime.Now.AddDays(1);

            return this.View(reservation);
        }

        [HttpPost]
        public async Task <IActionResult> Add(AddReservationFormModel reservation)
        {

            if (reservation.StartDate < DateTime.Now.Date)
            {
                reservation.StartDate = DateTime.Now.Date;
                reservation.EndDate = DateTime.Now.Date.AddDays(1);
            }

            if(!string.IsNullOrWhiteSpace(reservation.LoadRoomsButton))
            {
                reservation = this.resService.ListFreeRooms(reservation);
            }


            if (!string.IsNullOrWhiteSpace(reservation.AddReservationButton))
            {
                if (!ModelState.IsValid)
                {
                    reservation = this.resService.ListFreeRooms(reservation);

                    return this.View(reservation);
                }

                await this.resService.AddReservation(reservation);

                return this.RedirectToAction("All", "Reservations");
            }        

            return this.View(reservation);
        }

        public IActionResult Details(string id)
        {
            var curReservation = this.resService.GetDetails(id);

            return this.View(curReservation);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.resService.Delete(id);

            return this.RedirectToAction("All", "Reservations");
        }

        public IActionResult AssignCustomer(string id)
        {
            var guest = new AssignGuestFormModel
            {
                ReservationId = id,
                Vouchers = this.resService.GetVouchers()
            };

            return this.View(guest);
        }

        [HttpPost]
        public async Task<IActionResult> AssignCustomer(AssignGuestFormModel guest)
        {

            guest.Vouchers = this.resService.GetVouchers();

            if (!string.IsNullOrWhiteSpace(guest.LoadGuestButton))
            {
                var curGuest = this.resService.LoadGuest(guest.IdentityId);
                if (curGuest != null)
                {
                    curGuest.Vouchers = guest.Vouchers;
                    return this.View(curGuest);
                }

                return this.View(guest);
            }

            if(!string.IsNullOrWhiteSpace(guest.AssignButton))
            {
                if (!ModelState.IsValid)
                {
                    return this.View(guest);
                }

                await this.resService.AssignGuestToReservation(guest);

                return this.RedirectToAction("All", "Reservations");
            }

            return this.View(guest);
        }
    }
}
