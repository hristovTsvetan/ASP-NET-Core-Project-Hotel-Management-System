using DataLayer.Models;
using HotelManagementSystem.Controllers;
using HotelManagementSystem.Models.Rooms;
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
                .WithData(GetActiveRooms())
                .Calling(rooms => rooms.All(With.Default<ListRoomsQueryModel>()))
                .ShouldReturn()
                .View(v => v.WithModelOfType<ListRoomsViewModel>()
                            .Passing(all =>
                            {
                                
                            }));
                
                
        }

        private Room[] GetActiveRooms()
        {
            var currentHotel = new Hotel { Active = true };

            return new[]
            {
                new Room { Deleted = false, Number = "Room 101", Hotel = currentHotel  },
                new Room { Deleted = true, Number = "Room 102"  },
                new Room { Deleted = false, Number = "Room 103", Hotel = currentHotel },
                new Room { Deleted = false, Number = "Room 104"  },
                new Room { Deleted = false, Number = "Room 105"  },
            };
        }
    }
}
