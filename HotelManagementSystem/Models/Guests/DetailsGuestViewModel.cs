using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models.Guests
{
    public class DetailsGuestViewModel
    {
        public string Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Identity card id")]
        public string IdentityCardId { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Details")]
        public string Details { get; set; }

        [Display(Name = "Created on")]
        public string Created { get; set; }

        [Display(Name = "City")]
        public virtual string City { get; set; }

        [Display(Name = "Rank")]
        public virtual string Rank { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Total reservations")]
        public int CreatedReservationsCount { get; set; }
    }
}
