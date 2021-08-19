using HotelManagementSystem.Areas.Admin.Models.Countries;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface ICountriesService
    {
        CountriesQueryModel All(CountriesQueryModel query);

        CountriesFormModel LoadCountry(string id);

        void Edit(CountriesFormModel country);

        bool IsCountryExistForEdit(string name, string id);
    }
}
