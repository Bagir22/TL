using Domain.Entities;

namespace Domain.Services;

public interface IReservationService
{
    Task<IEnumerable<(RoomType RoomType, int AvailableRooms)>> SearchAvailableAsync(
        string city, DateOnly arrivalDate, DateOnly departureDate, int guests);

    Task<Reservation> CreateReservationAsync( Reservation reservation, List<Guest> guests );
    
    Task<Reservation?> GetReservationByIdAsync( Guid id );
    
    Task<IEnumerable<Reservation>> GetAllReservationsAsync(ReservationFilter filter);
    
    Task<bool> DeleteReservationAsync( Guid id );
    
    
}