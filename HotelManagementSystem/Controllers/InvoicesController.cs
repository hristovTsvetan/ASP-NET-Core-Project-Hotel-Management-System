using Microsoft.AspNetCore.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class InvoicesController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }

    }
}
