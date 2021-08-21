using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class CountriesService : ICountriesService
    {
        private HotelManagementDbContext db;

        public CountriesService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public async Task Add(AddCountryFormModel country)
        {
            var newCountry = new Country
            {
                Name = country.Name
            };

            await this.db.Countries.AddAsync(newCountry);
            await this.db.SaveChangesAsync();
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
                TotalPages = (int)Math.Ceiling((double)dbCount / query.ItemsPerPage)
            };

            return countriesQuery;
        }

        public async Task Delete(string id)
        {
            var country = this.db
                .Countries
                .FirstOrDefault(c => c.Id == id);


            country.Deleted = true;

            this.db.Update(country);
            await this.db.SaveChangesAsync();
        }

        public async Task Edit(EditCountryFormModel country)
        {
            var countryForEdit = this.db
                .Countries
                .FirstOrDefault(c => c.Id == country.Id);

            countryForEdit.Name = country.Name;

            this.db.Update(countryForEdit);
            await this.db.SaveChangesAsync();
        }

        public bool IsCountryExistForAdd(string name)
        {
            return this.db
                .Countries
                .Where(c => c.Deleted == false)
                .Any(c => c.Name == name);
        }

        public bool IsCountryNameExist(string name)
        {
            return this.db
                .Countries
                .Where(c => c.Deleted == false)
                .Any(c => c.Name == name);
        }

        public bool IsCountryIdExist(string id)
        {
            return this.db
                .Countries
                .Where(c => c.Deleted == false)
                .Any(c => c.Id == id);
        }

        public bool IsCountryExistForEdit(string name, string id)
        {
            return this.db
                .Countries
                .Where(c => c.Deleted == false && c.Id != id)
                .Any(c => c.Name == name);

                
        }

        public EditCountryFormModel LoadCountry(string id)
        {
            var currentCountry = this.db
                .Countries
                .Where(c => c.Id == id)
                .Select(c => new EditCountryFormModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefault();

            return currentCountry;
        }
    }
}
