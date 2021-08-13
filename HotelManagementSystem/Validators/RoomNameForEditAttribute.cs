using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Validators
{
    public class RoomNameForEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var roomService = (IRoomsService)validationContext.GetService(typeof(IRoomsService));

            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string roomId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (roomService.GetRoomNameForEdit(value?.ToString().Trim(), roomId))
            {
                return new ValidationResult(ValidatorConstants.validateRoomName);
            }

            return ValidationResult.Success;
        }
    }
}
