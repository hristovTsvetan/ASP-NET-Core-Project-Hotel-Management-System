using HotelManagementSystem.Areas.Admin.Models.Hotels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IHotelsService
    {
        HotelsQueryModel All(HotelsQueryModel query);

        ICollection<SelectListItem> GetCountries();

        void Add(AddHotelFormModel hotel);

        EditHotelFormModel GetHotel(string id);

        ICollection<SelectListItem> GetCities();

        ICollection<SelectListItem> GetActiveList();

        void Edit(EditHotelFormModel hotel);

        void Delete(string id);
    }
}
