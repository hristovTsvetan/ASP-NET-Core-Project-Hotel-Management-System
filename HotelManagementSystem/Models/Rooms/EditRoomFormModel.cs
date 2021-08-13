using HotelManagementSystem.Validators;
using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Rooms
{
    public class EditRoomFormModel
    {
        public EditRoomFormModel()
        {
            this.RoomTypes = new List<RoomTypeViewModel>();
            this.HasAirConditionCol = new List<string>
                {
                    "Yes",
                    "No"
                };

        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [RoomNameForEdit]
        public string Number { get; set; }

        [Range(1, 30)]
        public int Floor { get; set; }

        public string Description { get; set; }

        [Required]
        public List<string> HasAirConditionCol { get; set; }

        [Required]
        [Display(Name = "Has air condition")]
        public string HasAirCondition { get; set; }

        [Required]
        [Display(Name = "Current room type")]
        public string CurrentRoomTypeId { get; set; }

        [Required]
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
