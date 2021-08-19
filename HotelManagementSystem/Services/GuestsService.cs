using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Data.Models.Enums;
using HotelManagementSystem.Models.Countries;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Models.Guests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class GuestsService : IGuestsService
    {
        private readonly HotelManagementDbContext db;

        public GuestsService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public IEnumerable<CountriesViewModel> GetCountries()
        {
            return this.db
                .Countries
                .Select(c => new CountriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList();
        }

        public IEnumerable<RankViewModel> GetRanks()
        {
            return this.db
                .Ranks.Select(r => new RankViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
        }

        public void Add(AddCustomerFormModel customer)
        {
            var guest = new Guest
            {
                Address = customer.Address,
                CityId = customer.CityId,
                Details = customer.Details,
                Email = customer.Email,
                FirstName = customer.FirstName,
                IdentityCardId = customer.IdentityCardId,
                LastName = customer.LastName,
                Phone = customer.Phone,
                RankId = customer.RankId,
                Created = DateTime.UtcNow
            };

            this.db.Guests.Add(guest);
            this.db.SaveChanges();
        }

        public ListGuestsQueryModel GetGuests(ListGuestsQueryModel query)
        {
            var dBase = this.db.Guests.Where(g => g.Deleted == false);

            dBase = Search(query, dBase);
            dBase = Sort(query, dBase);

            var allGuests = dBase
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(g => new ListGuestsViewModel
                {
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    RankName = g.Rank.Name,
                    Id = g.Id,
                    Created = g.Created.Date,
                    City = g.City.Name
                })
                .ToList();

            var guestQueryModel = new ListGuestsQueryModel
            {
                CurrentPage = query.CurrentPage,
                AllGuests = allGuests,
                TotalPages = (int)Math.Ceiling((double)dBase.ToList().Count / query.ItemsPerPage),
                AscOrDesc = query.AscOrDesc
            };

            guestQueryModel.SortBy = query.SortBy;
            guestQueryModel.Search = query.Search;

            return guestQueryModel;
        }

        private IQueryable<Guest> Search(ListGuestsQueryModel query, IQueryable<Guest> currentDb)
        {
            if (string.IsNullOrWhiteSpace(query.Search))
            {
                return currentDb;
            }

            return currentDb
                .Where(g => (g.FirstName.ToLower() + " " + g.LastName.ToLower()).Contains(query.Search.ToLower()) ||
                        g.IdentityCardId.ToLower().Contains(query.Search.ToLower()) || g.Phone.ToLower().Contains(query.Search.ToLower()) ||
                        g.Rank.Name.ToLower().Contains(query.Search.ToLower()) || g.Email.ToLower().Contains(query.Search.ToLower()) ||
                        g.City.Name.ToLower().Contains(query.Search.ToLower()) || g.Address.ToLower().Contains(query.Search.ToLower()) ||
                        g.City.Country.Name.ToLower().Contains(query.Search.ToLower()));

        }

        private IQueryable<Guest> Sort(ListGuestsQueryModel query, IQueryable<Guest> dbase)
        {
            switch (query.SortBy)
            {
                case SortBy.None:
                    return dbase.OrderByDescending(g => g.Created);
                case SortBy.FName:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.FirstName)
                        : dbase.OrderByDescending(g => g.FirstName);
                case SortBy.LName:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.LastName)
                            : dbase.OrderByDescending(g => g.LastName);
                case SortBy.Phone:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.Phone)
                            : dbase.OrderByDescending(g => g.Phone);
                case SortBy.Rank:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.Rank.Name)
                            : dbase.OrderByDescending(g => g.Rank.Name);
                case SortBy.City:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.City.Name)
                            : dbase.OrderByDescending(g => g.City.Name);
                case SortBy.CreatedOn:
                    return query.AscOrDesc == 1 ? dbase.OrderBy(g => g.Created)
                            : dbase.OrderByDescending(g => g.Created);
                default:
                    return dbase.OrderByDescending(g => g.Created);
            }

        }

        public bool IsCityExist(string cityId)
        {
            return this.db
                .Cities
                .Any(c => c.Id == cityId);
        }

        public bool IsCountryExist(string countryId)
        {
            return this.db
                .Countries
                .Any(c => c.Id == countryId);
        }

        public bool IsIdentityNumberExist(string identityNumber)
        {
            return this.db.Guests.Any(g => g.IdentityCardId == identityNumber);
        }

        public bool IsRankExist(string rankId)
        {
            return this.db
                .Ranks
                .Any(r => r.Id == rankId);
        }

        public DetailsGuestViewModel Details(string id)
        {
            return this.db.Guests
                .Where(g => g.Id == id)
                .Select(g => new DetailsGuestViewModel
                {
                    FirstName = g.FirstName,
                    Address = g.Address,
                    City = g.City.Name,
                    Country = g.City.Country.Name,
                    Created = g.Created.ToString("dd-MM-yyyy"),
                    CreatedReservationsCount = g.Reservations.Where(r => r.Status == ReservationStatus.Confirmed).Count(),
                    Details = g.Details,
                    Email = g.Email,
                    IdentityCardId = g.IdentityCardId,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    Rank = g.Rank.Name,
                    Id = g.Id,
                }).FirstOrDefault();
        }

        public void Delete(string id)
        {
            ChangeReservationStatus(id);

            var guest = this.db.Guests.Where(g => g.Id == id).FirstOrDefault();

            guest.Deleted = true;

            this.db.Update(guest);
            this.db.SaveChanges();
        }

        public void ChangeReservationStatus(string id)
        {
            this.db.
                Guests
                .Where(g => g.Id == id);


            var reservations = this.db
                .Reservations
                .Where(r => r.GuestId == id && r.StartDate >= DateTime.Now.Date)
                .ToList();

            foreach(var res in reservations)
            {
                res.Status = ReservationStatus.Canceled;
            }

            this.db.Reservations.UpdateRange(reservations);
            this.db.SaveChanges();
        }

        public EditGuestFormModel GetGuest(string id)
        {
            var editGuest = this.db
                .Guests
                .Where(g => g.Id == id)
                .Select(g => new EditGuestFormModel
                {
                    Address = g.Address,
                    CityId = g.CityId,
                    Details = g.Details,
                    Email = g.Email,
                    FirstName = g.FirstName,
                    Id = g.Id,
                    IdentityCardId = g.IdentityCardId,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    CountryId = g.City.CountryId,
                    RankId = g.RankId
                })
                .FirstOrDefault();

            editGuest.Countries = this.GetCountries();
            editGuest.Ranks = this.GetRanks();

            return editGuest;
        }

        public bool IsIdentityNumExistExceptSelf(string identityNumber, string id)
        {
            var dBase =this.db
                .Guests
                .Where(g => g.Id != id && g.Deleted == false)
                .AsQueryable();

            return dBase.Any(g => g.IdentityCardId == identityNumber);
        }

        public void Edit(EditGuestFormModel guest)
        {
            var currentGuest = this.db
                .Guests.FirstOrDefault(g => g.Id == guest.Id);

            currentGuest.FirstName = guest.FirstName;
            currentGuest.Address = guest.Address;
            currentGuest.CityId = guest.CityId;
            currentGuest.Details = guest.Details;
            currentGuest.Email = guest.Email;
            currentGuest.IdentityCardId = guest.IdentityCardId;
            currentGuest.LastName = guest.LastName;
            currentGuest.Phone = guest.Phone;
            currentGuest.RankId = guest.RankId;

            this.db.Update(currentGuest);
            this.db.SaveChanges();
        }
    }
}
