namespace Domain.Entities;

public class Amenity
{
    public int Id { get; init; }
    public required string Name { get; init; }

    public ICollection<RoomTypeAmenity> RoomTypeAmenities { get; init; } = new List<RoomTypeAmenity>();
}