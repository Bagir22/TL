using Domain.Entities;

namespace WebAPI.DTOs;

public class CreatedRoomTypeDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public required string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public Currency Currency { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public string Services { get; set; } = "";
    public string Amenities { get; set; } = "";
    public int RoomsCount { get; set; }
}
