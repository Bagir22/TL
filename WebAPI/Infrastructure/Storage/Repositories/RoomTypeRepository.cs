using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly WebAPIDbContext _context;

    public RoomTypeRepository( WebAPIDbContext context )
    {
        _context = context;
    }

    public async Task<RoomType> CreateRoomTypeAsync( RoomType roomType )
    {
        if ( roomType.Id == Guid.Empty )
        {
            roomType.Id = Guid.NewGuid();
        }

        _context.RoomType.Add( roomType );
        
        return roomType;
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync( Guid id )
    {
        return await _context.RoomType.FirstOrDefaultAsync( x => x.Id == id );
    }
    
    public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync()
    {
        return await _context.RoomType.ToListAsync();
    }

    public async Task<IEnumerable<RoomType>> GetRoomTypesByPropertyIdAsync( Guid propertyId )
    {
        return await _context.RoomType
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync();
    }

    public async Task UpdateRoomTypeAsync( RoomType roomType )
    {
        RoomType? entity = await _context.RoomType.FindAsync(roomType.Id);
        if ( entity == null )
        {
            throw new KeyNotFoundException($"RoomType with Id {roomType.Id} not found");
        }
        
        entity.Name = roomType.Name;
        entity.DailyPrice = roomType.DailyPrice;
        entity.Currency = roomType.Currency;
        entity.MinPersonCount = roomType.MinPersonCount;
        entity.MaxPersonCount = roomType.MaxPersonCount;
        entity.RoomsCount = roomType.RoomsCount;
        
        
        // Сделал временно что бы не ругалось при миграции
        foreach (Domain.Entities.RoomTypeService service in roomType.RoomTypeServices)
        {
            roomType.RoomTypeServices.Add(service);
        }
        
        foreach (Domain.Entities.RoomTypeAmenity amenity in roomType.RoomTypeAmenities)
        {
            roomType.RoomTypeAmenities.Add(amenity);
        }

        _context.RoomType.Update(entity);
    }
    
    public async Task DeleteRoomTypeAsync( Guid id )
    {
        RoomType? roomType = await _context.RoomType.FirstOrDefaultAsync( rt => rt.Id == id );
        if ( roomType != null )
        {
            _context.RoomType.Remove( roomType );
        }
    }
}