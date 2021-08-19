using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Data;
using System;
using System.Linq;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class CountriesService : ICountriesService
    {
        private HotelManagementDbContext db;

        public CountriesService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public CountriesQueryModel All(CountriesQueryModel query)
        {
            var dbQuery = this.db
                .Countries
                .Where(c => c.Deleted == false)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbQuery = dbQuery
                    .Where(c => c.Name.ToLower().Contains(query.Search.ToLower()));
            }

            var dbCount = dbQuery.Count();

            var allCountries = dbQuery
                .OrderBy(c => c.Name)
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(c => new CountriesViewModel 
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();

            var countriesQuery = new CountriesQueryModel
            {
                Countries = allCountries,
                Search = query.Search,
                CurrentPage = query.CurrentPage,
                NextPage = query.NextPage,
                PreviousPage = query.PreviousPage,
                TotalPages = (int)Math.Ceiling((double)dbCount / query.ItemsPerPage),
            };

            return countriesQuery;
        }

        public void Edit(CountriesFormModel country)
        {
            var countryForEdit = this.db
                .Countries
                .FirstOrDefault(c => c.Id == country.Id);

            countryForEdit.Name = country.Name;

            this.db.Update(countryForEdit);
            this.db.SaveChanges();
        }

        public bool IsCountryExistForEdit(string name, string id)
        {
            return this.db
                .Countries
                .Where(c => c.Deleted == false && c.Id != id)
                .Any(c => c.Name == name);

                
        }

        public CountriesFormModel LoadCountry(string id)
        {
            var currentCountry = this.db
                .Countries
                .Where(c => c.Id == id)
                .Select(c => new CountriesFormModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefault();

            return currentCountry;
        }
    }
}
