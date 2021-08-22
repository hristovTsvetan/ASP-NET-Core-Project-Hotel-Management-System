using HotelManagementSystem.Models.Cities;
using System.Collections.Generic;

namespace HotelManagementSystem.Services
{
    public interface ICitiesService
    {
        IEnumerable<CitiesViewModel> GetCities(string countryId);
    }
}
