using HotelManagementSystem.Areas.Admin.Models.Countries;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface ICountriesService
    {
        CountriesQueryModel All(CountriesQueryModel query);

        EditCountryFormModel LoadCountry(string id);

        Task Edit(EditCountryFormModel country);

        bool IsCountryExistForEdit(string name, string id);

        bool IsCountryExistForAdd(string name);

        Task Add(AddCountryFormModel country);

        Task Delete(string id);

        public bool IsCountryIdExist(string id);
    }
}
