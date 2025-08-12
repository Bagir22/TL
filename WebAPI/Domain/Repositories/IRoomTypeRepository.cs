using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType> CreateRoomTypeAsync( RoomType roomType );
    Task<RoomType?> GetRoomTypeDtoByIdAsync( Guid id );
    Task<IEnumerable<RoomType>> GetAllRoomTypesAsync();
    Task<IEnumerable<RoomType>> GetRoomTypesByPropertyIdAsync( Guid propertyId );
    Task UpdateRoomTypeAsync( RoomType roomType );
    Task DeleteRoomTypeAsync( Guid id );
}