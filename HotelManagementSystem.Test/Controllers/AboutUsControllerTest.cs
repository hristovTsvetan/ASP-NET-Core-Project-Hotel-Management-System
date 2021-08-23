using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.AboutUs;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class AboutUsControllerTest
    {
        [Fact]
        public void ShouldReturnCompanyInfo()
        {
            MyController<AboutUsController>
                .Instance()
                .WithData(GeneralMocking.GetCompany(), GeneralMocking.GetActiveHotel())
                .Calling(m => m.AboutUs())
                .ShouldReturn()
                .View(result =>
                    result
                    .WithModelOfType<AboutUsViewModel>()
                    .Passing(model =>
                    {
                        Assert.Equal("My company", model.CompanyName);
                        Assert.Equal("Sofia", model.CompanyCity);
                        Assert.Equal("Bulgaria", model.CompanyCountry);
                        Assert.Equal("My hotel", model.HotelName);
                    })
                );
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<AboutUsController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());

        }
    }
}
