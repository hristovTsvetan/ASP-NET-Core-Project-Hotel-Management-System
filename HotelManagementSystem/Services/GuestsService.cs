using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
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

        public AddCustomerFormModel AddGet()
        {
            return new AddCustomerFormModel
            {
                Countries = this.db
                .Countries
                .Select(c => new CountriesViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToList(),
                Ranks = this.db
                .Ranks.Select(r => new RankViewModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList()
            };
        }

        public void AddPost(AddCustomerFormModel customer)
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
            var dBase = Sort(query);

            var allGuests = dBase
                .Where(g => g.Deleted == false)
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
                TotalPages = (int)Math.Ceiling((double)this.db.Guests.ToList().Count / query.ItemsPerPage),
                AscOrDesc = query.AscOrDesc
            };

            guestQueryModel.SortBy = query.SortBy;

            return guestQueryModel;
        }

        private IQueryable<Guest> Sort(ListGuestsQueryModel query)
        {
            switch (query.SortBy)
            {
                case SortBy.None:
                    return this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.Created);
                case SortBy.FName:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.FirstName)
                        : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.FirstName);
                case SortBy.LName:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.LastName)
                            : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.LastName);
                case SortBy.Phone:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.Phone)
                            : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.Phone);
                case SortBy.Rank:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.Rank.Name)
                            : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.Rank.Name);
                case SortBy.City:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.City.Name)
                            : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.City.Name);
                case SortBy.CreatedOn:
                    return query.AscOrDesc == 1 ? this.db.Guests.Where(g => g.Deleted == false).OrderBy(g => g.Created)
                            : this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.Created);
                default:
                    return this.db.Guests.Where(g => g.Deleted == false).OrderByDescending(g => g.Created);
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


    }
}
