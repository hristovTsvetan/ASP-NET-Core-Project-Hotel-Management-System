using HotelManagementSystem.Areas.Admin.Models.Hotels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public interface IHotelsService
    {
        HotelsQueryModel All(HotelsQueryModel query);

        ICollection<SelectListItem> GetCountries();

        Task Add(AddHotelFormModel hotel);

        EditHotelFormModel GetHotel(string id);

        ICollection<SelectListItem> GetCities();

        ICollection<SelectListItem> GetActiveList();

        Task Edit(EditHotelFormModel hotel);

        Task Delete(string id);
    }
}
