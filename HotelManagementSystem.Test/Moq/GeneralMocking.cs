using DataLayer.Models;
using DataLayer.Models.Enums;
using HotelManagementSystem.Areas.Admin.Models.Cities;
using HotelManagementSystem.Areas.Admin.Models.Company;
using HotelManagementSystem.Areas.Admin.Models.Countries;
using HotelManagementSystem.Areas.Admin.Models.Hotels;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Models.Guests;
using HotelManagementSystem.Models.Rooms;
using HotelManagementSystem.Models.RoomsType;
using HotelManagementSystem.Models.Vouchers;
using System;
using System.Collections.Generic;

namespace HotelManagementSystem.Test.Moq
{
    public class GeneralMocking
    {
        public static Hotel GetActiveHotelWithRooms()
        {
            return new Hotel
            {
                Id = "TestId",
                Name = "Sunny",
                Active = true,
                Address = "Po Box 101",
                CityId = "TestCityId",
                CompanyId = "TestCountryId",
                Phone = "345345435",
                Email = "hotel@hotel.bg",
                Rooms = new[] {
                    new Room { Floor = 2, Number = "Room 101", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 3, Number = "Room 102", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 4, Number = "Room 103", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 5, Number = "Room 104", RoomType = new RoomType{ Name = "Single" } }
                }
            };
        }

        public static Hotel GetInactivHotel()
        {
            return new Hotel
            {
                Id = "TestHotelId",
                Deleted = false,
                Name = "Sunny",
                Address = "Po Box 101",
                CityId = "TestId",
                Phone = "345345435",
                Email = "hotel@hotel.bg",
                Rooms = new[] {
                    new Room { Floor = 2, Number = "Room 101", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 3, Number = "Room 102", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 4, Number = "Room 103", RoomType = new RoomType{ Name = "Single" } },
                    new Room { Floor = 5, Number = "Room 104", RoomType = new RoomType{ Name = "Single" } }
                }
            };
        }

        public static EditHotelFormModel EditHotelFormModel()
        {
            return new EditHotelFormModel
            {
                ActiveSelection = "Yes",
                Id = "TestId",
                Name = "Best One",
                Address = "Po Box 110",
                CityId = "TestCityId",
                CountryId = "TestCountryId",
                Phone = "345636546",
                Email = "bestOne@hotel.bg",
            };
        }

        public static AddHotelFormModel AddHotelFormModel()
        {
            return new AddHotelFormModel
            {
                Name = "Sunny Way",
                Address = "Po Box 101",
                Phone = "345345435",
                Email = "hotel@hotel.bg",
                CityId = "TestCityId",
                CountryId = "TestCountryId"
            };
        }

        public static Country GetCountries()
        {
            return new Country
            {
                Id = "TestCountryId",
                Name = "Bulgaria",
                Cities = new List<City> { new City { Id = "TestCityId", Name = "Sofia", PostalCode = "1000" } }
            };
        }

        public static City GetCity()
        {
            return new City
            {
                Id = "TestId",
                CountryId = "TestCountryId",
                Name = "Burgas",
                PostalCode = "2000",
            };
        }

        public static Hotel GetActiveHotel()
        {
            return new Hotel
            {
                Id = "TestId",
                Active = true,
                Name = "My hotel",
                Address = "Po Box 101",
                City = new City { Name = "Sofia", Country = new Country { Name = "Bulgaria" } },
                Email = "hotel@hotel.bg",
                Phone = "36456546546"
            };
        }

        public static Company GetCompany()
        {
            return new Company
            {
                Id = "TestId",
                Name = "My company",
                Address = "Po Box 101",
                City = new City { Id = "TestCityId", Name = "Sofia", Country = new Country { Id = "TestCountryId", Name = "Bulgaria" } },
                Email = "company@company.bg",
                Phone = "53326345345"
            };
        }

        public static CompanyFormModel CompanyFormModel()
        {
            return new CompanyFormModel
            {
                Id = "TestId",
                CityId = "TestCityId",
                CountryId = "TestCountryId",
                Name = "My company",
                Address = "Po Box 101",
                Email = "company@company.bg",
                Phone = "53326345345"
            };
        }

        public static AddRoomFormModel AddRoom()
        {
            return new AddRoomFormModel
            {
                Floor = 2,
                Number = "505",
                HotelName = "Test",
                HotelId = "TestId",
                RoomTypeId = "TestId",
            };
        }

        public static EditRoomFormModel EditRoom()
        {
            return new EditRoomFormModel
            {
                Id = "TestId",
                Floor = 4,
                Number = "Room 402",
                HasAirCondition = "Yes",
                CurrentRoomTypeId = "TestId"
            };
        }

