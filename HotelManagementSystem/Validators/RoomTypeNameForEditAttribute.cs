using HotelManagementSystem.Services;
using HotelManagementSystem.Validators.Messages;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelManagementSystem.Validators
{
    public class RoomTypeNameForEditAttribute : ValidationAttribute
    {
        public override bool RequiresValidationContext { get { return true; } }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var roomTypeService = (IRoomsTypeSercvice)validationContext.GetService(typeof(IRoomsTypeSercvice));

            var httpAccesor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            string rTyprId = httpAccesor.HttpContext.Request.RouteValues.Values.Last().ToString();

            if (roomTypeService.IsRoomNameExistForEdit(value?.ToString().Trim(), rTyprId))
            {
                return new ValidationResult(ValidatorConstants.roomType);
            }

            return ValidationResult.Success;
        }
    }
}
