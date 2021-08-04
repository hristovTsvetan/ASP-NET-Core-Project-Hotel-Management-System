using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Data.Models
{
    public class RoomType
    {
        public RoomType()
        {
            this.Rooms = new HashSet<Room>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
