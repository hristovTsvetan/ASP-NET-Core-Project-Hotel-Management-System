using HotelManagementSystem.Models.Rooms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IRoomsService
    {
        ListRoomsQueryModel All(ListRoomsQueryModel rooms);

        DetailsRoomViewModel Details(string id);

        EditRoomFormModel Edit(string id);

        bool GetRoomNameForEdit(string name, string id);

        bool GetRoomNameForAdd(string id);

        IEnumerable<RoomTypeViewModel> GetRoomTypes();

        Task Update(EditRoomFormModel room);

        Task Add(AddRoomFormModel room);

        AddRoomFormModel FillRoomAddForm();

        Task Delete(string id);

    }
}
