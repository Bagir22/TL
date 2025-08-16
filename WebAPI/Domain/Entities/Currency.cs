namespace Domain.Entities;

public class Currency
{
    public int Id { get; init; }
    public required string Type { get; init; }

    public ICollection<Reservation> Reservations { get; init; } = new List<Reservation>();
    public ICollection<RoomType> RoomTypes { get; init; } = new List<RoomType>();
}