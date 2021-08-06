using HotelManagementSystem.Models.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IGuestsService
    {
        AddCustomerFormModel AddGet();

        void AddPost(AddCustomerFormModel customer);

        ListGuestsQueryModel GetGuests(ListGuestsQueryModel query);

        bool IsCountryExist(string countryId);

        bool IsCityExist(string cityId);

        bool IsRankExist(string rankId);

        bool IsIdentityNumberExist(string identityNumber);
    }
}
