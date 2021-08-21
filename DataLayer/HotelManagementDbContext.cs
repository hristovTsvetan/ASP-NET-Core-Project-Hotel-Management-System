using DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class HotelManagementDbContext : IdentityDbContext<User>
    {
        public HotelManagementDbContext(DbContextOptions<HotelManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Guest> Guests { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Rank> Ranks { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomReserved> RoomReserveds { get; set; }

        public DbSet<RoomType> RoomTypes { get; set; }

        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RoomReserved>(r =>
            {
                r.HasKey(k => new { k.ReservationId, k.RoomId });

                r.HasOne(ro => ro.Room)
                .WithMany(rr => rr.RoomReserveds)
                .HasForeignKey(ro => ro.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Hotel>(h =>
            {
                h.HasOne(c => c.City)
                .WithMany(ho => ho.Hotels)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

                h.HasOne(c => c.Company)
                .WithMany(ho => ho.Hotels)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Company>()
                .HasOne(s => s.City)
                .WithMany(c => c.Companies)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Guest>(g =>
            {
                g.HasOne(c => c.City)
                .WithMany(gu => gu.Guests)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict);

                g.HasOne(r => r.Rank)
                .WithMany(gu => gu.Guests)
                .HasForeignKey(r => r.RankId)
                .OnDelete(DeleteBehavior.Restrict);
            });


            builder.Entity<Reservation>(r =>
            {
                r.HasOne(g => g.Guest)
                .WithMany(re => re.Reservations)
                .HasForeignKey(g => g.GuestId)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(re => re.Voucher)
                .WithMany(v => v.Reservations)
                .HasForeignKey(re => re.VoucherId)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(res => res.Invoice)
                .WithOne(i => i.Reservation)
                .HasForeignKey<Invoice>(i => i.ReservationId);
            });

            builder.Entity<Room>(r =>
            {
                r.HasOne(h => h.Hotel)
                .WithMany(ro => ro.Rooms)
                .HasForeignKey(h => h.HotelId)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(ru => ru.RoomType)
                .WithMany(ro => ro.Rooms)
                .HasForeignKey(ru => ru.RoomTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            });
                

            base.OnModelCreating(builder);
        }
    }
}
