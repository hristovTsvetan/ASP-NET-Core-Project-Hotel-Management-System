using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class RoomsController : Controller
    {
        public IActionResult All()
        {
            return this.View();
        }
    }
}
