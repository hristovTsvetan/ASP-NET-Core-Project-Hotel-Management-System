using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class IsCountryIdExistAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var countryService = (ICountriesService)validationContext.GetService(typeof(ICountriesService));


            if (!countryService.IsCountryIdExist(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.validateCountryIdErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
