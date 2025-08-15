using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.DTOs.RoomTypeDTOs;
using WebAPI.Exceptions;

namespace WebAPI.Controllers;

[ApiController]
[Route("api")]
public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly IMapper _mapper;
    
    public ReservationController( IReservationService reservationService, IMapper mapper )
    {
        _reservationService = reservationService;
        _mapper = mapper;
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] SearchReservationsDTO query)
    {
        if (!ModelState.IsValid)
        {
            throw new HttpResponseException(400, ModelState.ToString() );
        }

        if (!DateOnly.TryParse(query.ArrivalDate, out DateOnly arrival))
        {
            throw new HttpResponseException( 400, "Invalid arrivalDate format, expected yyyy-MM-dd" );
        }

        if (!DateOnly.TryParse(query.DepartureDate, out DateOnly departure))
        {
            throw new HttpResponseException(400, "Invalid departureDate format, expected yyyy-MM-dd" );
        }

        IEnumerable<(RoomType RoomType, int AvailableRooms)> results = await _reservationService.SearchAvailableAsync(query.City, arrival, departure, query.Guests);

        IEnumerable<AvailableRoomTypeDTO> dtos = results.Select(x =>
        {
            AvailableRoomTypeDTO? dto = _mapper.Map<AvailableRoomTypeDTO>(x.RoomType);
            dto.AvailableRoomsCount = x.AvailableRooms;
            
            return dto;
        });
        
        return Ok( dtos );
    }
    
    [HttpPost("reservations")]
    public async Task<IActionResult> CreateReservation([FromBody] ReadReservationDTO dto)
    {
        if (!ModelState.IsValid)
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            throw new HttpResponseException(400, string.Join("; ", errors));
        }
        
        Reservation reservation = _mapper.Map<Reservation>(dto);
        
        List<Guest> guests = _mapper.Map<List<Guest>>(dto.Guests);
        
        try
        {
            Reservation? createdReservation = await _reservationService.CreateReservationAsync(
                reservation,
                guests
            );
            
            CreatedReservationDTO responseDto = _mapper.Map<CreatedReservationDTO>(createdReservation);

            return Ok(responseDto);
        }
        catch (ValidationException ex)
        {
            throw new HttpResponseException(400, ex.Message);
        }
    }
    
    [HttpGet("reservations/{id:guid}")]
    public async Task<ActionResult<CreatedReservationDTO>> GetReservationById(Guid id)
    {
        Reservation? reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
            return NotFound();

        CreatedReservationDTO? reservationDto = _mapper.Map<CreatedReservationDTO>(reservation);
        
        return Ok(reservationDto);
    }
    
    [HttpGet("reservations/")]
    public async Task<IActionResult> GetAllReservations([FromQuery] ReservationSearchFilterDTO dtoFilter)
    {
        ReservationFilter? filter = _mapper.Map<ReservationFilter>(dtoFilter);

        IEnumerable<Reservation> reservations = await _reservationService.GetAllReservationsAsync(filter);
        IEnumerable<CreatedReservationDTO>? dtos = _mapper.Map<IEnumerable<CreatedReservationDTO>>(reservations);

        return Ok(dtos);
    }
    
    [HttpDelete("reservations/{id:guid}")]
    public async Task<IActionResult> DeleteReservation(Guid id)
    {
        bool deleted = await _reservationService.DeleteReservationAsync(id);
        if ( !deleted )
        {
            return NotFound();
        }

        return Ok();
    }
}