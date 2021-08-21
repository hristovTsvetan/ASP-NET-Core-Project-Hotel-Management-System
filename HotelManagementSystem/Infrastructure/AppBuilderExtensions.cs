using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
            var services = scopedServices.ServiceProvider;

            data.Database.Migrate();

            SeedCities(data);
            SeedRanks(data);
            SeedCompany(data);
            SeedHotel(data);
            SeedGuests(data);
            SeedRoomTypes(data);
            SeedRooms(data);
            SeedRoles(data, services);
            SeedAdmin(services);
            SeedUser(services);
            SeedVouchers(data);

            return app;
        }

        public static void SeedVouchers(HotelManagementDbContext db)
        {
            if(db.Vouchers.Any())
            {
                return;
            }

            var vouchers = new[]
            {
                new Voucher { Discount = 5, Name = "Happy birthday", Active = true },
                new Voucher { Discount = 10, Name = "Best customer ever", Active = true },
                new Voucher { Discount = 7, Name = "Our gift", Active = true }
            };

            db.Vouchers.AddRange(vouchers);
            db.SaveChanges();
        }

        public static void SeedRoles(HotelManagementDbContext db, IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Admin"))
                    {
                        return;
                    }

                    var roleAdmin = new IdentityRole { Name = "Admin" };
                    var roleUser = new IdentityRole { Name = "User" };

                    await roleManager.CreateAsync(roleAdmin);
                    await roleManager.CreateAsync(roleUser);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var curUser = await userManager.FindByEmailAsync("user@hotel.bg");

                    if (curUser != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "User" };

                    const string userEmail = "user@hotel.bg";
                    const string userPassword = "654321";
                    const string userName = "Pesho Iliev";

                    var user = new User
                    {
                        Email = userEmail,
                        UserName = userEmail,
                        FullName = userName
                    };

                    await userManager.CreateAsync(user, userPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedAdmin(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var curUser =  await userManager.FindByEmailAsync("admin@hotel.bg");

                    if(curUser != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Admin" };

                    const string adminEmail = "admin@hotel.bg";
                    const string adminPassword = "123456";
                    const string fullName = "Tsvetan Hristov";

                    var user = new User
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = fullName
                    };

                    await userManager.CreateAsync(user, adminPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedGuests(HotelManagementDbContext db)
        {
            if(db.Guests.Any())
            {
                return;
            }


            var guests = new[]
            {
                new Guest { FirstName = "Tsvetan", LastName = "Hristov", Address = "Lomsko shose", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "test1@abv.bg", IdentityCardId = "45677567", Phone="64585674876", Rank = db.Ranks.OrderBy(r => r.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Iva", LastName = "Hristov", Address = "Lomsko shose 1", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "iva@abv.bg", IdentityCardId = "87684563", Phone="45674567657", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Misho", LastName = "Mirchev", Address = "Street 202", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Burgas"),
                Created = DateTime.UtcNow, Email = "misho@abv.bg@abv.bg", IdentityCardId = "4564376", Phone="234523545", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Viktor", LastName = "Denchev", Address = "Banishora 2", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Varna"),
                Created = DateTime.UtcNow, Email = "viki@abv.bg", IdentityCardId = "46509808", Phone="4353425", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Teodora", LastName = "Ivanova", Address = "Avenue street 3", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "teodora@abv.bg", IdentityCardId = "456789434876", Phone="3467435745", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Miro", LastName = "Loshev", Address = "Serdika 23", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "Miro@abv.bg", IdentityCardId = "08906978", Phone="54637654745", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Sofia", LastName = "Petkova", Address = "Arena street 3", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "sofia@abv.bg", IdentityCardId = "4568756748", Phone="34563456", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Orlin", LastName = "Kirchev", Address = "Slatinska 101", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "Orlin@abv.bg", IdentityCardId = "6780056789", Phone="432565436456", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                  new Guest { FirstName = "Tsvetan", LastName = "Hristov", Address = "Lomsko shose", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "test1@abv.bg", IdentityCardId = "33333337654", Phone="64585674876", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Iva", LastName = "Hristov", Address = "Lomsko shose 1", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "iva@abv.bg", IdentityCardId = "589659678", Phone="45674567657", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Misho", LastName = "Mirchev", Address = "Street 202", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Burgas"),
                Created = DateTime.UtcNow, Email = "misho@abv.bg@abv.bg", IdentityCardId = "567905698", Phone="234523545", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Viktor", LastName = "Denchev", Address = "Banishora 2", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Varna"),
                Created = DateTime.UtcNow, Email = "viki@abv.bg", IdentityCardId = "567857644", Phone="4353425", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Teodora", LastName = "Ivanova", Address = "Avenue street 3", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "teodora@abv.bg", IdentityCardId = "56799780567", Phone="3467435745", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Miro", LastName = "Loshev", Address = "Serdika 23", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Plovdiv"),
                Created = DateTime.UtcNow, Email = "Miro@abv.bg", IdentityCardId = "67846544876", Phone="54637654745", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Sofia", LastName = "Petkova", Address = "Arena street 3", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "sofia@abv.bg", IdentityCardId = "760567986", Phone="34563456", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")},

                new Guest { FirstName = "Orlin", LastName = "Kirchev", Address = "Slatinska 101", City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Created = DateTime.UtcNow, Email = "Orlin@abv.bg", IdentityCardId = "4568468949", Phone="432565436456", Rank = db.Ranks.OrderBy(c => c.Name).FirstOrDefault(r => r.Name == "Regular")}
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

            var bgCountry = new Country { Name = "Bulgaria" } ;
            var ukCountry = new Country { Name = "UK" };

            db.Countries.Add(bgCountry);
            db.Countries.Add(ukCountry);

            db.Cities.AddRange(new[]
            {
                new City
                {
                    Name = "Sofia",
                    PostalCode = "1000",
                    Country = bgCountry
                },
                new City
                {
                    Name = "Burgas",
                    PostalCode = "2000",
                    Country = bgCountry
                },
                new City
                {
                    Name = "Plovdiv",
                    PostalCode = "3000",
                    Country = bgCountry
                },
                new City
                {
                    Name = "Varna",
                    PostalCode = "5000",
                    Country = bgCountry
                },
                new City
                {
                    Name = "London",
                    PostalCode = "PRA 30",
                    Country = ukCountry
                }
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
                City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Address = "Maria Luiza 20",
                Phone = "+359 5743 434 543",
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
                City = db.Cities.OrderBy(c => c.Name).FirstOrDefault(c => c.Name == "Sofia"),
                Active = true,
                Address = "Po box 101",
                Company = db.Companies.OrderBy(c => c.Name).FirstOrDefault(),
                Phone = "5435 543 564",
                Email = "SunnySmiles@SunnySmiles.bg"
            };

            db.Hotels.Add(hotel);
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
                new RoomType { Name = "Single", NumberOfBeds=1, Price = 50, Image = @"https://i.pinimg.com/originals/02/ff/29/02ff29990662c919e761aa4557e8bf93.jpg" },
                new RoomType { Name = "Delux", NumberOfBeds=2, Price = 80, Image = @"https://media-cdn.tripadvisor.com/media/photo-s/0c/59/8e/d4/deluxe-room-type.jpg" },
                new RoomType { Name = "Studio", NumberOfBeds=3, Price = 100, Image = @"https://media-cdn.tripadvisor.com/media/photo-s/0c/59/8e/d4/deluxe-room-type.jpg" }
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

            var currentActiveHotel = db.Hotels.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(h => h.Active == true);
            var singleRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Single");
            var deluxRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Delux");
            var studioRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Studio");

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
