using Domain.Entities;

namespace Domain.Services;

public interface IReservationService
{
    Task<Reservation> CreateReservationAsync(Reservation reservation);
    
    Task<Reservation?> GetReservationByIdAsync( Guid id );
    
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    
    Task<bool> DeleteReservationAsync( Guid id );
    
    Task<IEnumerable<Reservation>> SearchAsync(string city, DateOnly arrivalDate, DateOnly departureDate, int guests);
}