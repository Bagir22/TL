namespace Domain.Entities;
public class ReservationFilter
{
    public Guid? PropertyId { get; set; }
    public Guid? RoomTypeId { get; set; }

    public DateOnly? ArrivalDateFrom { get; set; }
    public DateOnly? ArrivalDateTo { get; set; }
    public DateOnly? DepartureDateFrom { get; set; }
    public DateOnly? DepartureDateTo { get; set; }

    public string? GuestName { get; set; }
    public string? GuestPhoneNumber { get; set; }

    public string? Service { get; set; }
    public string? Amenity { get; set; }

    public int Limit { get; set; } = 10;
    public int Offset { get; set; } = 0;
}