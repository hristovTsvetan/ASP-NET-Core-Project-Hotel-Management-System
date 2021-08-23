using HotelManagementSystem.Areas.Admin.Services;
using HotelManagementSystem.Areas.Admin.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelManagementSystem.Areas.Admin.Validators
{
    public class IsCityNameExistWhenEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var cityService = (IAdminCitiesService)validationContext.GetService(typeof(IAdminCitiesService));

            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string cityId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (cityService.IsCityExistForEdit(value?.ToString().Trim(), cityId))
            {
                return new ValidationResult(ValidatorConstants.validateCityNameErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
