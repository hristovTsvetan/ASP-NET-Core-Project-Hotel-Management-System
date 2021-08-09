using HotelManagementSystem.Models.RoomsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IRoomsTypeSercvice
    {
        IEnumerable<ListRoomTypeViewModel> ListTypes();

        EditRoomTypeFormModel GetRoomType(string id);

        void Update(EditRoomTypeFormModel roomType);
    }
}
