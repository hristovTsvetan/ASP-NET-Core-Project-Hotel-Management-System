using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class IsCityNameExistWhenEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var countryService = (ICountriesService)validationContext.GetService(typeof(ICountriesService));


            if (countryService.IsCountryExistForAdd(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.validateCountryNameErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
