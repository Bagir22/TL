namespace Domain.Entities;

public class Amenity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<RoomTypeAmenity> RoomTypeAmenities { get; set; } = new List<RoomTypeAmenity>();
}