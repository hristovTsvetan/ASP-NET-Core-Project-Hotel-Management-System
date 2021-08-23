using DataLayer.Models;
using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class GuestRankControllerTest
    {
        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<GuestRanksController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void ShouldReturnAllGuestRanks()
        {
            MyController<GuestRanksController>
                .Instance()
                .WithData(GeneralMocking.GetRank())
                .Calling(m => m.All())
                .ShouldReturn()
                .View(v => v.WithModelOfType<IEnumerable<AllRanksViewModel>>()
                    .Passing(m =>
                    {
                        Assert.Equal("BestEver", m.FirstOrDefault().Name);
                        Assert.Equal(10, m.FirstOrDefault().Discount);
                    })
                );
        }

        [Fact]
        public void ShouldReturnCorrectGuestRankForEditView()
        {
            MyController<GuestRanksController>
              .Instance()
              .WithData(GeneralMocking.GetRank())
              .Calling(m => m.Edit("TestRankId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditRankFormModel>()
              .Passing(rank =>
              {
                  Assert.Equal("BestEver", rank.Name);
                  Assert.Equal(10, rank.Discount);
              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeGuestRankWhenEdit()
        {
            MyController<GuestRanksController>
                .Instance()
                .WithData(GeneralMocking.GetRank())
                .WithHttpRequest(r => r.WithPath("/GuestRanks/Edit/TestRankId"))
                .Calling(m => m.Edit(GeneralMocking.EditRankForm()))
                .ShouldReturn()
                .RedirectToAction("All", "GuestRanks");
        }

        [Fact]
        public void ShouldRedirectOnDeleteGuestRankType()
        {
            MyController<GuestRanksController>
                .Instance()
                .WithData(GeneralMocking.GetRank())
                .Calling(d => d.Delete("TestRankId"))
                .ShouldReturn()
                .RedirectToAction("All", "GuestRanks");
        }

        [Fact]
        public void ShouldReturnViewOnAdd()
        {
            MyController<GuestRanksController>
                .Instance()
                .Calling(c => c.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectIfAddIsSuccess()
        {
            MyController<GuestRanksController>
                .Instance()
                .WithData(GeneralMocking.GetRank())
                .Calling(c => c.Add(GeneralMocking.AddRankForm()))
                .ShouldReturn()
                .RedirectToAction("All", "GuestRanks");
        }
    }
}
