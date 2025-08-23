namespace Domain.Entities;

public class ReservationFilter
{
    public Guid? PropertyId { get; init; }
    public Guid? RoomTypeId { get; init; }
    public DateOnly? ArrivalDateFrom { get; init; }
    public DateOnly? ArrivalDateTo { get; init; }
    public DateOnly? DepartureDateFrom { get; init; }
    public DateOnly? DepartureDateTo { get; init; }
    public string? GuestName { get; init; }
    public string? GuestPhoneNumber { get; init; }
    public string? Service { get; init; }
    public string? Amenity { get; init; }
    public int Limit { get; init; } = 10;
    public int Offset { get; init; } = 0;
}