using HotelManagementSystem.Areas.Admin.Controllers;
using HotelManagementSystem.Areas.Admin.Models.Hotels;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class HotelControllerTest
    {
        [Fact]
        public void ShouldReturnHotel()
        {
            MyController<HotelsController>
                .Instance()
                .WithData(GeneralMocking.GetActiveHotelWithRooms(), GeneralMocking.GetCountries())
                .Calling(rooms => rooms.All(With.Default<HotelsQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<HotelsQueryModel>()
                    .Passing(g =>
                    {
                        Assert.Equal("Sunny", g.Hotels.FirstOrDefault().Name);
                    }
                ));
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<HotelsController>
                .Instance()
                .ShouldHave()
                .Attributes(attr =>
                    attr.RestrictingForAuthorizedRequests()
                );
        }

        [Fact]
        public void ShouldReturnViewOnAddHotel()
        {
            MyController<HotelsController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectIfAddHotelIsSuccess()
        {
            MyController<HotelsController>
                .Instance()
                .WithData(GeneralMocking.GetActiveHotelWithRooms(), GeneralMocking.GetCountries())
                .Calling(c => c.Add(GeneralMocking.AddHotelFormModel()))
                .ShouldReturn()
                .RedirectToAction("All", "Hotels");
        }

        [Fact]
        public void ShouldRedirectOnDeleteHotel()
        {
            MyController<HotelsController>
                .Instance()
                .WithData(GeneralMocking.GetInactivHotel())
                .Calling(d => d.Delete("TestHotelId"))
                .ShouldReturn()
                .RedirectToAction("All", "Hotels");
        }

        [Fact]
        public void ShouldRedirectWhenChangeHotelWhenEdit()
        {
            MyController<HotelsController>
                .Instance()
                .WithData(GeneralMocking.GetActiveHotel(), GeneralMocking.GetCountries())
                .Calling(m => m.Edit(GeneralMocking.EditHotelFormModel()))
                .ShouldReturn()
                .RedirectToAction("All", "Hotels");
        }

        [Fact]
        public void ShouldReturnCorrectHotelForEditView()
        {
            MyController<HotelsController>
              .Instance()
              .WithData(GeneralMocking.GetActiveHotelWithRooms(), GeneralMocking.GetCountries())
              .Calling(m => m.Edit("TestId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditHotelFormModel>()
              .Passing(c =>
              {
                  Assert.Equal("Sunny", c.Name);
                  Assert.Equal("Po Box 101", c.Address);
                  Assert.Equal("345345435", c.Phone);
                  Assert.Equal("hotel@hotel.bg", c.Email);
              }));
        }
    }


}
