using HotelManagementSystem.Areas.Admin.Models.Hotels;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HotelsController : Controller
    {
        private readonly IHotelsService hotelService;

        public HotelsController(IHotelsService hService)
        {
            this.hotelService = hService;
        }

        public IActionResult All([FromQuery] HotelsQueryModel query)
        {
            var hotelsQuery = hotelService.All(query);

            return this.View(hotelsQuery);
        }

        public IActionResult Add()
        {
            var queryHotel = new AddHotelFormModel
            {
                Countries = this.hotelService.GetCountries()
            };

            return this.View(queryHotel);
        }

        [HttpPost]
        public IActionResult Add(AddHotelFormModel hotel)
        {
            if(!ModelState.IsValid)
            {
                hotel.Countries = this.hotelService.GetCountries();

                return this.View(hotel);
            }

            this.hotelService.Add(hotel);

            return this.RedirectToAction("All", "Hotels");
        }

        public IActionResult Edit(string id)
        {
            var hotel = this.hotelService.GetHotel(id);

            return this.View(hotel);
        }

        [HttpPost]
        public IActionResult Edit(EditHotelFormModel hotel)
        {
            if (!ModelState.IsValid)
            {
                hotel.Countries = this.hotelService.GetCountries();
                hotel.Cities = this.hotelService.GetCities();
                hotel.Active = this.hotelService.GetActiveList();

                return this.View(hotel);
            }

            this.hotelService.Edit(hotel);

            return this.RedirectToAction("All", "Hotels");
        }

        public IActionResult Delete(string id)
        {
            this.hotelService.Delete(id);

            return this.RedirectToAction("All", "Hotels");
        }
    }
}

