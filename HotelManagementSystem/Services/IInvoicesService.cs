using HotelManagementSystem.Models.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IInvoicesService
    {
        AllInvoicesQueryModel All(AllInvoicesQueryModel query);

        void Pay(string id);
    }
}
