using HotelManagementSystem.Models.Vouchers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IVouchersService
    {
        Task AddVoucherAsync(AddVoucherFormModel voucher);

        IEnumerable<ListAllVouchersViewModel> GetAllVouchers();

        EditVoucherFormModel GetVoucher(string id);

        Task UpdateVoucher(EditVoucherFormModel voucher);

        Task Delete(string id);
    }
}
