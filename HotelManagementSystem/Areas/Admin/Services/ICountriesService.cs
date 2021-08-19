using HotelManagementSystem.Areas.Admin.Models.Countries;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface ICountriesService
    {
        CountriesQueryModel All(CountriesQueryModel query);

        EditCountryFormModel LoadCountry(string id);

        void Edit(EditCountryFormModel country);

        bool IsCountryExistForEdit(string name, string id);

        bool IsCountryExistForAdd(string name);

        void Add(AddCountryFormModel country);

        void Delete(string id);
    }
}
