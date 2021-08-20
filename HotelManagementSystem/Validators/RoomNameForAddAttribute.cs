using HotelManagementSystem.Validators.Messages;
using HotelManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Validators
{
    public class RoomNameForAddAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var roomService = (IRoomsService)validationContext.GetService(typeof(IRoomsService));

            if (roomService.GetRoomNameForAdd(value?.ToString().Trim()))
            {
                return new ValidationResult(ValidatorConstants.validateRoomName);
            }

            return ValidationResult.Success;
        }
    }
}
