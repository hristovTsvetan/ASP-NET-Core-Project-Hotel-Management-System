using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Infrastructure
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<HotelManagementDbContext>();

            data.Database.Migrate();

            SeedCountries(data);
            SeedCities(data);
            SeedRanks(data);
            SeedCompany(data);
            SeedHotel(data);
            SeedGuests(data);
            SeedRoomTypes(data);
            SeedRooms(data);

            return app;
        }

        public static void SeedGuests(HotelManagementDbContext db)
        {
            if(db.Guests.Any())
            {
                return;
            }


            var guests = new[]
            {
                new Guest { FirstName = "Tsvetan", LastName = "Hristov", Address = "Lomsko shose", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "test1@abv.bg", IdentityCardId = "45677567", Phone="64585674876", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Iva", LastName = "Hristov", Address = "Lomsko shose 1", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "iva@abv.bg", IdentityCardId = "87684563", Phone="45674567657", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Misho", LastName = "Mirchev", Address = "Street 202", City = db.Cities.FirstOrDefault(c => c.Name == "Burgas"),
                Created = DateTime.UtcNow, Email = "misho@abv.bg@abv.bg", IdentityCardId = "4564376", Phone="234523545", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Viktor", LastName = "Denchev", Address = "Banishora 2", City = db.Cities.FirstOrDefault(c => c.Name == "Varna"),
                Created = DateTime.UtcNow, Email = "viki@abv.bg", IdentityCardId = "46509808", Phone="4353425", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Teodora", LastName = "Ivanova", Address = "Avenue street 3", City = db.Cities.FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "teodora@abv.bg", IdentityCardId = "456789434876", Phone="3467435745", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Miro", LastName = "Loshev", Address = "Serdika 23", City = db.Cities.FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "Miro@abv.bg", IdentityCardId = "08906978", Phone="54637654745", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Sofia", LastName = "Petkova", Address = "Arena street 3", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "sofia@abv.bg", IdentityCardId = "4568756748", Phone="34563456", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Orlin", LastName = "Kirchev", Address = "Slatinska 101", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "Orlin@abv.bg", IdentityCardId = "6780056789", Phone="432565436456", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                  new Guest { FirstName = "Tsvetan", LastName = "Hristov", Address = "Lomsko shose", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "test1@abv.bg", IdentityCardId = "33333337654", Phone="64585674876", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Iva", LastName = "Hristov", Address = "Lomsko shose 1", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "iva@abv.bg", IdentityCardId = "589659678", Phone="45674567657", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Misho", LastName = "Mirchev", Address = "Street 202", City = db.Cities.FirstOrDefault(c => c.Name == "Burgas"),
                Created = DateTime.UtcNow, Email = "misho@abv.bg@abv.bg", IdentityCardId = "567905698", Phone="234523545", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Viktor", LastName = "Denchev", Address = "Banishora 2", City = db.Cities.FirstOrDefault(c => c.Name == "Varna"),
                Created = DateTime.UtcNow, Email = "viki@abv.bg", IdentityCardId = "567857644", Phone="4353425", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Teodora", LastName = "Ivanova", Address = "Avenue street 3", City = db.Cities.FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "teodora@abv.bg", IdentityCardId = "56799780567", Phone="3467435745", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Miro", LastName = "Loshev", Address = "Serdika 23", City = db.Cities.FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "Miro@abv.bg", IdentityCardId = "67846544876", Phone="54637654745", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Sofia", LastName = "Petkova", Address = "Arena street 3", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "sofia@abv.bg", IdentityCardId = "760567986", Phone="34563456", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Orlin", LastName = "Kirchev", Address = "Slatinska 101", City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "Orlin@abv.bg", IdentityCardId = "4568468949", Phone="432565436456", Rank = db.Ranks.FirstOrDefault(r => r.Name == "Regular")}
            };

            db.Guests.AddRange(guests);
            db.SaveChanges();
        }

        public static void SeedRanks(HotelManagementDbContext db)
        {
            if(db.Ranks.Any())
            {
                return;
            }

            db.Ranks.AddRange(
                new Rank { Name = "Regular", Discount = 0 },
                new Rank { Name = "Silver", Discount = 5 },
                new Rank { Name = "Gold", Discount = 10 },
                new Rank { Name = "Vip", Discount = 15 }
            );

            db.SaveChanges();
        }

        public static void SeedCities(HotelManagementDbContext db)
        {
            if (db.Cities.Any())
            {
                return;
            }

            db.Cities.AddRange(new[]
            {
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Bulgaria"), Name = "Sofia", PostalCode = "1000" },
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Bulgaria"), Name = "Plovdiv", PostalCode = "4000" },
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Bulgaria"), Name = "Burgas", PostalCode = "8000" },
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Bulgaria"), Name = "Varna", PostalCode = "9000" },
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Uk"), Name = "London", PostalCode = "2FF 11" },
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Uk"), Name = "Liverpool", PostalCode = "5AB 22" }
            });

            db.SaveChanges();
        }

        public static void SeedCompany(HotelManagementDbContext db)
        {
            if(db.Companies.Any())
            {
                return;
            }

            var company = new Company
            {
                Name = "Miracle LTD",
                Email = "office@miracle.com",
                City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Address = "Maria Luiza 20",
            };

            db.Companies.Add(company);
            db.SaveChanges();
        }

        public static void SeedHotel(HotelManagementDbContext db)
        {
            if (db.Hotels.Any())
            {
                return;
            }

            var hotel = new Hotel
            {
                Name = "Sunny Smiles",
                City = db.Cities.FirstOrDefault(c => c.Name == "Sofia"),
                Active = true,
                Address = "Po box 101",
                Company = db.Companies.FirstOrDefault(),
            };

            db.Hotels.Add(hotel);
            db.SaveChanges();
        }

        public static void SeedCountries(HotelManagementDbContext db)
        {
            if (db.Countries.Any())
            {
                return;
            }

            db.Countries.AddRange(new[]
            {
                new Country { Name = "Bulgaria" },
                new Country { Name = "Uk" }
            });

            db.SaveChanges();
        }

        public static void SeedRoomTypes(HotelManagementDbContext db)
        {
            if(db.RoomTypes.Any())
            {
                return;
            }

            var roomTypes = new[]
            {
                new RoomType { Name = "Single", NumberOfBeds=1, Price = 50 },
                new RoomType { Name = "Delux", NumberOfBeds=2, Price = 80 },
                new RoomType { Name = "Studio", NumberOfBeds=3, Price = 100 }
            };

            db.AddRange(roomTypes);
            db.SaveChanges();
        }

        public static void SeedRooms(HotelManagementDbContext db)
        {
            if(db.Rooms.Any())
            {
                return;
            }

            var currentActiveHotel = db.Hotels.FirstOrDefault(h => h.Active == true);
            var singleRoom = db.RoomTypes.FirstOrDefault(rt => rt.Name == "Single");
            var deluxRoom = db.RoomTypes.FirstOrDefault(rt => rt.Name == "Delux");
            var studioRoom = db.RoomTypes.FirstOrDefault(rt => rt.Name == "Studio");

            var allRooms = new[]
            {
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 101", RoomType = singleRoom },
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 102", RoomType = singleRoom },
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 103", RoomType = deluxRoom },
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 104", RoomType = deluxRoom },
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 105", RoomType = studioRoom },
                new Room{ Floor=1, Hotel = currentActiveHotel, Number= "Room 106", RoomType = singleRoom },
                new Room{ Floor=2, Hotel = currentActiveHotel, Number= "Room 201", RoomType = singleRoom },
                new Room{ Floor=2, Hotel = currentActiveHotel, Number= "Room 202", RoomType = deluxRoom  },
                new Room{ Floor=2, Hotel = currentActiveHotel, Number= "Room 203", RoomType = deluxRoom  },
                new Room{ Floor=2, Hotel = currentActiveHotel, Number= "Room 204", RoomType = studioRoom },
                new Room{ Floor=2, Hotel = currentActiveHotel, Number= "Room 205", RoomType = singleRoom },
                new Room{ Floor=3, Hotel = currentActiveHotel, Number= "Room 301", RoomType = singleRoom },
                new Room{ Floor=3, Hotel = currentActiveHotel, Number= "Room 302", RoomType = deluxRoom  },
                new Room{ Floor=3, Hotel = currentActiveHotel, Number= "Room 303", RoomType = deluxRoom  },
                new Room{ Floor=3, Hotel = currentActiveHotel, Number= "Room 304", RoomType = studioRoom },
                new Room{ Floor=3, Hotel = currentActiveHotel, Number= "Room 305", RoomType = singleRoom },
            };

            db.Rooms.AddRange(allRooms);
            db.SaveChanges();
        }
    }
}
