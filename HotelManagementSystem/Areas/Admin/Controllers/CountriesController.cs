using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountriesController : Controller
    {
        private readonly ICountriesService countryService;

        public CountriesController(ICountriesService cService)
        {
            this.countryService = cService;
        }

        public IActionResult All([FromQuery] CountriesQueryModel query)
        {
            var countriesQuery = countryService.All(query);

            return this.View(countriesQuery);
        }

        public IActionResult Edit(string id)
        {
            var currentCountry = this.countryService.LoadCountry(id);

            return this.View(currentCountry);
        }

        [HttpPost]
        public IActionResult Edit(EditCountryFormModel country)
        {
            if(!ModelState.IsValid)
            {
                return this.View(country);
            }

            this.countryService.Edit(country);

            return this.RedirectToAction("All", "Countries");
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddCountryFormModel country)
        {
            if(!ModelState.IsValid)
            {
                return this.View(country);
            }

            this.countryService.Add(country);

            return this.RedirectToAction("All", "Countries");
        }

        public IActionResult Delete(string id)
        {
            this.countryService.Delete(id);

            return this.RedirectToAction("All", "Countries");
        }
    }
}
