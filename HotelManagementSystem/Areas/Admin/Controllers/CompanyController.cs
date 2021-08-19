using HotelManagementSystem.Areas.Admin.Models.Company;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService compService;

        public CompanyController(ICompanyService companyService)
        {
            this.compService = companyService;
        }

        public IActionResult CompanyInfo()
        {
            var company = this.compService.CompanyInfo();
            company = this.compService.GetCountries(company);
            company = this.compService.GetCities(company);

            return this.View(company);
        }

        [HttpPost]
        public IActionResult CompanyInfo(CompanyFormModel company)
        {
            if (!ModelState.IsValid)
            {
                company = this.compService.GetCountries(company);
                company = this.compService.GetCities(company);

                return this.View(company);
            }

            this.compService.Update(company);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
