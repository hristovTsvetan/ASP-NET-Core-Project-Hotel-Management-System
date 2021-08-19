using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Edit(CountriesFormModel country)
        {
            if(!ModelState.IsValid)
            {
                return this.View(country);
            }

            this.countryService.Edit(country);

            return this.RedirectToAction("All", "Countries");
        }
    }
}
