using HotelManagementSystem.Areas.Admin.Models.Cities;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IAdminCitiesService
    {
        CitiesQueryModel All(CitiesQueryModel query);

        EditCityFormModel LoadCity(string id);

        Task Edit(EditCityFormModel city);

        bool IsCityExistForEdit(string name, string id);

        bool IsCityExistForAdd(string name);

        Task Add(AddCityFormModel city);

        AddCityFormModel LoadCountries();

        Task Delete(string id);
    }
}
