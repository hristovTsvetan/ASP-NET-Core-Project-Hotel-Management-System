using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class CountryNameEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var countryService = (ICountriesService)validationContext.GetService(typeof(ICountriesService));

            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string countryId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (countryService.IsCountryExistForEdit(value?.ToString().Trim(), countryId))
            {
                return new ValidationResult(ValidatorConstants.validateCountryNameErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
