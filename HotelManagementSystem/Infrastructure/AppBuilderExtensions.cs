using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

            return app;
        }

        public static void SeedRanks(HotelManagementDbContext db)
        {
            if(db.Ranks.Any())
            {
                return;
            }

            db.Ranks.Add(
                new Rank { Name = "Regular", Discount = 0, }
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
                new City { Country = db.Countries.FirstOrDefault(c => c.Name == "Bulgaria"), Name = "Varna", PostalCode = "9000" }
            });

            db.SaveChanges();
        }

        public static void SeedCountries(HotelManagementDbContext db)
        {
            if (db.Countries.Any())
            {
                return;
            }

            db.Countries.Add(new Country
            {
                Name = "Bulgaria"
            });

            db.SaveChanges();
        }
    }
}
