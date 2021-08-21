using DataLayer;
using HotelManagementSystem.Areas.Admin.Models.Company;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HotelManagementDbContext db;

        public CompanyService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public CompanyFormModel CompanyInfo()
        {
            var currentCompany = this.db
                .Companies
                .OrderBy(c => c.Name)
                .Select(c => new CompanyFormModel
                {
                    Address = c.Address,
                    Email = c.Email,
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone
                })
                .FirstOrDefault();

            return currentCompany;
        }

        public CompanyFormModel GetCountries(CompanyFormModel company)
        {
            var companyCountry = this.db
                .Companies
                .OrderBy(c => c.Name)
                .Select(c => c.City.Country.Id)
                .FirstOrDefault();

            company.CountryId = companyCountry;

            company.Countries = this.db
                .Countries
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
                .ToList();

            return company;
        }

        public CompanyFormModel GetCities(CompanyFormModel company)
        {
            var companyCityId = this.db
                .Companies
                .OrderBy(c => c.Name)
                .Select(c => c.CityId)
                .FirstOrDefault();

            company.CityId = companyCityId;

            company.Cities = this.db
                .Cities
                .Where(c => c.CountryId == company.CountryId)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
                .ToList();

            return company;
        }

        public async Task Update(CompanyFormModel company)
        {
            var currentCompany = this.db
                .Companies
                .OrderBy(c => c.Name)
                .FirstOrDefault();

            currentCompany.Address = company.Address;
            currentCompany.CityId = company.CityId;
            currentCompany.Email = company.Email;
            currentCompany.Name = company.Name;
            currentCompany.Phone = company.Phone;

            this.db.Companies.Update(currentCompany);
            await this.db.SaveChangesAsync();
        }
    }
}
