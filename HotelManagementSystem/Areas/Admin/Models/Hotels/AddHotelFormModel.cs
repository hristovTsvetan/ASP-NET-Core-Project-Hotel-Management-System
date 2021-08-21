using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Hotels
{
    public class AddHotelFormModel
    {
        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        [RegularExpression(@"[0-9\s+]*", ErrorMessage = ValidatorConstants.phoneErrMsg)]
        public string Phone { get; set; }

        public ICollection<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = "City")]
        public string CityId { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name ="Country")]
        public string CountryId { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        public string Address { get; set; }

        [Url]
        [Display(Name ="Image url")]
        public string Image { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
