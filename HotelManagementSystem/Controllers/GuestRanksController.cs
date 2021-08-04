using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Controllers
{
    public class GuestRanksController : Controller
    {
        private readonly IGuestRanksService guestRankService;

        public GuestRanksController(IGuestRanksService guestRService)
        {
            this.guestRankService = guestRService;
        }

        public IActionResult All()
        {
            var ranks = this.guestRankService.GetAllRanks();

            return this.View(ranks);
        }

        public IActionResult Edit(string id)
        {
            var rank = this.guestRankService.GetRank(id);

            return this.View(rank);
        }

        [HttpPost]
        public IActionResult Edit(EditRankFormModel rank)
        {
            if(!ModelState.IsValid)
            {
                return this.View(rank);
            }

            this.guestRankService.Edit(rank);

            return this.RedirectToAction("All", "GuestRanks");
        }

        public IActionResult Delete(string id)
        {
            this.guestRankService.Delete(id);

            return this.RedirectToAction("All", "GuestRanks");
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRankFormModel rank)
        {
            if (!ModelState.IsValid)
            {
                return this.View(rank);
            }

            await this.guestRankService.Add(rank);

            return this.RedirectToAction("All", "GuestRanks");
        }


    }
}
