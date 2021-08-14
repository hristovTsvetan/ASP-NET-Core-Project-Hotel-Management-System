using HotelManagementSystem.Models.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IRoomsService
    {
        ListRoomsQueryModel All(ListRoomsQueryModel rooms);

        DetailsRoomViewModel Details(string id);

        EditRoomFormModel Edit(string id);

        bool GetRoomNameForEdit(string name, string id);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        void Update(EditRoomFormModel room);

        void Add(AddRoomFormModel room);

        AddRoomFormModel FillRoomAddForm();

    }
}
