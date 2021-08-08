using HotelManagementSystem.Models.Countries;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Models.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IGuestsService
    {
        void Edit(EditGuestFormModel guest);

        void Add(AddCustomerFormModel customer);

        ListGuestsQueryModel GetGuests(ListGuestsQueryModel query);

        DetailsGuestViewModel Details(string id);

        EditGuestFormModel GetGuest(string id);

        IEnumerable<CountriesViewModel> GetCountries();

        IEnumerable<RankViewModel> GetRanks();

        void ChangeReservationStatus(string id);

        void Delete(string id);

        bool IsCountryExist(string countryId);

        bool IsCityExist(string cityId);

        bool IsRankExist(string rankId);

        bool IsIdentityNumberExist(string identityNumber);

        bool IsIdentityNumExistExceptSelf(string identityNumber, string id);
    }
}
