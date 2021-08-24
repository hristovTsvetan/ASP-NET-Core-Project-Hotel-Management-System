using HotelManagementSystem.Areas.Admin.Models.Company;
using HotelManagementSystem.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> CompanyInfo(CompanyFormModel company)
        {
            company = this.compService.GetCountries(company);

            if (!ModelState.IsValid)
            {
                return this.View(company);
            }

            await this.compService.Update(company);

            return this.View(company);
        }
    }
}
