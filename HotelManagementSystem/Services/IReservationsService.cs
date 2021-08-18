using HotelManagementSystem.Models.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IReservationsService
    {
        ReservationsQueryModel All(ReservationsQueryModel res);

        void CancelReservation(string roomId);

        AddReservationFormModel ListFreeRooms(AddReservationFormModel reservation);

        void AddReservation(AddReservationFormModel reservation);

        DetailsReservationViewModel GetDetails(string id);

        void Delete(string id);

        AssignGuestFormModel LoadGuest(string guestId);

        void AssignGuestToReservation(AssignGuestFormModel guest);

        IEnumerable<SelectListItem> GetVouchers();
    }
}
