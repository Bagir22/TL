using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType> CreateRoomTypeAsync( RoomType roomType );
    Task<RoomType?> GetRoomTypeByIdAsync( Guid id );
    Task<IEnumerable<RoomType?>> GetAllRoomTypesAsync();
    Task<IEnumerable<RoomType?>> GetRoomTypesByPropertyIdAsync( Guid propertyId );
    Task UpdateRoomTypeAsync(RoomType roomType, IEnumerable<string> serviceNames, IEnumerable<string> amenityNames);
    Task DeleteRoomTypeAsync( Guid id );
}