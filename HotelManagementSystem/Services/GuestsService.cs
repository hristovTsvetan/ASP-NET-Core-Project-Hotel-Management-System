using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.Countries;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Models.Guests;
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

        public IEnumerable<ListGuestsViewModel> GetGuests()
        {
            return this.db
                .Guests
                .Where(g => g.Deleted == false)
                .Select(g => new ListGuestsViewModel
                {
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Phone = g.Phone,
                    RankName = g.Rank.Name,
                    Id = g.Id,
                    Created = g.Created
                })
                .OrderByDescending(g => g.Created)
                .ToList();
                
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
