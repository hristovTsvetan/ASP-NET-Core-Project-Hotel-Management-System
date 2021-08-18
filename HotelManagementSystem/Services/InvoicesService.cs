using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models.Enums;
using HotelManagementSystem.Models.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly HotelManagementDbContext db;

        public InvoicesService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public AllInvoicesQueryModel All(AllInvoicesQueryModel query)
        {
            var dbInvoices = this.db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled)
                .Select(o => new
                {
                    Name = o.Reservation.Name,
                    Id = o.Id,
                    Paid = o.Paid ? "Yes" : "No",
                    Price = o.Amount,
                    Status = o.Status.ToString(),
                    IssuedDate = o.IssuedDate
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                dbInvoices = dbInvoices
                    .Where(i => i.Name.ToLower().Contains(query.Search.ToLower()) ||
                        i.Paid.ToLower().Contains(query.Search.ToLower()) ||
                        i.Status.ToLower().Contains(query.Search.ToLower()))
                    .ToList();
            }


            var allInvoices = dbInvoices
                .OrderByDescending(i => i.IssuedDate)
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(i => new AllInvoicesViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    Paid = i.Paid,
                    Price = i.Price,
                    Status = i.Status,
                })
                .ToList();

            var invoicesQueryModel = new AllInvoicesQueryModel
            {
                Invoices = allInvoices,
                Search = query.Search,
                CurrentPage = query.CurrentPage,
                TotalPages = (int)Math.Ceiling((double)dbInvoices.Count() / query.ItemsPerPage),
                NextPage = query.NextPage,
                PreviousPage = query.PreviousPage
            };

            return invoicesQueryModel;
        }

        public void Pay(string id)
        {
            throw new NotImplementedException();
        }
    }
}
