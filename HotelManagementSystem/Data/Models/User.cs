using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }
    }
}
