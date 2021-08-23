using MyTested.AspNetCore.Mvc;
using Xunit;
using HotelManagementSystem.Controllers;
using HotelManagementSystem.Test.Moq;
using HotelManagementSystem.Models.RoomsType;
using System.Linq;
using DataLayer.Models;

namespace HotelManagementSystem.Test.Controllers
{
    public class RoomTypeControllerTest
    {
        [Fact]
        public void ShouldReturnAllRoomTypes()
        {
            MyController<RoomTypesController>
                .Instance()
                .WithData(GeneralMocking.GetRoomType())
                .Calling(m => m.All(With.Default<ListRoomTypeQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<ListRoomTypeQueryModel>()
                    .Passing(m =>
                    {
                        Assert.Equal("Delux", m.RoomTypes.FirstOrDefault().Name);
                    })
                );
        }

        [Fact]
        public void ShouldReturnViewOnAdd()
        {
            MyController<RoomTypesController>
                .Instance()
                .Calling(m => m.Add())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void ShouldRedirectOnAddRoomType()
        {
            MyController<RoomTypesController>
                .Instance()
                .WithData(new RoomType())
                .Calling(m => m.Add(GeneralMocking.AddRoomType()))
                .ShouldReturn()
                .RedirectToAction("All", "RoomTypes");
        }

        [Fact]
        public void ShouldRedirectOnDeleteRoomType()
        {
            MyController<RoomTypesController>
                .Instance()
                .WithData(GeneralMocking.GetRoomType())
                .Calling(d => d.Delete("TestId"))
                .ShouldReturn()
                .RedirectToAction("All", "RoomTypes");
        }

        [Fact]
        public void ShouldLoadRoomTypeNameWhenEdit()
        {
            MyController<RoomTypesController>
                .Instance()
                .WithData(GeneralMocking.GetRoomType())
                .Calling(m => m.Edit("TestId"))
                .ShouldReturn()
                .View(v => v.WithModelOfType<EditRoomTypeFormModel>()
                .Passing(v =>
                   Assert.Equal("Delux", v.Name)
                 ));
        }

        [Fact]
        public void ShouldRedirectWhenRoomTypeIsEdit()
        {
            MyController<RoomTypesController>
                .Instance()
                .WithData(GeneralMocking.GetRoomType())
                .WithHttpRequest(r => r.WithPath("/RoomTypes/Edit/TestId"))
                .Calling(m => m.Edit(GeneralMocking.EditRoomType()))
                .ShouldReturn()
                .RedirectToAction("All", "RoomTypes");
        }

        [Fact]
        public void ShoulHaveAuthorizeAttribute()
        {
            MyController<RoomTypesController>
                .Instance()
                .ShouldHave()
                .Attributes(attr => attr.RestrictingForAuthorizedRequests());

        }
    }
}
