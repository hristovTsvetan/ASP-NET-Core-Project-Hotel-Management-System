using HotelManagementSystem.Models.Invoices;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesService invoiceService;

        public InvoicesController(IInvoicesService iService)
        {
            this.invoiceService = iService;
        }

        public IActionResult All([FromQuery] AllInvoicesQueryModel query)
        {

            var allInvoices = invoiceService.All(query);

            return this.View(allInvoices);
        }

        public IActionResult Details(string id)
        {
            var currentInvoice = this.invoiceService.Details(id);

            return this.View(currentInvoice);
        }

        public IActionResult Delete(string id)
        {
            this.invoiceService.Delete(id);

            return this.RedirectToAction("All", "Invoices");
        }

        public IActionResult Pay(string id)
        {
            return this.RedirectToAction("All", "Invoices");
        }

    }
}
