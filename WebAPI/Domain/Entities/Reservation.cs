namespace Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; init; }
    public required Property Property { get; init; }
    public Guid RoomTypeId { get; init; }
    public required RoomType RoomType { get; init; }
    public DateOnly ArrivalDateUTC { get; init; }
    public DateOnly DepartureDateUTC { get; init; }
    public TimeOnly ArrivalTime { get; init; }
    public TimeOnly DepartureTime { get; init; }
    public decimal Total { get; set; }
    public int CurrencyId { get; set; }
    public required Currency Currency { get; init; }

    public ICollection<ReservationGuest> ReservationGuests { get; set; } = new List<ReservationGuest>();
}