namespace Domain.Entities;

public class Service
{
    public int Id { get; init; }
    public required string Name { get; init; }

    public ICollection<RoomTypeService> RoomTypeServices { get; set; } = new List<RoomTypeService>();
}