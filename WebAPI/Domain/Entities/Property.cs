using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public ICollection<RoomType> RoomTypes { get; private set; } = new List<RoomType>();
    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
}
