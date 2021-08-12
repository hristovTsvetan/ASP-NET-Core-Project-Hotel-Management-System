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

        public IActionResult All()
        {
            var allRooms = rService.All();

            return this.View(allRooms);
        }

        public IActionResult Details(string id)
        {
            var currentRoom = this.rService.Details(id);

            return this.View(currentRoom);
        }
    }
}
