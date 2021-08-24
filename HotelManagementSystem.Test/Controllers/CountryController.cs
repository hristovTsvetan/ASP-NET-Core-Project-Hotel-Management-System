using HotelManagementSystem.Areas.Admin.Controllers;
using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class CountryController
    {
        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<CountriesController>
                .Instance()
                .ShouldHave()
                .Attributes(attr =>
                    attr.RestrictingForAuthorizedRequests()
                );
        }

        [Fact]
        public void ShouldReturnCountry()
        {
            MyController<CountriesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(rooms => rooms.All(With.Default<CountriesQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<CountriesQueryModel>()
                    .Passing(g =>
                    {
                        Assert.Equal("Bulgaria", g.Countries.FirstOrDefault().Name);
                    }
                ));
        }

        [Fact]
        public void ShouldRedirectOnDelete()
        {
            MyController<CountriesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(m => m.Delete("TestCountryId"))
                .ShouldReturn()
                .RedirectToAction("All", "Countries");
        }

        [Fact]
        public void ShouldReturnCorrectCountryForEditView()
        {
            MyController<CountriesController>
              .Instance()
              .WithData(GeneralMocking.GetCountries())
              .Calling(m => m.Edit("TestCountryId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditCountryFormModel>()
              .Passing(c =>
              {
                  Assert.Equal("Bulgaria", c.Name);
              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeCountryWhenEdit()
        {
            MyController<CountriesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .WithHttpRequest(r => r.WithPath("/Countries/Edit/TestCountryId"))
                .Calling(m => m.Edit(GeneralMocking.EditCountryForm()))
                .ShouldReturn()
                .RedirectToAction("All", "Countries");
        }

        [Fact]
        public void ShouldReturnViewOnAdd()
        {
            MyController<CountriesController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectOnDeleteCountry()
        {
            MyController<CountriesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(d => d.Delete("TestCountryId"))
                .ShouldReturn()
                .RedirectToAction("All", "Countries");
        }

        [Fact]
        public void ShouldRedirectIfAddCountryIsSuccess()
        {
            MyController<CountriesController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(c => c.Add(GeneralMocking.AddCountryForm()))
                .ShouldReturn()
                .RedirectToAction("All", "Countries");
        }
    }
}
