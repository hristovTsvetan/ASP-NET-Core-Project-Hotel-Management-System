using DataLayer;
using DataLayer.Models.Enums;
using HotelManagementSystem.Models.Invoices;
using System;
using System.Linq;

namespace HotelManagementSystem.Services
{
    public class InvoicesService : IInvoicesService
    {
        private readonly HotelManagementDbContext db;
        private readonly IReservationsService rsService;

        public InvoicesService(HotelManagementDbContext dBase, IReservationsService resService)
        {
            this.db = dBase;
            this.rsService = resService;
        }

        public AllInvoicesQueryModel All(AllInvoicesQueryModel query)
        {
            var ActiveHotel = this.rsService.GetActiveHotel();

            var dbInvoices = this.db
                .Invoices
                .Where(i => i.Status != InvoiceStatus.Canceled && i.Reservation.RoomReserveds.All(r => r.Room.Hotel == ActiveHotel))
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

        public void Delete(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .FirstOrDefault(i => i.Id == id);

            var currentReservation = this.db
                .Reservations
                .FirstOrDefault(r => r.Invoice.Id == id);

            currentInvoice.Status = InvoiceStatus.Canceled;
            currentReservation.Status = ReservationStatus.Canceled;

            this.db
                .Invoices
                .Update(currentInvoice);

            this.db
                .Reservations
                .Update(currentReservation);

            this.db.SaveChanges();
        }

        public DetailsInvoiceViewModel Details(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .Where(i => i.Id == id)
                .Select(i => new DetailsInvoiceViewModel
                {
                    Status = i.Status.ToString(),
                    Price = i.Amount,
                    IssueDate = i.IssuedDate.ToString("dd-MM-yyyy"),
                    PaidDate = i.PaidDate,
                    Paid = i.Paid ? "Yes" : "No",
                    ReservationName = i.Reservation.Name,
                    GuestName = i.Reservation.Guest.FirstName + " " + i.Reservation.Guest.LastName,
                    Address = i.Reservation.Guest.Address,
                    City = i.Reservation.Guest.City.Name,
                    Country = i.Reservation.Guest.City.Country.Name,
                    Id = i.Id,
                    IdentityCard = i.Reservation.Guest.IdentityCardId
                })
                .FirstOrDefault();

            return currentInvoice;
        }

        public void Pay(string id)
        {
            var currentInvoice = this.db
                .Invoices
                .FirstOrDefault(i => i.Id == id);

            currentInvoice.Paid = true;
            currentInvoice.PaidDate = DateTime.Now.Date;
            currentInvoice.Status = InvoiceStatus.Active;

            this.db.Invoices.Update(currentInvoice);
            this.db.SaveChanges();
        }
    }
}
