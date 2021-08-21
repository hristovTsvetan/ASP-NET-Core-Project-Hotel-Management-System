using DataLayer;
using DataLayer.Models;
using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Infrastructure;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace HotelManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HotelManagementDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<HotelManagementDbContext>();

            services.AddControllersWithViews(option => 
            {
                option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IVouchersService, VouchersService>();
            services.AddTransient<IGuestRanksService, GuestRanksService>();
            services.AddTransient<ICitiesService, CitiesService>();
            services.AddTransient<IGuestsService, GuestsService>();
            services.AddTransient<IRoomsTypeSercvice, RoomsTypeSercvice>();
            services.AddTransient<IRoomsService, RoomsService>();
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<IInvoicesService, InvoicesService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IAdminCitiesService, AdminCitiesService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IHotelsService, HotelsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ILoginUsersService, LoginUsersService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Identity/Account/Login", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Login", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));

                endpoints.MapGet("/Identity/Account/Logout", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Logout", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));

                endpoints.MapGet("/Identity/Account/Manage", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Manage", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));

                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/", true, true)));

                endpoints.MapControllerRoute(
                name: "MyArea",
                pattern: "{area:exists}/{controller=Countries}/{action=All}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=LoginUsers}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
