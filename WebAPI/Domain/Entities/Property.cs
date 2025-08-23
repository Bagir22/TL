using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Property
{
    public Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public ICollection<RoomType> RoomTypes { get; private set; } = new List<RoomType>();
    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
}