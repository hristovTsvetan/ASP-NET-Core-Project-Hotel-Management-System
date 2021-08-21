using HotelManagementSystem.Models.Vouchers;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    [Authorize]
    public class VouchersController : Controller
    {
        private readonly IVouchersService vcService;

        public VouchersController(IVouchersService vcSrv)
        {
            this.vcService = vcSrv;
        }

        public IActionResult All()
        {
            var allVouchers = this.vcService.GetAllVouchers();

            return this.View(allVouchers);
        }

        public IActionResult Edit(string id)
        {
            var currentVaoucher = this.vcService.GetVoucher(id);

            return this.View(currentVaoucher);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVoucherFormModel voucher)
        {
            if(!ModelState.IsValid)
            {
                return this.View(voucher);
            }

            await this.vcService.UpdateVoucher(voucher);

            return RedirectToAction("All", "Vouchers");
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVoucherFormModel voucher)
        {
            if(!ModelState.IsValid)
            {
                return this.View(voucher);
            }

            await vcService.AddVoucherAsync(voucher);

            return this.RedirectToAction("All", "Vouchers");
        }

        public async Task<IActionResult> Delete(string id)
        {
            await this.vcService.Delete(id);

            return RedirectToAction("All", "Vouchers");
        }
    }
}
