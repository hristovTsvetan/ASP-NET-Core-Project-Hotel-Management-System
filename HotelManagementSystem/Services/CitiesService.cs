using DataLayer;
using HotelManagementSystem.Models.Cities;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Services
{
    public class CitiesService : ICitiesService
    {
        private readonly HotelManagementDbContext db;

        public CitiesService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public IEnumerable<CitiesViewModel> GetCities(string countryId)
        {
            return db.Cities
                .Where(c => c.CountryId == countryId)
                .Select(c => new CitiesViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}
