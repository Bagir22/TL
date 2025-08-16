using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.DTOs.RoomTypeDTOs;
using WebAPI.Exceptions;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace WebAPI.Controllers;

[ApiController]
[Route( "api" )]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IMapper _mapper;

    public ReservationController( IReservationService reservationService, IMapper mapper )
    {
        _reservationService = reservationService;
        _mapper = mapper;
    }

    [HttpGet( "search" )]
    public async Task<IActionResult> Search( [FromQuery] SearchReservationsDTO query )
    {
        if ( !ModelState.IsValid )
        {
            throw new ValidationException( ModelState.ToString() );
        }

        IEnumerable<(RoomType RoomType, int AvailableRooms)> results =
            await _reservationService.SearchAvailableAsync( query.City, query.ArrivalDate, query.DepartureDate,
                query.Guests );

        IEnumerable<AvailableRoomTypeDTO> dtos = results.Select( x =>
        {
            AvailableRoomTypeDTO? dto = _mapper.Map<AvailableRoomTypeDTO>( x.RoomType );
            dto.AvailableRoomsCount = x.AvailableRooms;

            return dto;
        } );

        return Ok( dtos );
    }

    [HttpPost( "reservations" )]
    public async Task<IActionResult> CreateReservation( [FromBody] ReadReservationDTO dto )
    {
        if ( !ModelState.IsValid )
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany( v => v.Errors )
                .Select( e => e.ErrorMessage );

            throw new ValidationException( string.Join( "; ", errors ) );
        }

        Reservation reservation = _mapper.Map<Reservation>( dto );

        List<Guest> guests = _mapper.Map<List<Guest>>( dto.Guests );

        Reservation? createdReservation = await _reservationService.CreateReservationAsync(
            reservation,
            guests
        );

        CreatedReservationDTO responseDto = _mapper.Map<CreatedReservationDTO>( createdReservation );

        return Ok( responseDto );
    }

    [HttpGet( "reservations/{id:guid}" )]
    public async Task<ActionResult<CreatedReservationDTO>> GetReservationById( Guid id )
    {
        Reservation? reservation = await _reservationService.GetReservationByIdAsync( id );
        if ( reservation == null )
        {
            throw new NotFoundException( $"Reservation with {id} not found" );
        }

        CreatedReservationDTO? reservationDto = _mapper.Map<CreatedReservationDTO>( reservation );

        return Ok( reservationDto );
    }

    [HttpGet( "reservations/" )]
    public async Task<IActionResult> GetAllReservations( [FromQuery] ReservationSearchFilterDTO dtoFilter )
    {
        ReservationFilter? filter = _mapper.Map<ReservationFilter>( dtoFilter );

        IEnumerable<Reservation> reservations = await _reservationService.GetAllReservationsAsync( filter );
        IEnumerable<CreatedReservationDTO>? dtos = _mapper.Map<IEnumerable<CreatedReservationDTO>>( reservations );

        return Ok( dtos );
    }

    [HttpDelete( "reservations/{id:guid}" )]
    public async Task<IActionResult> DeleteReservation( Guid id )
    {
        bool deleted = await _reservationService.DeleteReservationAsync( id );
        if ( !deleted )
        {
            throw new NotFoundException( $"Reservation with {id} not found" );
        }

        return Ok();
    }
}