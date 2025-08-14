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
        return await _context.RoomType
            .Include(rt => rt.Currency)
            .Include(rt => rt.RoomTypeServices)
            .ThenInclude(rts => rts.Service)
            .Include(rt => rt.RoomTypeAmenities)
            .ThenInclude(rta => rta.Amenity)
            .FirstOrDefaultAsync(rt => rt.Id == id);
    }
    
    public async Task<IEnumerable<RoomType?>> GetAllRoomTypesAsync()
    {
        return await _context.RoomType
            .Include(rt => rt.Currency)
            .Include(rt => rt.RoomTypeServices)
            .ThenInclude(rts => rts.Service)
            .Include(rt => rt.RoomTypeAmenities)
            .ThenInclude(rta => rta.Amenity)
            .ToListAsync();
    }

    public async Task<IEnumerable<RoomType?>> GetRoomTypesByPropertyIdAsync( Guid propertyId )
    {
        return await _context.RoomType
            .Where(rt => rt.PropertyId == propertyId)
            .Include(rt => rt.Currency)
            .Include(rt => rt.RoomTypeServices)
            .ThenInclude(rts => rts.Service)
            .Include(rt => rt.RoomTypeAmenities)
            .ThenInclude(rta => rta.Amenity)
            .ToListAsync();
    }

    public async Task UpdateRoomTypeAsync(RoomType roomType, IEnumerable<string> serviceNames, IEnumerable<string> amenityNames)
    {
        RoomType? entity = await _context.RoomType
            .Include(rt => rt.RoomTypeServices)
            .ThenInclude(rts => rts.Service)
            .Include(rt => rt.RoomTypeAmenities)
            .ThenInclude(rta => rta.Amenity)
            .FirstOrDefaultAsync(rt => rt.Id == roomType.Id);

        if (entity == null)
            throw new InvalidOperationException("RoomType not found");

        entity.Name = roomType.Name;
        entity.DailyPrice = roomType.DailyPrice;
        entity.MinPersonCount = roomType.MinPersonCount;
        entity.MaxPersonCount = roomType.MaxPersonCount;
        entity.RoomsCount = roomType.RoomsCount;
        entity.CurrencyId = roomType.CurrencyId;
        
        entity.RoomTypeServices.Clear();
        foreach (string serviceName in serviceNames.Distinct())
        {
            Service? service = await _context.Service.FirstOrDefaultAsync(s => s.Name == serviceName);
            if (service == null)
            {
                service = new Service { Name = serviceName };
                _context.Service.Add(service);
                await _context.SaveChangesAsync();
            }

            _context.Attach(service);
            entity.RoomTypeServices.Add(new RoomTypeService
            {
                RoomTypeId = entity.Id,
                ServiceId = service.Id
            });
        }
        
        entity.RoomTypeAmenities.Clear();
        foreach (string amenityName in amenityNames.Distinct())
        {
            Amenity? amenity = await _context.Amenity.FirstOrDefaultAsync(a => a.Name == amenityName);
            if (amenity == null)
            {
                amenity = new Amenity { Name = amenityName };
                _context.Amenity.Add(amenity);
                await _context.SaveChangesAsync();
            }

            _context.Attach(amenity);
            entity.RoomTypeAmenities.Add(new RoomTypeAmenity
            {
                RoomTypeId = entity.Id,
                AmenityId = amenity.Id
            });
        }

        await _context.SaveChangesAsync();
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