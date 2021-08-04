using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.GuestRanks
{
    public class AllRanksViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Discount { get; set; }
    }
}
