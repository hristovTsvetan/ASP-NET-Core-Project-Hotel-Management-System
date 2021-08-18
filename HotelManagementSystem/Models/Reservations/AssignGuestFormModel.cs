using HotelManagementSystem.Validators;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Reservations
{
    public class AssignGuestFormModel
    {
        [Required]
        [IdentityCardExist]
        [Display(Name = "Identity Card ID")]
        public string IdentityId { get; set; }

        public string GuestId { get; set; }

        public string ReservationId { get; set; }

        [Display(Name = "Full name")]
        public string GuestName { get; set; }

        [Display(Name = "Country")]
        public string GuestCountry { get; set; }

        [Display(Name = "City")]
        public string GuestCity { get; set; }

        [Display(Name = "Address")]
        public string GuestAddress { get; set; }

        [Display(Name = "Phone")]
        public string GuestPhone { get; set; }

        public IEnumerable<SelectListItem> Vouchers { get; set; }

        [Display(Name = "Voucher")]
        public string VoucherId { get; set; }

        public string LoadGuestButton { get; set; }

        public string AssignButton { get; set; }
    }
}
