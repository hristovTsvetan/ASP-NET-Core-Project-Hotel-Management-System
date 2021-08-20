using HotelManagementSystem.Areas.Admin.Models.Hotels;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class HotelsService : IHotelsService
    {
        private HotelManagementDbContext db;

        public HotelsService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public void Add(AddHotelFormModel hotel)
        {
            var company = this.db.Companies.OrderBy(c => c.Name).FirstOrDefault();

            var newHotel = new Hotel
            {
                Address = hotel.Address,
                CityId = hotel.CityId,
                Company = company,
                Email = hotel.Email,
                Image = hotel.Image,
                Name = hotel.Name,
                Phone = hotel.Phone,
            };

            this.db.Hotels.Add(newHotel);
            this.db.SaveChanges();
        }

        public HotelsQueryModel All(HotelsQueryModel query)
        {
            var allHotelsAsQuery = this.db
                .Hotels
                .Where(h => h.Deleted == false)
                .OrderByDescending(h => h.Active)
                .AsQueryable();

            var allHotels = allHotelsAsQuery
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(h => new HotelViewModel
                {
                    Address = h.Address,
                    CityName = h.City.Name,
                    Id = h.Id,
                    Image = h.Image,
                    IsActive = h.Active ? "Yes" : "No",
                    Name = h.Name,
                    RoomCount = h.Rooms.Count(),
                    Phone = h.Phone,
                    Email = h.Email
                })
                .ToList();

            var currentHotelQuery = new HotelsQueryModel
            {
                Hotels = allHotels,
                CurrentPage = query.CurrentPage,
                NextPage = query.NextPage,
                PreviousPage = query.PreviousPage,
                TotalPages = (int)Math.Ceiling((double)allHotelsAsQuery.Count() / query.ItemsPerPage)
            };

            return currentHotelQuery;
        }

        public ICollection<SelectListItem> GetCountries()
        {
            var allCountries = this.db
                .Countries
                .Where(c => c.Deleted == false)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
                .ToList();

            return allCountries;
        }

        public ICollection<SelectListItem> GetActiveList()
        {
            return new[]
            {
                new SelectListItem
                {
                    Text = "Yes",
                    Value = "Yes"
                },
                new SelectListItem
                {
                    Text = "No",
                    Value = "No"
                },
            };
        }

        public ICollection<SelectListItem> GetCities()
        {
            var allCities = this.db
                .Cities
                .Where(c => c.Deleted == false)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id
                })
                .ToList();

            return allCities;
        }

        public EditHotelFormModel GetHotel(string id)
        {
            var hotel = this.db
                .Hotels
                .Where(h => h.Id == id)
                .Select(h => new EditHotelFormModel
                {
                    ActiveSelection = h.Active ? "Yes" : "No",
                    Address = h.Address,
                    CityId = h.CityId,
                    CountryId = h.City.CountryId,
                    Email = h.Email,
                    Id = h.Id,
                    Image = h.Image,
                    Name = h.Name,
                    Phone = h.Phone,
                })
                .FirstOrDefault();

            hotel.Active = this.GetActiveList();
            hotel.Countries = this.GetCountries();
            hotel.Cities = this.GetCities();

            return hotel;
        }

        public void Edit(EditHotelFormModel hotel)
        {
            var currentHotel = this.db
                .Hotels
                .OrderBy(h => h.Name)
                .FirstOrDefault(h => h.Id == hotel.Id);

            if(hotel.ActiveSelection == "Yes")
            {
                this.ChangeHotelStatus(hotel);
            }

            currentHotel.Active = hotel.ActiveSelection == "Yes" ? true : false;
            currentHotel.Address = hotel.Address;
            currentHotel.CityId = hotel.CityId;
            currentHotel.Email = hotel.Email;
            currentHotel.Image = hotel.Image;
            currentHotel.Name = hotel.Name;
            currentHotel.Phone = hotel.Phone;

            this.db.Hotels.Update(currentHotel);
            this.db.SaveChanges();
        }

        private void ChangeHotelStatus(EditHotelFormModel hotel)
        {
            var activeHotel = this.db
                .Hotels
                .Where(h => h.Deleted == false && h.Active == true)
                .OrderBy(h => h.Name)
                .FirstOrDefault();

            if(hotel.Id != activeHotel.Id)
            {
                activeHotel.Active = false;

                this.db.Hotels.Update(activeHotel);
                this.db.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var isHotelActive = this.db
                .Hotels
                .Any(h => h.Id == id && h.Active == true);

            if(!isHotelActive)
            {
                var hotelForDelete = this.db
                    .Hotels
                    .OrderBy(h => h.Name)
                    .FirstOrDefault(h => h.Id == id);

                hotelForDelete.Deleted = true;

                this.DeleteRooms(id);
                this.DeleteActiveReservations(id);
                //this.DeleteInvoices(id);


                this.db.Hotels.Update(hotelForDelete);
                this.db.SaveChanges();
            }
        }

        private void DeleteInvoices(string id)
        {
            var invoicesForDelete = this.db
                .RoomReserveds
                .Where(rr => rr.Reservation.StartDate >= DateTime.Now.Date &&
                    rr.Room.HotelId == id &&
                    rr.Reservation.Invoice.Status != InvoiceStatus.Canceled)
                .Select(rr => rr.Reservation.Invoice)
                .ToList();

            foreach (var invoice in invoicesForDelete)
            {
                invoice.Status = InvoiceStatus.Canceled;
            }

            this.db.Invoices.UpdateRange(invoicesForDelete);
            this.db.SaveChanges();
        }

        private void DeleteActiveReservations(string id)
        {
            var reservationForDelete = this.db
                .RoomReserveds
                .Where(rr => rr.Reservation.StartDate >= DateTime.Now.Date &&
                rr.Room.HotelId == id && rr.Reservation.Status != ReservationStatus.Canceled)
                .Select(rr => rr.Reservation)
                .ToList();

            foreach (var reservation in reservationForDelete)
            {
                reservation.Status = ReservationStatus.Canceled;
            }

            this.db.Reservations.UpdateRange(reservationForDelete);
            this.db.SaveChanges();
        }

        private void DeleteRooms(string id)
        {
            var roomsForDelete = this.db
                .Rooms
                .Where(r => r.HotelId == id && r.Deleted == false)
                .ToList();

            foreach (var room in roomsForDelete)
            {
                room.Deleted = true;
            }

            this.db.Rooms.UpdateRange(roomsForDelete);
            this.db.SaveChanges();
        }
    }

    
}
