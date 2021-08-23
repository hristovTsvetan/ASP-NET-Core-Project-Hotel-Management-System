using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Test.Moq;
using MyTested.AspNetCore.Mvc;
using Shouldly;
using System.Linq;
using Xunit;

namespace HotelManagementSystem.Test.Controllers
{
    public class RoomControllerTest
    {
        [Fact]
        public void ShouldReturnAllActiveRooms()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetHotelsWithRooms())
                .Calling(rooms => rooms.All(With.Default<ListRoomsQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<ListRoomsQueryModel>()
                    .Passing(all => 
                    all.Rooms.Count().ShouldBe(4))
                );
        }

        [Fact]
        public void ShouldReturnCorrectRoomForDetailsView()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetRoom(), GeneralMocking.GetActiveHotel())
                .Calling(m => m.Details("TestId"))
                .ShouldReturn()
                .View(v => v.WithModelOfType<DetailsRoomViewModel>()
                .Passing(room =>
                {
                    Assert.Equal("Room 101", room.Number);
                    Assert.Equal(1, room.Floor);
                }));
        }

        [Fact]
        public void ShouldReturnCorrectRoomForEditView()
        {
            MyController<RoomsController>
              .Instance()
              .WithData(GeneralMocking.GetRoom(), GeneralMocking.GetActiveHotel())
              .Calling(m => m.Edit("TestId"))
              .ShouldReturn()
              .View(v => v.WithModelOfType<EditRoomFormModel>()
              .Passing(room =>
              {
                  Assert.Equal("Room 101", room.Number);
                  Assert.Equal(1, room.Floor);
              }));
        }

        [Fact]
        public void ShouldRedirectWhenChangeRoomWhenEdit()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetRoom(), GeneralMocking.GetActiveHotel(), GeneralMocking.GetRoomType())
                .WithHttpRequest(r => r.WithPath("/Rooms/Edit/TestId"))
                .Calling(m => m.Edit(GeneralMocking.EditRoom()))
                .ShouldReturn()
                .RedirectToAction("All", "Rooms");

        }

        [Fact]
        public void ShouldRedirectIfAddIsSuccess()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetActiveHotel())
                .Calling(c => c.Add(GeneralMocking.AddRoom()))
                .ShouldReturn()
                .RedirectToAction("All", "Rooms");
        }

        [Fact]
        public void ShouldReturnViewWhenOpenAddRoomAction()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetActiveHotel(), GeneralMocking.GetRoomType())
                .Calling(c => c.Add())
                .ShouldReturn()
                .View(v => v.WithModelOfType<AddRoomFormModel>()
                    .Passing(model => 
                    {
                        Assert.Null(model.Description);
                        Assert.Null(model.Number);
                        Assert.Equal("My hotel", model.HotelName);
                    }));
        }

        [Fact]
        public void ShouldRedirectOnDelete()
        {
            MyController<RoomsController>
                .Instance()
                .WithData(GeneralMocking.GetRoom())
                .Calling(m => m.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "Rooms");
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<RoomsController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());

        }
    }
}
