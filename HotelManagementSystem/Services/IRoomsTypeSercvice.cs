using HotelManagementSystem.Models.RoomsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IRoomsTypeSercvice
    {
        ListRoomTypeQueryModel ListTypes(ListRoomTypeQueryModel rTQuery);

        EditRoomTypeFormModel GetRoomType(string id);

        Task Update(EditRoomTypeFormModel roomType);

        Task Add(AddRoomTypeFormModel roomType);

        bool IsRoomNameExistForAdd(string name);

        bool IsRoomNameExistForEdit(string name, string id);

        Task Delete(string id);
    }
}