        public static EditRoomTypeFormModel EditRoomType()
        {
            return new EditRoomTypeFormModel
            {
                Id = "TestId",
                Name = "Delux",
                Price = 50,
                NumberOfBeds = 3
            };
        }

        public static Room GetRoom()
        {
            return new Room
            {
                Id = "TestId",
                Floor = 1,
                Number = "Room 101",
                HotelId = "TesId",
                Deleted = false,
                RoomType = new RoomType { Name = "Delux", Deleted = false }
            };
        }

        public static RoomType GetRoomType()
        {
            return new RoomType
            {
                Id = "TestId",
                Name = "Delux"
            };
        }

        public static AddRoomTypeFormModel AddRoomType()
        {
            return new AddRoomTypeFormModel
            {
                Name = "Single",
                NumberOfBeds = 1,
                Price = 60
            };
        }

        public static Voucher GetVoucher()
        {
            return new Voucher
            {
                Active = true,
                Deleted = false,
                Name = "HappyBirthDay",
                Discount = 10,
                Id = "TestId"
            };
        }

        public static EditVoucherFormModel EditVoucher()
        {
            return new EditVoucherFormModel
            {
                Name = "HappyBirthDay",
                Discount = 5,
                Id = "TestId"
            };
        }

        public static AddVoucherFormModel AddVoucher()
        {
            return new AddVoucherFormModel
            {
                Discount = 10,
                Name = "HappyBirthDay"
            };
        }

        public static Invoice GetInvoice()
        {
            return new Invoice
            {
                Amount = 200,
                Paid = true,
                Status = InvoiceStatus.Active,
                Id = "TestId",
                IssuedDate = DateTime.Now,
                PaidDate = DateTime.Now,
                ReservationId = "TestId",
                Reservation = new Reservation { Name = "Tsvetan reservation" }
            };
        }

        public static Guest GetGuest()
        {
            return new Guest
            {
                Id = "TestId",
                Address = "PoBox 101",
                City = new City { Name = "Sofia", Country = new Country { Name = "Bulgaria" } },
                Created = DateTime.Now,
                Deleted = false,
                Email = "ceci@abv.bg",
                FirstName = "Tsvetan",
                IdentityCardId = "34543543",
                LastName = "Hristov",
                Phone = "3453665436",
                Rank = new Rank { Name = "Test", Discount = 10, Deleted = false },
            };
        }

        public static Rank GetRank()
        {
            return new Rank
            {
                Id = "TestRankId",
                Discount = 10,
                Deleted = false,
                Name = "BestEver"
            };
        }

        public static AddCustomerFormModel AddCustomer()
        {
            return new AddCustomerFormModel
            {
                Address = "Po Box 101",
                CityId = "TestCityId",
                CountryId = "TestCountryId",
                Email = "test@abv.bg",
                FirstName = "Ceco",
                LastName = "Petkov",
                IdentityCardId = "34645645",
                Phone = "4563456456",
                RankId = "TestRankId"
            };
        }

        public static EditGuestFormModel EditGuestForm()
        {
            return new EditGuestFormModel
            {
                Id = "TestId",
                Address = "Po Box 101",
                CityId = "TestCityId",
                CountryId = "TestCountryId",
                Email = "test@abv.bg",
                FirstName = "Ceco",
                LastName = "Petkov",
                IdentityCardId = "34645645",
                Phone = "4563456456",
                RankId = "TestRankId"
            };
        }

        public static EditRankFormModel EditRankForm()
        {
            return new EditRankFormModel
            {
                Id = "TestRankId",
                Name = "Vip",
                Discount = 15
            };
        }

        public static AddRankFormModel AddRankForm()
        {
            return new AddRankFormModel
            {
                Discount = 10,
                Name = "BestOne Ever"
            };
        }

        public static EditCityFormModel EditCityForm()
        {
            return new EditCityFormModel
            {
                Id = "TestCityId",
                Name = "Burgas",
                PostalCode = "3000"
            };
        }

        public static AddCityFormModel AddCityForm()
        {
            return new AddCityFormModel
            {
                CountryId = "TestCountryId",
                Name = "Varna",
                PostalCode = "1000"
            };
        }

        public static EditCountryFormModel EditCountryForm()
        {
            return new EditCountryFormModel
            {
                Id = "TestCountryId",
                Name = "Norway",
            };
        }

        public static AddCountryFormModel AddCountryForm()
        {
            return new AddCountryFormModel
            {
                Name = "Norway",
            };
        }

    }
}
