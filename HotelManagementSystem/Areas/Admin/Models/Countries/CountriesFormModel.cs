using HotelManagementSystem.Areas.Admin.Validators;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Models.Countries
{
    public class CountriesFormModel
    {
        public string Id { get; set; }

        [Required]
        [CountryNameEdit]
        [MinLength(3, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        public string Name { get; set; }
    }
}
