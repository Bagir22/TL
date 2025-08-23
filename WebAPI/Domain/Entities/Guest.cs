namespace Domain.Entities;

public class Guest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string PhoneNumber { get; init; }

    public ICollection<ReservationGuest> ReservationGuests { get; init; } = new List<ReservationGuest>();
}