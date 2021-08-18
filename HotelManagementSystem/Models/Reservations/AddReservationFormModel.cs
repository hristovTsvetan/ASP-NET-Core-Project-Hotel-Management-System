using HotelManagementSystem.Validators.Messages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Reservations
{
    public class AddReservationFormModel : IValidatableObject
    {
        public AddReservationFormModel()
        {
            this.AvailableRooms = new List<SelectListItem>();
            this.SelectedRooms = new List<string>();
        }

        [MaxLength(50, ErrorMessage = ValidatorConstants.maxLength)]
        [MinLength(2, ErrorMessage = ValidatorConstants.minLength)]
        [Display(Name = "Reservation name")]
        public string Name { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End date")]
        public DateTime EndDate { get; set; }

        public string LoadRoomsButton { get; set; }

        public string AddReservationButton { get; set; }

        public ICollection<SelectListItem> AvailableRooms;

        [Display(Name = "Available rooms")]
        public ICollection<string> SelectedRooms { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.AddReservationButton))
            {
                yield return new ValidationResult(
                   "Should select one or more rooms for reservation!", new[] { nameof(this.Name) });
            }

            if (this.SelectedRooms.Count == 0 && !string.IsNullOrWhiteSpace(this.AddReservationButton))
            {
                yield return new ValidationResult(
                   "Should select one or more rooms for reservation!", new[] { nameof(this.SelectedRooms) });
            }

            if (this.StartDate >= this.EndDate || this.StartDate < DateTime.Now.Date)
            {
                yield return new ValidationResult(
                   "Start date can't be greater than end date, and start date must be from today ot later!", new[] { nameof(this.StartDate) });
            }

            if (this.EndDate <= this.StartDate || this.EndDate <= DateTime.Now.Date)
            {
                yield return new ValidationResult(
                   "End date can't be lower than start date, and end date must be from tommorow ot later!", new[] { nameof(this.EndDate) });
            }
        }
    }
}
