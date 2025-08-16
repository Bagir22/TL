namespace Domain.Entities;

public class RoomType
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property Property { get; set; }
    public required string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int RoomsCount { get; set; }

    public ICollection<RoomTypeService> RoomTypeServices { get; set; } = new List<RoomTypeService>();
    public ICollection<RoomTypeAmenity> RoomTypeAmenities { get; set; } = new List<RoomTypeAmenity>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}