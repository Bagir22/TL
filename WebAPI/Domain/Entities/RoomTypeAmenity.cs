namespace Domain.Entities;

public class RoomTypeAmenity
{
    public Guid RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public int AmenityId { get; set; }
    public Amenity Amenity { get; set; }
}