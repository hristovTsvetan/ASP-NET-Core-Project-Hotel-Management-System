using System.Collections.Generic;

namespace HotelManagementSystem.Areas.Admin.Models.Hotels
{
    public class HotelsQueryModel
    {
        public HotelsQueryModel()
        {
            this.Hotels = new List<HotelViewModel>();
            this.CurrentPage = 1;
            this.ItemsPerPage = 3;
        }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int TotalPages { get; set; }

        public IEnumerable<HotelViewModel> Hotels { get; set; }
    }
}
