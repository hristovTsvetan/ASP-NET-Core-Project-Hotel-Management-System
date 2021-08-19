using HotelManagementSystem.Areas.Admin.Models.Cities;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {
        private IAdminCitiesService citiesService;

        public CitiesController(IAdminCitiesService cityService)
        {
            this.citiesService = cityService;
        }

        public IActionResult All([FromQuery] CitiesQueryModel query)
        {
            query = citiesService.All(query);

            return this.View(query);
        }

        public IActionResult Edit(string id)
        {
            var curcity = this.citiesService.LoadCity(id);

            return this.View(curcity);
        }

        [HttpPost]
        public IActionResult Edit(EditCityFormModel city)
        {
            if(!ModelState.IsValid)
            {
                return this.View(city);
            }

            this.citiesService.Edit(city);

            return this.RedirectToAction("All", "Cities");
        }

        public IActionResult Add()
        {
            var allCountries = this.citiesService.LoadCountries();

            return this.View(allCountries);
        }

        [HttpPost]
        public IActionResult Add(AddCityFormModel city)
        {
            if(!ModelState.IsValid)
            {
                city = this.citiesService.LoadCountries();

                return this.View(city);
            }

            this.citiesService.Add(city);

            return this.RedirectToAction("All", "Cities");
        }

        public IActionResult Delete(string id)
        {
            this.citiesService.Delete(id);

            return this.RedirectToAction("All", "Cities");
        }
    }
}
