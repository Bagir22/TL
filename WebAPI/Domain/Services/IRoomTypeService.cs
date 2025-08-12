using Domain.Entities;

namespace Domain.Services;

public interface IRoomTypeService
{
    Task<RoomType> CreateRoomTypeAsync( Guid propertyId, RoomType roomType );
    Task<RoomType?> GetRoomTypeByIdAsync( Guid id );
    Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();
    Task<IEnumerable<RoomType>> GetRoomTypesByPropertyIdAsync( Guid propertyId );
    Task<RoomType?> UpdateRoomTypeAsync( Guid id, RoomType updatedRoomType );
    Task<bool> DeleteRoomTypeAsync( Guid id );
}