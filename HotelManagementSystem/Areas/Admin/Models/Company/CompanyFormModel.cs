using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Company
{
    public class CompanyFormModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        [RegularExpression(@"[0-9\s+]*", ErrorMessage = ValidatorConstants.phoneErrMsg)]
        public string Phone { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }

        [Display(Name = "Country")]
        [Required]
        public string CountryId { get; set; }

        public ICollection<SelectListItem> Cities { get; set; }

        [Display(Name="City")]
        [Required]
        public string CityId { get; set; }

        [Required]
        public string Id { get; set; }
    }
}
