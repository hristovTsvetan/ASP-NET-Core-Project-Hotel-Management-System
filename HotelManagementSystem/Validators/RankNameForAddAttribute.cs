using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Validators
{
    public class RankNameForAddAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var guestRankService = (IGuestRanksService)validationContext.GetService(typeof(IGuestRanksService));

            if (guestRankService.IsNameExistWhenAdd(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.validateRankNameErrMsg);
            }

            return ValidationResult.Success;
        }
    }
}
