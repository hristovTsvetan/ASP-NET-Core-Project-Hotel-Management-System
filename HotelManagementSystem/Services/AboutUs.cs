using DataLayer;
using HotelManagementSystem.Models.AboutUs;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace HotelManagementSystem.Services
{
    [Authorize]
    public class AboutUs : IAboutUs
    {
        private HotelManagementDbContext db;

        public AboutUs(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public AboutUsViewModel AboutUsData()
        {
            var aboutUsViewModel = new AboutUsViewModel();

            aboutUsViewModel = GetCompany(aboutUsViewModel);
            aboutUsViewModel = GetHotel(aboutUsViewModel);

            return aboutUsViewModel;
        }

        private AboutUsViewModel GetHotel(AboutUsViewModel data)
        {
            var currentHotel = this.db
                .Hotels
                .Where(h => h.Deleted == false && h.Active == true)
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    HotelAddress = c.Address,
                    HotelCity = c.City.Name,
                    HotelCountry = c.City.Country.Name,
                    HotelMail = c.Email,
                    HotelName = c.Name,
                    HotelPhone = c.Phone
                })
                .FirstOrDefault();

            data.HotelAddress = currentHotel.HotelAddress;
            data.HotelCity = currentHotel.HotelCity;
            data.HotelCountry = currentHotel.HotelCountry;
            data.HotelMail = currentHotel.HotelMail;
            data.HotelName = currentHotel.HotelName;
            data.HotelPhone = currentHotel.HotelPhone;

            return data;
        }

        private AboutUsViewModel GetCompany(AboutUsViewModel data)
        {
            var currentCompany = this.db
               .Companies
               .OrderBy(c => c.Name)
               .Select(c => new
               {
                   CompanyAddress = c.Address,
                   CompanyCity = c.City.Name,
                   CompanyCountry = c.City.Country.Name,
                   CompanyMail = c.Email,
                   CompanyName = c.Name,
                   CompanyPhone = c.Phone
               })
               .FirstOrDefault();

            data.CompanyAddress = currentCompany.CompanyAddress;
            data.CompanyCity = currentCompany.CompanyCity;
            data.CompanyCountry = currentCompany.CompanyCountry;
            data.CompanyMail = currentCompany.CompanyMail;
            data.CompanyName = currentCompany.CompanyName;
            data.CompanyPhone = currentCompany.CompanyPhone;

            return data;
        }
    }
}
