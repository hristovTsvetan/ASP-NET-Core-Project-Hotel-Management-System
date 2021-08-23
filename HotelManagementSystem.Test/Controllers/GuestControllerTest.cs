using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Guests;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class GuestControllerTest
    {
        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<GuestsController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());
        }

        [Fact]
        public void ShouldReturnGuest()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetGuest())
                .Calling(rooms => rooms.All(With.Default<ListGuestsQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<ListGuestsQueryModel>()
                    .Passing(g =>
                    {
                        Assert.Equal("Tsvetan", g.AllGuests.FirstOrDefault().FirstName);
                        Assert.Equal("Hristov", g.AllGuests.FirstOrDefault().LastName);
                        Assert.Equal("Test", g.AllGuests.FirstOrDefault().RankName);
                        Assert.Equal("3453665436", g.AllGuests.FirstOrDefault().Phone);
                    }
                ));
        }

        [Fact]
        public void ShouldReturnCorrectGuestForDetailsView()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetGuest())
                .Calling(m => m.Details("TestId"))
                .ShouldReturn()
                .View(v => v.WithModelOfType<DetailsGuestViewModel>()
                .Passing(guest =>
                {
                    Assert.Equal("Tsvetan", guest.FirstName);
                    Assert.Equal("Hristov", guest.LastName);
                    Assert.Equal("3453665436", guest.Phone);
                    Assert.Equal("Test", guest.Rank);
                    Assert.Equal("34543543", guest.IdentityCardId);
                    Assert.Equal("ceci@abv.bg", guest.Email);
                }));
        }

        [Fact]
        public void ShouldRedirectOnDelete()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetGuest())
                .Calling(m => m.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "Guests");
        }

        [Fact]
        public void ShouldReturnViewWhenAddGuestAction()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetCountries())
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(v => v.WithModelOfType<AddCustomerFormModel>()
                    .Passing(model =>
                    {
                        Assert.NotNull(model.Countries);
                    }));
        }

        [Fact]
        public void ShouldRedirectIfAddIsSuccess()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetGuest(), GeneralMocking.GetCountries(), GeneralMocking.GetRank())
                .Calling(c => c.Add(GeneralMocking.AddCustomer()))
                .ShouldReturn()
                .RedirectToAction("All", "Guests");
        }

        [Fact]
        public void ShouldReturnCorrectGuestForEditView()
        {
            MyController<GuestsController>
              .Instance()
              .WithData(GeneralMocking.GetGuest(), GeneralMocking.GetCountries(), GeneralMocking.GetRank())
              .Calling(m => m.Edit("TestId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditGuestFormModel>()
              .Passing(g =>
              {
                  Assert.Equal("PoBox 101", g.Address);
                  Assert.Equal("ceci@abv.bg", g.Email);
                  Assert.Equal("Tsvetan", g.FirstName);
                  Assert.Equal("Hristov", g.LastName);
                  Assert.Equal("3453665436", g.Phone);
                  Assert.Equal("34543543", g.IdentityCardId);

              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeGuestOnEdit()
        {
            MyController<GuestsController>
                .Instance()
                .WithData(GeneralMocking.GetGuest(), GeneralMocking.GetCountries(), GeneralMocking.GetRank())
                .WithHttpRequest(r => r.WithPath("/Guest/Edit/TestId"))
                .Calling(m => m.Edit(GeneralMocking.EditGuestForm()))
                .ShouldReturn()
                .RedirectToAction("All", "Guests");
        }
    }
}
