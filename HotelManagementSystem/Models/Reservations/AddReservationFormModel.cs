using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models.Reservations
{
    public class AddReservationFormModel
    {
        public AddReservationFormModel()
        {
            this.Vouchers = new List<string>();
            this.ReserveRooms = new List<string>();
            this.AvailableRooms = new List<string>();
        }

        
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Range(1, 1000)]
        public int Duration { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string GuestIdentityId { get; set; }

        public string VoucherId { get; set; }

        public IEnumerable<string> Vouchers { get; set; }

        public IEnumerable<string> AvailableRooms;

        public IEnumerable<string> ReserveRooms;
    }
}
