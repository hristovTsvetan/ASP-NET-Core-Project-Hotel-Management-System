using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Edit(EditRoomFormModel room)
        {
            if(!ModelState.IsValid)
            {
                room.RoomTypes = this.rService.GetRoomTypes();
       
                return this.View(room);
            }

            await this.rService.Update(room);

            return this.RedirectToAction("All", "Rooms");
        }

        public IActionResult Add()
        {
            var room = this.rService.FillRoomAddForm();

            return this.View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomFormModel room)
        {
            if(!ModelState.IsValid)
            { 
                var fillRoomFields = this.rService.FillRoomAddForm();

                room.HotelId = fillRoomFields.HotelId;
                room.RoomTypes = fillRoomFields.RoomTypes;
                room.HotelName = fillRoomFields.HotelName;

                return this.View(room);
            }
            
            await this.rService.Add(room);

            return this.RedirectToAction("All", "Rooms");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await rService.Delete(id);

            return this.RedirectToAction("All", "Rooms");
        }
    }
}
