using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Areas.Admin.Models.Users
{
    public class ChangeUserRoleFormModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public ICollection<SelectListItem> Roles { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
