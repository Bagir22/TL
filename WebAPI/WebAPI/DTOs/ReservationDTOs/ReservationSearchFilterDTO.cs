using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WebAPI.DTOs;

public class ReservationSearchFilterDTO
{
    public Guid? PropertyId { get; set; }
    public Guid? RoomTypeId { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string? ArrivalDateFrom { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string? ArrivalDateTo { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string? DepartureDateFrom { get; set; }
    
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string? DepartureDateTo { get; set; }
    
    public string? GuestName { get; set; }
    
    [Phone]
    public string? GuestPhoneNumber { get; set; }
    
    public string? Service { get; set; }
    public string? Amenity { get; set; }

    public int? Limit { get; set; } = 10;
    public int? Offset { get; set; } = 0;
}