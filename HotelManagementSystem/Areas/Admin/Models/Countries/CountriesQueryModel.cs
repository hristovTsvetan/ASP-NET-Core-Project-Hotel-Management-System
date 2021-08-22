using System.Collections.Generic;

namespace HotelManagementSystem.Areas.Admin.Models.Countries
{
    public class CountriesQueryModel
    {
        public CountriesQueryModel()
        {
            this.Countries = new List<CountriesViewModel>();
            this.CurrentPage = 1;
            this.ItemsPerPage = 10;

        }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage { get; set; }

        public int NextPage { get; set; }

        public int ItemsPerPage { get; set; }

        public IEnumerable<CountriesViewModel> Countries { get; set; }
    }
}
