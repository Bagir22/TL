namespace Domain.Entities;

public class RoomTypeService
{
    public Guid RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }

    public int ServiceId { get; set; }
    public Service Service { get; set; }
}
