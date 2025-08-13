using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

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
    
    [HttpGet("reservations/{id:guid}")]
    public async Task<IActionResult> GetReservationById(Guid id)
    {
        Reservation? reservation = await _reservationService.GetReservationByIdAsync(id);
        if (reservation == null)
            return NotFound();

        CreatedReservationDTO? dto = _mapper.Map<CreatedReservationDTO>(reservation);
        
        return Ok(dto);
    }
    
    [HttpPost("reservations")]
    public async Task<IActionResult> CreateReservation([FromBody] ReadReservationDTO dto)
    {
        Reservation? reservation = _mapper.Map<Reservation>(dto);
        
        Reservation createdReservation = await _reservationService.CreateReservationAsync( reservation);

        CreatedReservationDTO? createdDto = _mapper.Map<CreatedReservationDTO>(createdReservation);
        
        return CreatedAtAction(nameof(GetReservationById), new { id = createdDto.Id }, createdDto);
    }
    
    [HttpGet("reservations/")]
    public async Task<IActionResult> GetAllReservations()
    {
        IEnumerable<Reservation> reservations = await _reservationService.GetAllReservationsAsync();
        
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

        return NoContent();
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string city,
        [FromQuery] string arrivalDate,
        [FromQuery] string departureDate,
        [FromQuery] int guests)
    {
        DateOnly arrival;
        if (!DateOnly.TryParse(arrivalDate, out arrival))
            return BadRequest("Invalid arrivalDate format, expected yyyy-MM-dd");
        if (!DateOnly.TryParse(departureDate, out DateOnly departure))
            return BadRequest("Invalid departureDate format, expected yyyy-MM-dd");

        IEnumerable<Reservation> results = await _reservationService.SearchAsync(city, arrival, departure, guests);
        IEnumerable<CreatedReservationDTO>? dtos = _mapper.Map<IEnumerable<CreatedReservationDTO>>(results);

        return Ok(dtos);
    }
}