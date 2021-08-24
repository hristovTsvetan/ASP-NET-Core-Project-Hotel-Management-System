using DataLayer;
using DataLayer.Models;
using DataLayer.Models.Enums;
using HotelManagementSystem.Areas.Admin.Models.Hotels;
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

        public async Task Add(AddHotelFormModel hotel)
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

            await this.db.Hotels.AddAsync(newHotel);
            await this.db.SaveChangesAsync();
        }

        public HotelsQueryModel All(HotelsQueryModel query)
        {
            var allHotelsAsQuery = this.db
                .Hotels
                .Where(h => h.Deleted == false)
                .OrderByDescending(h => h.Active)
                .AsQueryable();

            var tPages = (int)Math.Ceiling((double)allHotelsAsQuery.Count() / query.ItemsPerPage);

            if(query.CurrentPage > tPages)
            {
                query.CurrentPage = tPages;
            }

            if(query.CurrentPage <= 0)
            {
                query.CurrentPage = 1;
            }

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
                TotalPages = tPages
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

        public async Task Edit(EditHotelFormModel hotel)
        {
            var allHotels = this.db
                .Hotels
                .Where(h => h.Deleted == false)
                .OrderBy(h => h.Name)
                .ToList();

            if(allHotels.Count <= 1 && hotel.ActiveSelection == "No")
            {
                return;
            }

            var currentHotel = allHotels
                .FirstOrDefault(h => h.Id == hotel.Id);

            if(hotel.ActiveSelection == "Yes")
            {
                await this.ChangeHotelStatus(hotel);
            }

            currentHotel.Active = hotel.ActiveSelection == "Yes" ? true : false;
            currentHotel.Address = hotel.Address;
            currentHotel.CityId = hotel.CityId;
            currentHotel.Email = hotel.Email;
            currentHotel.Image = hotel.Image;
            currentHotel.Name = hotel.Name;
            currentHotel.Phone = hotel.Phone;

            this.db.Hotels.Update(currentHotel);
            await this.db.SaveChangesAsync();
        }

        private async Task ChangeHotelStatus(EditHotelFormModel hotel)
        {
            var activeHotel = this.db
                .Hotels
                .Where(h => h.Deleted == false && h.Active == true)
                .OrderBy(h => h.Name)
                .FirstOrDefault();

                if (hotel.Id != activeHotel.Id)
                {
                    activeHotel.Active = false;

                    this.db.Hotels.Update(activeHotel);
                    await this.db.SaveChangesAsync();
                }

        }

        public async Task Delete(string id)
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

                await this.DeleteRooms(id);
                await this.DeleteActiveReservations(id);

                this.db.Hotels.Update(hotelForDelete);
                await this.db.SaveChangesAsync();
            }
        }

        private async Task DeleteActiveReservations(string id)
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
            await this.db.SaveChangesAsync();
        }

        private async Task DeleteRooms(string id)
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
            await this.db.SaveChangesAsync ();
        }
    }

    
}
