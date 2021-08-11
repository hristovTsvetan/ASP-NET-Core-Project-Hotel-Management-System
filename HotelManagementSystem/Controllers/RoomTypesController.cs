using HotelManagementSystem.Models.RoomsType;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class RoomTypesController : Controller
    {
        private readonly IRoomsTypeSercvice roomTypeService;

        public RoomTypesController(IRoomsTypeSercvice rTService)
        {
            this.roomTypeService = rTService;
        }

        public IActionResult All()
        {
            var allRoomTypes = this.roomTypeService.ListTypes();

            return this.View(allRoomTypes);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddRoomTypeFormModel room)
        {
            if (!ModelState.IsValid)
            {
                return this.View(room);
            }

            this.roomTypeService.Add(room);

            return this.RedirectToAction("All", "RoomTypes");
        }

        public IActionResult Delete(string id)
        {
            this.roomTypeService.Delete(id);

            return this.RedirectToAction("All", "RoomTypes");
        }

        public IActionResult Edit(string id)
        {
            var currentType = this.roomTypeService.GetRoomType(id);

            return this.View(currentType);
        }

        [HttpPost]
        public IActionResult Edit(EditRoomTypeFormModel rType)
        {
            if(!ModelState.IsValid)
            {
                return this.View(rType);
            }

            this.roomTypeService.Update(rType);

            return this.RedirectToAction("All", "RoomTypes");
        }
    }
}
