using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<(RoomType RoomType, int AvailableRooms)>>  SearchAvailableAsync(string city, DateOnly arrivalDate, DateOnly departureDate, int guests);
    
    
    Task<Reservation> CreateReservationAsync(Reservation reservation);
    Task<Reservation?> GetReservationByIdAsync( Guid id );
    Task<IEnumerable<Reservation>> GetAllReservationsAsync( ReservationFilter filter );
    Task DeleteReservationAsync( Guid id );
}