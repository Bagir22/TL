using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    Task<RoomType?> CreateRoomTypeAsync( Guid propertyId,
        RoomType roomType,
        string currencyType,
        List<string> services,
        List<string> amenities );

    Task<RoomType?> GetRoomTypeByIdAsync( Guid id );
    Task<IEnumerable<RoomType?>> GetAllRoomTypesAsync();
    Task<IEnumerable<RoomType?>> GetRoomTypesByPropertyIdAsync( Guid propertyId );

    Task<RoomType?> UpdateRoomTypeAsync(
        RoomType roomType,
        string currencyType,
        List<string> services,
        List<string> amenities );

    Task<bool> DeleteRoomTypeAsync( Guid id );
}