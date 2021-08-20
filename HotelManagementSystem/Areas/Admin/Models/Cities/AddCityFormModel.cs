using HotelManagementSystem.Areas.Admin.Validators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Cities
{
    public class AddCityFormModel
    {
        [Required]
        [IsCityNameExistWhenAdd]
        public string Name { get; set; }

        [Required]
        [Display(Name="Postal code")]
        public string PostalCode { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }

        [Display(Name = "Country")]
        [Required]
        [IsCountryIdExist]
        public string CountryId { get; set; }
    }
}
