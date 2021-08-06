using HotelManagementSystem.Models.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface ICitiesService
    {
        IEnumerable<CitiesViewModel> GetCities(string countryId);
    }
}
