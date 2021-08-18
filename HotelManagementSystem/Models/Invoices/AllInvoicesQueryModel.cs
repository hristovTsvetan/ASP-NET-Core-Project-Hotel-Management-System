using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Invoices
{
    public class AllInvoicesQueryModel
    {
        public AllInvoicesQueryModel()
        {
            this.CurrentPage = 1;
            this.ItemsPerPage = 10;
        }

        public string Search { get; set; }

        public int  CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<AllInvoicesViewModel> Invoices { get; set; }

    }
}
