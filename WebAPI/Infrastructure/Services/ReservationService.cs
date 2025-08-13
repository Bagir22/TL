using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ReservationService( IReservationRepository reservationRepository, IUnitOfWork unitOfWork )
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Reservation> CreateReservationAsync( Reservation reservation )
    {
        reservation.Id = Guid.NewGuid();
        
        await _reservationRepository.CreateReservationAsync( reservation );
        await _unitOfWork.CommitAsync();

        return reservation;
    }

    public async Task<Reservation?> GetReservationByIdAsync( Guid id )
    {
        return await _reservationRepository.GetReservationByIdAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _reservationRepository.GetAllReservationsAsync();
    }

    public async Task<bool> DeleteReservationAsync( Guid id )
    {
        Reservation? reservation = await _reservationRepository.GetReservationByIdAsync( id );
        if ( reservation == null )
        {
            return false;
        }

        await _reservationRepository.DeleteReservationAsync( id );
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<IEnumerable<Reservation>> SearchAsync( string city, DateOnly arrivalDate, DateOnly departureDate, int guests )
    {
        return await _reservationRepository.SearchAsync(city, arrivalDate, departureDate, guests);
    }
}