﻿using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Hotels
{
    public class EditHotelFormModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }

        public ICollection<SelectListItem> Active { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string ActiveSelection { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        public string Address { get; set; }

        [Url]
        [RegularExpression(@"(http[s]*:\/\/)([a-z\-_0-9\/.]+)\.([a-z.]{2,3})\/([a-z0-9\-_\/._~:?#\[\]@!$&'()*+,;=%]*)([a-z0-9]+\.)(jpg|jpeg|png)")]
        [Display(Name="Image url")]
        public string Image { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(4, ErrorMessage = ValidatorConstants.minLength)]
        [RegularExpression(@"[0-9\s+]*", ErrorMessage = ValidatorConstants.phoneErrMsg)]
        public string Phone { get; set; }

        public ICollection<SelectListItem> Countries { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string CountryId { get; set; }

        public ICollection<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = "City")]
        public string CityId { get; set; }
    }
}
