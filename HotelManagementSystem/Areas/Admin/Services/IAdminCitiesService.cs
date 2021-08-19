using HotelManagementSystem.Areas.Admin.Models.Cities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IAdminCitiesService
    {
        CitiesQueryModel All(CitiesQueryModel query);

        EditCityFormModel LoadCity(string id);

        void Edit(EditCityFormModel city);

        bool IsCityExistForEdit(string name, string id);

        void Add(AddCityFormModel city);

        AddCityFormModel LoadCountries();

        void Delete(string id);
    }
}
