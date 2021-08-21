using DataLayer.Models;
using HotelManagementSystem.Models.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelManagementSystem.Services
{
    public interface IReservationsService
    {
        ReservationsQueryModel All(ReservationsQueryModel res);

        Task CancelReservation(string roomId);

        AddReservationFormModel ListFreeRooms(AddReservationFormModel reservation);

        Task AddReservation(AddReservationFormModel reservation);

        DetailsReservationViewModel GetDetails(string id);

        Task Delete(string id);

        AssignGuestFormModel LoadGuest(string guestId);

        Task AssignGuestToReservation(AssignGuestFormModel guest);

        IEnumerable<SelectListItem> GetVouchers();

        Hotel GetActiveHotel();
    }
}
