using HotelManagementSystem.Models.Vouchers;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
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
        public IActionResult Edit(EditVoucherFormModel voucher)
        {
            if(!ModelState.IsValid)
            {
                return this.View(voucher);
            }

            this.vcService.UpdateVoucher(voucher);

            return RedirectToAction("All");
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

            return this.RedirectToAction("All");
        }

        public IActionResult Delete(string id)
        {
            this.vcService.Delete(id);

            return RedirectToAction("All");
        }
    }
}
