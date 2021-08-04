using HotelManagementSystem.Data;
using HotelManagementSystem.Data.Models;
using HotelManagementSystem.Models.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public class VouchersService : IVouchersService
    {
        private readonly HotelManagementDbContext db;

        public VouchersService(HotelManagementDbContext dBase)
        {
            this.db = dBase;
        }

        public async Task AddVoucherAsync(AddVoucherFormModel vchr)
        {
            var voucher = new Voucher
            {
                Name = vchr.Name,
                Discount = vchr.Discount,
                Active = true,
                Deleted = false
            };

            await this.db.Vouchers.AddAsync(voucher);
            await this.db.SaveChangesAsync();

        }

        public void Delete(string id)
        {
            var vc = this.db
                .Vouchers
                .FirstOrDefault(v => v.Id == id);

            vc.Deleted = true;

            this.db.Vouchers.Update(vc);
            this.db.SaveChanges();
        }

        public IEnumerable<ListAllVouchersViewModel> GetAllVouchers()
        {
            return this.db
                .Vouchers
                .Where(v => v.Deleted == false)
                .Select(v => new ListAllVouchersViewModel
                {
                    Name = v.Name,
                    Discount = v.Discount,
                    Active = v.Active ? "Yes" : "No",
                    Id = v.Id
                })
                .ToList();
        }

        public EditVoucherFormModel GetVoucher(string id)
        {
            return this.db
                .Vouchers
                .Where(v => v.Id == id)
                .Select(v => new EditVoucherFormModel
                {
                    Id = v.Id,
                    Discount = v.Discount,
                    IsActive = v.Active,
                    Name = v.Name
                }).FirstOrDefault();
                
        }

        public void UpdateVoucher(EditVoucherFormModel voucher)
        {
            var vc = this.db
                .Vouchers
                .FirstOrDefault(v => v.Id == voucher.Id);

            vc.Name = voucher.Name;
            vc.Discount = voucher.Discount;
            vc.Active = voucher.IsActive;

            this.db.Vouchers.Update(vc);
            this.db.SaveChanges();
        }
    }
}
