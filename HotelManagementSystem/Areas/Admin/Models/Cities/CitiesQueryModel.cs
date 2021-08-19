using System.Collections.Generic;

namespace HotelManagementSystem.Areas.Admin.Models.Cities
{
    public class CitiesQueryModel
    {
        public CitiesQueryModel()
        {
            this.Cities = new List<CitiesViewModel>();
            this.CurrentPage = 1;
            this.ItemsPerPage = 5;

        }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<CitiesViewModel> Cities { get; set; }
    }
}
