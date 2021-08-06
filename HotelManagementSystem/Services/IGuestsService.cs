using HotelManagementSystem.Models.Guests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IGuestsService
    {
        void Add(AddCustomerFormModel customer);

        AddCustomerFormModel AddGet();

        void AddPost(AddCustomerFormModel customer);

        bool IsCountryExist(string countryId);

        bool IsCityExist(string cityId);

        bool IsRankExist(string rankId);
    }
}
