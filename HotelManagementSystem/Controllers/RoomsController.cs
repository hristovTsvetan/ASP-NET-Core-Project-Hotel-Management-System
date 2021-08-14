using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsService rService;

        public RoomsController(IRoomsService roomsService)
        {
            this.rService = roomsService;
        }

        public IActionResult All([FromQuery] ListRoomsQueryModel rms)
        {
            var allRooms = rService.All(rms);

            return this.View(allRooms);
        }

        public IActionResult Details(string id)
        {
            var currentRoom = this.rService.Details(id);

            return this.View(currentRoom);
        }

        public IActionResult Edit(string id)
        {
            var room = this.rService.Edit(id);

            return this.View(room);
        }

        [HttpPost]
        public IActionResult Edit(EditRoomFormModel room)
        {
            if(!ModelState.IsValid)
            {
                room.RoomTypes = this.rService.GetRoomTypes();
       
                return this.View(room);
            }

            this.rService.Update(room);

            return this.RedirectToAction("All", "Rooms");
        }

        public IActionResult Add()
        {
            var room = this.rService.FillRoomAddForm();

            return this.View(room);
        }
    }
}
