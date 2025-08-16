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

    public async Task<IEnumerable<(RoomType RoomType, int AvailableRooms)>> SearchAvailableAsync(
        string city, DateOnly arrivalDate, DateOnly departureDate, int guests )
    {
        List<RoomType?> roomTypes = await _context.RoomType
            .Include( rt => rt.Property )
            .Include( rt => rt.Currency )
            .Include( rt => rt.RoomTypeServices ).ThenInclude( s => s.Service )
            .Include( rt => rt.RoomTypeAmenities ).ThenInclude( a => a.Amenity )
            .Include( rt => rt.Reservations )
            .Where( rt => rt.Property.City == city && rt.MaxPersonCount >= guests )
            .ToListAsync();

        List<(RoomType RoomType, int AvailableRooms)> results = roomTypes.Select( rt =>
            {
                int bookingCount = rt.Reservations.Count( r =>
                    r.ArrivalDateUTC <= departureDate &&
                    r.DepartureDateUTC >= arrivalDate );

                int availableCount = rt.RoomsCount - bookingCount;

                return ( RoomType: rt, AvailableRooms: availableCount );
            } )
            .Where( x => x.AvailableRooms > 0 )
            .ToList();

        return results;
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
        return await _context.Reservation
            .Include( r => r.RoomType )
            .Include( r => r.Currency )
            .Include( r => r.ReservationGuests )
            .ThenInclude( rg => rg.Guest )
            .FirstOrDefaultAsync( x => x.Id == id );
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync( ReservationFilter filter )
    {
        IQueryable<Reservation>? query = _context.Reservation
            .Include( r => r.RoomType )
            .ThenInclude( rt => rt.RoomTypeServices )
            .Include( r => r.RoomType )
            .ThenInclude( rt => rt.RoomTypeAmenities )
            .Include( r => r.ReservationGuests )
            .ThenInclude( rg => rg.Guest )
            .Include( r => r.Currency )
            .AsQueryable();

        query = ParametrizeQuery( filter, query );

        query = query.Skip( filter.Offset ).Take( filter.Limit );

        return await query.ToListAsync();
    }

    public async Task DeleteReservationAsync( Guid id )
    {
        Reservation? reservation = await _context.Reservation.FirstOrDefaultAsync( rt => rt.Id == id );
        if ( reservation != null )
        {
            _context.Reservation.Remove( reservation );
        }
    }

    public async Task<int> GetBookedCountAsync( Guid roomTypeId, DateOnly arrivalDate, TimeOnly arrivalTime,
        DateOnly departureDate, TimeOnly departureTime )
    {
        DateTime newArrival = arrivalDate.ToDateTime( arrivalTime );
        DateTime newDeparture = departureDate.ToDateTime( departureTime );

        List<Reservation> reservations = await _context.Reservation
            .Where( r => r.RoomTypeId == roomTypeId )
            .ToListAsync();

        int bookingCount = reservations.Count( r =>
            r.ArrivalDateUTC.ToDateTime( r.ArrivalTime ) < newDeparture &&
            r.DepartureDateUTC.ToDateTime( r.DepartureTime ) > newArrival
        );

        return bookingCount;
    }

    private IQueryable<Reservation> ParametrizeQuery( ReservationFilter filter, IQueryable<Reservation> query )
    {
        if ( filter.PropertyId.HasValue )
            query = query.Where( r => r.PropertyId == filter.PropertyId );

        if ( filter.RoomTypeId.HasValue )
            query = query.Where( r => r.RoomTypeId == filter.RoomTypeId );

        if ( filter.ArrivalDateFrom.HasValue )
            query = query.Where( r => r.ArrivalDateUTC >= filter.ArrivalDateFrom.Value );

        if ( filter.ArrivalDateTo.HasValue )
            query = query.Where( r => r.ArrivalDateUTC <= filter.ArrivalDateTo.Value );

        if ( filter.DepartureDateFrom.HasValue )
            query = query.Where( r => r.DepartureDateUTC >= filter.DepartureDateFrom.Value );

        if ( filter.DepartureDateTo.HasValue )
            query = query.Where( r => r.DepartureDateUTC <= filter.DepartureDateTo.Value );

        if ( !string.IsNullOrEmpty( filter.GuestName ) )
            query = query.Where( r => r.ReservationGuests.Any( g => g.Guest.Name.Contains( filter.GuestName ) ) );

        if ( !string.IsNullOrEmpty( filter.GuestPhoneNumber ) )
            query = query.Where( r =>
                r.ReservationGuests.Any( g => g.Guest.PhoneNumber.Contains( filter.GuestPhoneNumber ) ) );

        if ( !string.IsNullOrEmpty( filter.Service ) )
        {
            query = query.Where( r => r.RoomType.RoomTypeServices
                .Any( rtService => rtService.Service.Name.Contains( filter.Service ) ) );
        }

        if ( !string.IsNullOrEmpty( filter.Amenity ) )
        {
            query = query.Where( r => r.RoomType.RoomTypeAmenities
                .Any( rtAmenity => rtAmenity.Amenity.Name.Contains( filter.Amenity ) ) );
        }

        return query;
    }
}