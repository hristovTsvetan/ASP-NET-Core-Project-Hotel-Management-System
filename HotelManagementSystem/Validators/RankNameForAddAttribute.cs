using HotelManagementSystem.Data;
using HotelManagementSystem.Models.GuestRanks;
using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
