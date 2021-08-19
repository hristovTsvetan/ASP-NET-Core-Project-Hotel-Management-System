using HotelManagementSystem.Areas.Admin.Models.Cities;
using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelManagementSystem.Areas.Admin.Services
{
    public class AdminCitiesService : IAdminCitiesService
    {
        private readonly HotelManagementDbContext db;

        public AdminCitiesService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public void Add(AddCityFormModel city)
        {
            var newCity = new City
            {
                CountryId = city.CountryId,
                Name = city.Name,
                PostalCode = city.PostalCode
            };

            this.db.Cities.Add(newCity);
            this.db.SaveChanges();
        }

        public CitiesQueryModel All(CitiesQueryModel query)
        {
            var citiesQueryDb = this.db
                .Cities
                .Where(c => c.Deleted == false)
                .AsQueryable();

            if(!string.IsNullOrWhiteSpace(query.Search))
            {
                citiesQueryDb = citiesQueryDb
                               .Where(c => c.Name.ToLower().Contains(query.Search.ToLower()) ||
                               c.Country.Name.ToLower().Contains(query.Search.ToLower()));
            }

            var allCities = citiesQueryDb
                .OrderBy(c => c.Name)
                .Skip((query.CurrentPage - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .Select(c => new CitiesViewModel
                {
                    Country = c.Country.Name,
                    Name = c.Name,
                    Id = c.Id
                })
                .ToList();

            var cQueryModel = new CitiesQueryModel
            {
                Cities = allCities,
                CurrentPage = query.CurrentPage,
                NextPage = query.NextPage,
                PreviousPage = query.PreviousPage,
                Search = query.Search,
                TotalPages = (int)Math.Ceiling((double)citiesQueryDb.Count() / query.ItemsPerPage)
            };

            return cQueryModel;
        }

        public void Edit(EditCityFormModel city)
        {
            var curCity = this.db
                .Cities
                .FirstOrDefault(c => c.Id == city.Id);

            curCity.Name = city.Name;
            curCity.PostalCode = city.PostalCode;

            this.db.Update(curCity);
            this.db.SaveChanges();
        }

        public bool IsCityExistForEdit(string name, string id)
        {
            return this.db
                .Cities
                .Where(c => c.Id != id)
                .Any(c => c.Deleted == false && c.Name == name);
        }

        public AddCityFormModel LoadCountries()
        {
            var allCountries = this.db
                .Countries
                .Where(c => c.Deleted == false)
                .Select(c => new SelectListItem
                {
                    Value = c.Id,
                    Text = c.Name
                })
                .ToList();

            var cityFormModel = new AddCityFormModel
            {
                Countries = allCountries
            };

            return cityFormModel;
        }

        public EditCityFormModel LoadCity(string id)
        {
            return this.db
                .Cities
                .Where(c => c.Id == id)
                .Select(c => new EditCityFormModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    PostalCode = c.PostalCode
                })
                .FirstOrDefault();
        }

        public void Delete(string id)
        {
            var city = this.db
                .Cities
                .Where(c => c.Id == id)
                .FirstOrDefault();

            city.Deleted = true;

            this.db.Cities.Update(city);
            this.db.SaveChanges();
        }
    }
}
