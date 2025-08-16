using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Exception = System.Exception;

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
        string city, string arrivalDate, string departureDate, int guests )
    {
        if ( !DateOnly.TryParse( arrivalDate, out DateOnly arrival ) )
        {
            throw new ValidationException( "Invalid arrivalDate format, expected yyyy-MM-dd" );
        }

        if ( !DateOnly.TryParse( departureDate, out DateOnly departure ) )
        {
            throw new ValidationException( "Invalid departureDate format, expected yyyy-MM-dd" );
        }

        if ( departure < arrival )
        {
            throw new ValidationException( "Departure date cannot be earlier than arrival date" );
        }

        return await _reservationRepository.SearchAvailableAsync( city, arrival, departure, guests );
    }

    public async Task<Reservation> CreateReservationAsync( Reservation reservation, List<Guest> guests )
    {
        ValidateReservationDates( reservation );

        RoomType roomType = await GetRoomTypeAsync( reservation.RoomTypeId );

        await CheckRoomsAvailability( reservation, roomType );

        CalculateReservationTotal( reservation, roomType );

        reservation.CurrencyId = roomType.CurrencyId;

        await AttachGuestsAsync( reservation, guests );

        await _reservationRepository.CreateReservationAsync( reservation );
        await _unitOfWork.CommitAsync();

        return reservation;
    }

    public async Task<Reservation?> GetReservationByIdAsync( Guid id )
    {
        return await _reservationRepository.GetReservationByIdAsync( id );
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync( ReservationFilter filter )
    {
        return await _reservationRepository.GetAllReservationsAsync( filter );
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

    private void ValidateReservationDates( Reservation reservation )
    {
        if ( reservation.ArrivalDateUTC > reservation.DepartureDateUTC )
        {
            throw new ValidationException( "Arrival date cannot be after departure date" );
        }
    }

    private async Task<RoomType> GetRoomTypeAsync( Guid roomTypeId )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync( roomTypeId );
        if ( roomType == null )
        {
            throw new Exception( $"Room type with id {roomTypeId} not found" );
        }

        return roomType;
    }

    private async Task CheckRoomsAvailability( Reservation reservation, RoomType roomType )
    {
        int bookingCount = await _reservationRepository.GetBookedCountAsync(
            reservation.RoomTypeId,
            reservation.ArrivalDateUTC,
            reservation.ArrivalTime,
            reservation.DepartureDateUTC,
            reservation.DepartureTime
        );

        int availableCount = roomType.RoomsCount - bookingCount;

        if ( availableCount <= 0 )
        {
            throw new Exception( "No available rooms for the selected dates and times" );
        }
    }

    private void CalculateReservationTotal( Reservation reservation, RoomType roomType )
    {
        int nights = ( reservation.DepartureDateUTC.ToDateTime( TimeOnly.MinValue ) -
                       reservation.ArrivalDateUTC.ToDateTime( TimeOnly.MinValue ) ).Days;
        if ( nights == 0 )
        {
            reservation.Total = roomType.DailyPrice;
        }
        else
        {
            reservation.Total = roomType.DailyPrice * nights;
        }
    }

    private async Task AttachGuestsAsync( Reservation reservation, List<Guest> guests )
    {
        foreach ( Guest guest in guests )
        {
            Guest? existingGuest = await _guestRepository.GetByPhoneNumberAsync( guest.PhoneNumber );
            if ( existingGuest != null )
            {
                reservation.ReservationGuests.Add( new ReservationGuest
                {
                    GuestId = existingGuest.Id,
                    Reservation = reservation
                } );
            }
            else
            {
                await _guestRepository.CreateAsync( guest );
                await _unitOfWork.CommitAsync();

                reservation.ReservationGuests.Add( new ReservationGuest
                {
                    Guest = guest,
                    Reservation = reservation
                } );
            }
        }
    }
}