using WebAPI.DTOs.PropertyDTOs;

namespace WebAPI.DTOs.RoomTypeDTOs;

public class AvailableRoomTypeDTO
{
    public Guid RoomTypeId { get; set; }
    public string RoomTypeName { get; set; }
    public decimal DailyPrice { get; set; }
    public string CurrencyType { get; set; }
    public int MinPersonCount { get; set; }
    public int MaxPersonCount { get; set; }
    public int AvailableRoomsCount { get; set; }

    public CreatedPropertyDto Property { get; set; }
    
    public List<string> Services { get; set; } = new();
    public List<string> Amenities { get; set; } = new();
}