using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    public Task<Property> CreatePropertyAsync( Property property );
    public Task<Property?> GetPropertyByIdAsync( Guid id );
    Task<IEnumerable<Property>> GetAllPropertiesAsync();
    Task UpdatePropertyAsync( Property property );
    Task DeletePropertyAsync( Guid id );
}