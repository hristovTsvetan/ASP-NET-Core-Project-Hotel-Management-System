using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Invoices
{
    public class DetailsInvoiceViewModel
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string IssueDate { get; set; }

        public DateTime? PaidDate { get; set; }

        public string Paid { get; set; }

        public decimal Price { get; set; }

        public string ReservationName { get; set; }

        public string GuestName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string IdentityCard { get; set; }
    }
}
