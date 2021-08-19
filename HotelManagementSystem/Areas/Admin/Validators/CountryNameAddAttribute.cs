using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class CountryNameAddAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var countryRankService = (ICountriesService)validationContext.GetService(typeof(ICountriesService));


            if (countryRankService.IsCountryExistForAdd(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.validateCountryNameErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
