using HotelManagementSystem.Models.GuestRanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IGuestRanksService
    {
        Task Edit(EditRankFormModel rank);

        EditRankFormModel GetRank(string id);

        IEnumerable<AllRanksViewModel> GetAllRanks();

        Task Add(AddRankFormModel rank);

        Task Delete(string id);

        bool IsNameExistWhenAdd(string name);

        bool IsNameExistWhenEdit(string name, string id);
    }
}
