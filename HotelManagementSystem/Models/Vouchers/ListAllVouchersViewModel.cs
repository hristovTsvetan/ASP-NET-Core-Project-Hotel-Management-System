using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Vouchers
{
    public class ListAllVouchersViewModel
    {
        public string Id { get; set; }

        public string Name { get; init; }

        public int Discount { get; init; }

        public string Active { get; init; }
    }
}
