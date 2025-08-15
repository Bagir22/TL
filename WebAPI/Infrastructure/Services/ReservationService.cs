using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public ReservationService( 
        IReservationRepository reservationRepository, 
        IGuestRepository guestRepository,
        IRoomTypeRepository roomTypeRepository,
        IUnitOfWork unitOfWork 
        )
    {
        _reservationRepository = reservationRepository;
        _guestRepository = guestRepository;
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<(RoomType RoomType, int AvailableRooms)>> SearchAvailableAsync(
        string city, DateOnly arrivalDate, DateOnly departureDate, int guests)
    {
        return await _reservationRepository.SearchAvailableAsync(city, arrivalDate, departureDate, guests);
    }
    
    public async Task<Reservation> CreateReservationAsync(Reservation reservation, List<Guest> guests)
    {
        if (reservation.ArrivalDateUTC > reservation.DepartureDateUTC)
        {
            throw new ValidationException( "Arrival date cannot be after departure date" );
        }
        
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync(reservation.RoomTypeId);
        if (roomType == null)
        {
            throw new ValidationException( "Room type not found" );
        }
        
        int nights = (reservation.DepartureDateUTC.ToDateTime(TimeOnly.MinValue) - 
                      reservation.ArrivalDateUTC.ToDateTime(TimeOnly.MinValue)).Days;
        if (nights == 0)
        {
            reservation.Total = roomType.DailyPrice;
        }
        else
        {
            reservation.Total = roomType.DailyPrice * nights;
            reservation.CurrencyId = roomType.CurrencyId;
        }
        
        await _reservationRepository.CreateReservationAsync(reservation);
        await _unitOfWork.CommitAsync();
        
        foreach (Guest guest in guests)
        {
            Guest? existingGuest = await _guestRepository.GetByPhoneNumberAsync(guest.PhoneNumber);
            if (existingGuest != null)
            {
                reservation.ReservationGuests.Add(new ReservationGuest
                {
                    GuestId = existingGuest.Id,
                    Reservation = reservation
                });
            }
            else
            {
                await _guestRepository.CreateAsync(guest); 
                reservation.ReservationGuests.Add(new ReservationGuest
                {
                    Guest = guest,
                    Reservation = reservation
                });
            }
        }

        await _unitOfWork.CommitAsync();

        return reservation;
    }
    
    public async Task<Reservation?> GetReservationByIdAsync( Guid id )
    {
        return await _reservationRepository.GetReservationByIdAsync(id);
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync(ReservationFilter filter)
    {
        return await _reservationRepository.GetAllReservationsAsync(filter);
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

    
}