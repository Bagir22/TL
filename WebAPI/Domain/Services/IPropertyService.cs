using Domain.Entities;

namespace Domain.Services;

public interface IPropertyService
{
    Task<Property> CreatePropertyAsync( Property property );
    Task<Property?> GetPropertyByIdAsync( Guid id );
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task<Property?> UpdatePropertyAsync( Guid id, Property updatedProperty );
    Task<bool> DeletePropertyAsync( Guid id );
}