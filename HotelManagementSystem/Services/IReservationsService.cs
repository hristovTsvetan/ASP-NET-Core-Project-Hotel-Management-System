using HotelManagementSystem.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IReservationsService
    {
        ReservationsQueryModel All();

        void CancelReservation(string roomId);
    }
}
