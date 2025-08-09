namespace Domain.Entities;

public class Reservation()
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public Guid RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }
    public DateOnly ArrivalDateUTC { get; set; }
    public DateOnly DepartureDateUTC { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public string GuestName { get; set; }
    public string GuestPhoneNumber { get; set; }
    public decimal Total { get; set; }
    public Currency Currency { get; set; }
}