using HotelManagementSystem.Validators;
using HotelManagementSystem.Validators.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Rooms
{
    public class AddRoomFormModel
    {
        public AddRoomFormModel()
        {
            this.RoomTypes = new List<RoomTypeViewModel>();
            this.HasAirConditionCol = new List<string>
                {
                    "Yes",
                    "No"
                };
        }

        [Required]
        [MinLength(1, ErrorMessage = ValidatorConstants.minLength)]
        [MaxLength(30, ErrorMessage = ValidatorConstants.maxLength)]
        [RoomNameForEdit]
        public string Number { get; set; }

        [Range(1, 30)]
        [Required]
        public int Floor { get; set; }

        public string Description { get; set; }

        [Display(Name = "Has air condition")]
        public string HasAirCondition { get; set; }

        [Required]
        public List<string> HasAirConditionCol { get; set; }

        [Required]
        [Display(Name = "Hotel")]
        public string HotelId { get; set; }

        [Required]
        public string HotelName { get; set; }

        [Required]
        [Display(Name = "Current room type")]
        public string RoomTypeId { get; set; }

        [Required]
        public IEnumerable<RoomTypeViewModel> RoomTypes { get; set; }
    }
}
