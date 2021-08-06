using HotelManagementSystem.Models.Guests;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class GuestsController : Controller
    {
        private readonly IGuestsService guestService;

        public GuestsController(IGuestsService gService)
        {
            this.guestService = gService;
        }

        public IActionResult All()
        {
            var guests = this.guestService.GetGuests();

            return this.View(guests);
        }

        public IActionResult Add()
        {
            var customer = this.guestService.AddGet();

            return this.View(customer);
        }

        [HttpPost]
        public IActionResult Add(AddCustomerFormModel customer)
        {
            if(!ModelState.IsValid)
            {
                var guest = this.guestService.AddGet();

                customer.Countries = guest.Countries;
                customer.Ranks = guest.Ranks;

                return this.View(customer);
            }

            this.guestService.AddPost(customer);

            return RedirectToAction("All", "Guests");
        }
    }
}
