using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class CountryNameEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var countryRankService = (ICountriesService)validationContext.GetService(typeof(ICountriesService));

            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string countryId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (countryRankService.IsCountryExistForEdit(value?.ToString().Trim(), countryId))
            {
                return new ValidationResult(ValidatorConstants.validateCountryNameOnEditErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
