using Xunit;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using HotelManagementSystem.Models.Home;
using Shouldly;
using HotelManagementSystem.Controllers;
using DataLayer.Models;

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
        public void HomeControllerShouldReturnView()
        {
            MyController<HomeController>
                .Instance()
                .Calling(d => d.Home())
                .ShouldReturn()
                .View();
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
