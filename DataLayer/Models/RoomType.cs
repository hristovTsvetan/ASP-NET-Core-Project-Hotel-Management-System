using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
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

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public int NumberOfBeds { get; set; }

        public string Image { get; set; }

        public bool Deleted { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
