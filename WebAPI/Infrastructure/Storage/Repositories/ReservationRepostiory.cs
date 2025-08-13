using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly WebAPIDbContext _context;
    private IReservationRepository _reservationRepositoryImplementation;

    public ReservationRepository( WebAPIDbContext context )
    {
        _context = context;
    }
    
    public async Task<Reservation> CreateReservationAsync( Reservation reservation )
    {
        if ( reservation.Id == Guid.Empty )
        {
            reservation.Id = Guid.NewGuid();
        }

        _context.Reservation.Add( reservation );
        
        return reservation;
    }

    public async Task<Reservation?> GetReservationByIdAsync( Guid id )
    {
        return await _context.Reservation.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservation.ToListAsync();
    }

    public async Task DeleteReservationAsync( Guid id )
    {
        Reservation? reservation = await _context.Reservation.FirstOrDefaultAsync( rt => rt.Id == id );
        if ( reservation != null )
        {
            _context.Reservation.Remove( reservation );
        }
    }

    public async Task<IEnumerable<Reservation>> SearchAsync( string city, DateOnly arrivalDate, DateOnly departureDate, int guests )
    {
        return await _context.Reservation
            .Include(r => r.Property)
            .Where(r => r.Property.City == city
                        && r.ArrivalDateUTC <= arrivalDate
                        && r.DepartureDateUTC >= departureDate)
            .ToListAsync();
    }
}