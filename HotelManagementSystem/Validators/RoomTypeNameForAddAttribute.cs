using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Validators
{
    public class RoomTypeNameForAddAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var guestRankService = (IRoomsTypeSercvice)validationContext.GetService(typeof(IRoomsTypeSercvice));

            if (guestRankService.IsRoomExist(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.roomType);
            }

            return ValidationResult.Success;
        }
    }
}
