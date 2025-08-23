using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using WebAPI.DTOs.GuestDTOs;

namespace WebAPI.DTOs;

public class ReadReservationDTO
{
    [Required]
    public Guid PropertyId { get; set; }

    [Required]
    public Guid RoomTypeId { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string ArrivalDateUTC { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string DepartureDateUTC { get; set; }

    [Required]
    [RegularExpression(@"^\d{2}:\d{2}$")]
    public string ArrivalTime { get; set; }

    [Required]
    [RegularExpression(@"^\d{2}:\d{2}$")]
    public string DepartureTime { get; set; }

    [Required]
    [MinLength(1)]
    public List<GuestDTO> Guests { get; set; } = new();
}