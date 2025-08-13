namespace Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<RoomTypeService> RoomTypeServices { get; set; } = new List<RoomTypeService>();
}