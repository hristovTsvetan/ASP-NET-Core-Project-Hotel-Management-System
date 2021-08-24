using HotelManagementSystem.Models.Guests;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IGuestsService
    {
        Task Edit(EditGuestFormModel guest);

        Task Add(AddCustomerFormModel customer);

        ListGuestsQueryModel GetGuests(ListGuestsQueryModel query);

        DetailsGuestViewModel Details(string id);

        EditGuestFormModel GetGuest(string id);

        ICollection<SelectListItem> GetCountries();

        ICollection<SelectListItem> GetRanks();

        Task Delete(string id);

        bool IsCountryExist(string countryId);

        bool IsCityExist(string cityId);

        bool IsRankExist(string rankId);

        bool IsIdentityNumberExist(string identityNumber);

        bool IsIdentityNumExistExceptSelf(string identityNumber, string id);
    }
}
