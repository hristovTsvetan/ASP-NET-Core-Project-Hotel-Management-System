using HotelManagementSystem.Areas.Admin.Models.Cities;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Edit(EditCityFormModel city)
        {
            if(!ModelState.IsValid)
            {
                return this.View(city);
            }

            await this.citiesService.Edit(city);

            return this.RedirectToAction("All", "Cities");
        }

        public IActionResult Add()
        {
            var allCountries = this.citiesService.LoadCountries();

            return this.View(allCountries);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCityFormModel city)
        {
            if(!ModelState.IsValid)
            {
                city = this.citiesService.LoadCountries();

                return this.View(city);
            }

            await this.citiesService.Add(city);

            return this.RedirectToAction("All", "Cities");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.citiesService.Delete(id);

            return this.RedirectToAction("All", "Cities");
        }
    }
}
