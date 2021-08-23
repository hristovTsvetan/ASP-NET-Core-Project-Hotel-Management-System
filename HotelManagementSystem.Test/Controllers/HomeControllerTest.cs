using DataLayer.Models;
using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Home;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class HomeControllerTest
    {

        [Fact]
        public void TestDashboardValues()
        {
            MyController<HomeController>
                .Instance()
                .WithData(GetGuest())
                .Calling(d => d.Home())
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<HomeViewModel>()
                    .Passing(dash =>
                    {
                        dash.TotalGuests.ShouldBe(4);
                    }));
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<HomeController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());

        }


        private Guest[] GetGuest()
        {
            return new[]
            {
                new Guest { FirstName = "Pesho", LastName = "Petrov" },
                new Guest { FirstName = "Ceco", LastName = "Hristov" },
                new Guest { FirstName = "Misho", LastName = "Iliev" },
                new Guest { FirstName = "Gosho", LastName = "Petrov" }
            };
        }
    }
}
