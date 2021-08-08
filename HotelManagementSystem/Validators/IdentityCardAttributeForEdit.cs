using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelManagementSystem.Validators
{
    public class IdentityCardAttributeForEdit : ValidationAttribute
    {

        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var guestService = (IGuestsService)validationContext.GetService(typeof(IGuestsService));
            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string IdentityId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (guestService.IsIdentityNumExistExceptSelf(value?.ToString().Trim(), IdentityId))
            {
                return new ValidationResult(ValidatorConstants.validateIdentityId);
            }

            return ValidationResult.Success;
        }
    }
}
