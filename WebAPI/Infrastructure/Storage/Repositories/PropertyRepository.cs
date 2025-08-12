using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly WebAPIDbContext _context;

    public PropertyRepository( WebAPIDbContext context )
    {
        _context = context;
    }

    public async Task<Property> CreatePropertyAsync( Property property )
    {
        if ( property.Id == Guid.Empty )
        {
            property.Id = Guid.NewGuid();
        }

        await _context.Property.AddAsync( property );

        return property;
    }

    public async Task<Property?> GetPropertyByIdAsync( Guid id )
    {
        return await _context.Property.FirstOrDefaultAsync( p => p.Id == id );
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _context.Property.ToListAsync();
    }

    public async Task UpdatePropertyAsync(Property property)
    {
        Property? entity = await _context.Property.FindAsync(property.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Property with Id {property.Id} not found");
        }

        entity.Name = property.Name;
        entity.Country = property.Country;
        entity.City = property.City;
        entity.Address = property.Address;
        entity.Latitude = property.Latitude;
        entity.Longitude = property.Longitude;

        _context.Property.Update(entity);
    }

    public async Task DeletePropertyAsync( Guid id )
    {
        Property? property = await _context.Property.FirstOrDefaultAsync( p => p.Id == id );
        if ( property != null )
        {
            _context.Property.Remove( property );
        }
    }
}