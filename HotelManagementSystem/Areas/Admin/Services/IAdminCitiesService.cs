using HotelManagementSystem.Areas.Admin.Models.Cities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IAdminCitiesService
    {
        CitiesQueryModel All(CitiesQueryModel query);

        EditCityFormModel LoadCity(string id);

        Task Edit(EditCityFormModel city);

        bool IsCityExistForEdit(string name, string id);

        Task Add(AddCityFormModel city);

        AddCityFormModel LoadCountries();

        Task Delete(string id);
    }
}
