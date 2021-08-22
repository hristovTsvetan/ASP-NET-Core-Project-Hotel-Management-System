using HotelManagementSystem.Validators;
using HotelManagementSystem.Validators.Messages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Guests
{
    public class AddCustomerFormModel
    {

        public AddCustomerFormModel()
        {
            this.Countries = new List<SelectListItem>();
            this.Ranks = new List<SelectListItem>();
        }

        [Display(Name ="First Name")]
        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        public string LastName { get; set; }

        [Display(Name = "Identity card number")]
        [Required]
        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [IdentityCard]
        public string IdentityCardId { get; set; }

        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"[0-9\s+]*", ErrorMessage = ValidatorConstants.phone)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(5, ErrorMessage = ValidatorConstants.minLength)]
        public string Address { get; set; }

        public string Details { get; set; }

        [CountryId]
        [Display(Name = "Country")]
        [Required]
        public string CountryId { get; set; }

        [CityId]
        [Display(Name = "City")]
        [Required]
        public string CityId { get; set; }

        [RankId]
        [Display(Name = "Rank")]
        [Required]
        public string RankId { get; set; }

        public ICollection<SelectListItem> Ranks { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }
    }
}
