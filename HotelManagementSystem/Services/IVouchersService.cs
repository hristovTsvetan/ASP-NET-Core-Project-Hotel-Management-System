using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementSystem.Models.Vouchers;

namespace HotelManagementSystem.Services
{
    public interface IVouchersService
    {
        Task AddVoucherAsync(AddVoucherFormModel voucher);

        IEnumerable<ListAllVouchersViewModel> GetAllVouchers();

        EditVoucherFormModel GetVoucher(string id);

        void UpdateVoucher(EditVoucherFormModel voucher);

        void Delete(string id);
    }
}
