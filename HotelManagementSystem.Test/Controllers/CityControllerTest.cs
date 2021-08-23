using HotelManagementSystem.Areas.Admin.Controllers;
using HotelManagementSystem.Areas.Admin.Models.Cities;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class CityControllerTest
    {
        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<CitiesController>
                .Instance()
                .ShouldHave()
                .Attributes(attr =>
                    attr.RestrictingForAuthorizedRequests()
                );
        }

        [Fact]
        public void ShouldReturnCity()
        {
            MyController<CitiesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(rooms => rooms.All(With.Default<CitiesQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<CitiesQueryModel>()
                    .Passing(g =>
                    {
                        Assert.Equal("Sofia", g.Cities.FirstOrDefault().Name);
                    }
                ));
        }

        [Fact]
        public void ShouldReturnCorrectCityForEditView()
        {
            MyController<CitiesController>
              .Instance()
              .WithData(GeneralMocking.GetCountries())
              .Calling(m => m.Edit("TestCityId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditCityFormModel>()
              .Passing(c =>
              {
                  Assert.Equal("Sofia", c.Name);
                  Assert.Equal("1000", c.PostalCode);
              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeCityWhenEdit()
        {
            MyController<CitiesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .WithHttpRequest(r => r.WithPath("/Cities/Edit/TestCityId"))
                .Calling(m => m.Edit(GeneralMocking.EditCityForm()))
                .ShouldReturn()
                .RedirectToAction("All", "Cities");
        }

        [Fact]
        public void ShouldReturnViewOnAdd()
        {
            MyController<CitiesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectIfAddIsSuccess()
        {
            MyController<CitiesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(c => c.Add(GeneralMocking.AddCityForm()))
                .ShouldReturn()
                .RedirectToAction("All", "Cities");
        }

        [Fact]
        public void ShouldRedirectOnDeleteCity()
        {
            MyController<CitiesController>
                .Instance()
                .WithData(GeneralMocking.GetCity())
                .Calling(d => d.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "Cities");
        }

    }
}
