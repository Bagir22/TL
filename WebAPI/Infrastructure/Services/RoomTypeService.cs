using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoomTypeService( IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork )
    {
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomType> CreateRoomTypeAsync( Guid propertyId, RoomType roomType )
    {
        roomType.Id = Guid.NewGuid();
        roomType.PropertyId = propertyId;
        
        await _roomTypeRepository.CreateRoomTypeAsync( roomType );
        await _unitOfWork.CommitAsync();

        return roomType;
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync( Guid id )
    {
        return await _roomTypeRepository.GetRoomTypeByIdAsync(id);
    }
    
    public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync()
    {
        return await _roomTypeRepository.GetAllRoomTypesAsync();
    }

    public async Task<IEnumerable<RoomType>> GetRoomTypesByPropertyIdAsync( Guid propertyId )
    {
        return await _roomTypeRepository.GetRoomTypesByPropertyIdAsync( propertyId );
    }

    public async Task<RoomType?> UpdateRoomTypeAsync( Guid id, RoomType updatedRoomType )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync( id );
        if ( roomType == null )
        {
            return null;
        }

        roomType.Name = updatedRoomType.Name;
        roomType.DailyPrice = updatedRoomType.DailyPrice;
        roomType.Currency = updatedRoomType.Currency;
        roomType.MinPersonCount = updatedRoomType.MinPersonCount;
        roomType.MaxPersonCount = updatedRoomType.MaxPersonCount;
        roomType.RoomsCount = updatedRoomType.RoomsCount;
        
        // Сделал временно что бы не ругалось при миграции
        foreach (Domain.Entities.RoomTypeService service in updatedRoomType.RoomTypeServices)
        {
            roomType.RoomTypeServices.Add(service);
        }
        
        foreach (Domain.Entities.RoomTypeAmenity amenity in updatedRoomType.RoomTypeAmenities)
        {
            roomType.RoomTypeAmenities.Add(amenity);
        }

        await _roomTypeRepository.UpdateRoomTypeAsync( roomType );
        await _unitOfWork.CommitAsync();

        return roomType;
    }

    public async Task<bool> DeleteRoomTypeAsync( Guid id )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync( id );
        if ( roomType == null )
        {
            return false;
        }

        await _roomTypeRepository.DeleteRoomTypeAsync( id );
        await _unitOfWork.CommitAsync();

        return true;
    }
}