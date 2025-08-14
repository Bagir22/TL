using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PropertyService( IPropertyRepository propertyRepository, IUnitOfWork unitOfWork )
    {
        _propertyRepository = propertyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Property?> CreatePropertyAsync( Property property )
    {
        await _propertyRepository.CreatePropertyAsync( property );
        await _unitOfWork.CommitAsync();

        return property;
    }

    public async Task<Property?> GetPropertyByIdAsync( Guid id )
    {
        return await _propertyRepository.GetPropertyByIdAsync( id );
    }

    public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
    {
        return await _propertyRepository.GetAllPropertiesAsync();
    }

    public async Task<Property?> UpdatePropertyAsync( Guid id, Property updatedProperty )
    {
        Property? property = await _propertyRepository.GetPropertyByIdAsync( id );
        if ( property == null )
        {
            return null;
        }

        property.Name = updatedProperty.Name;
        property.Country = updatedProperty.Country;
        property.City = updatedProperty.City;
        property.Address = updatedProperty.Address;
        property.Latitude = updatedProperty.Latitude;
        property.Longitude = updatedProperty.Longitude;

        await _propertyRepository.UpdatePropertyAsync(property);
        await _unitOfWork.CommitAsync();

        return property;
    }

    public async Task<bool> DeletePropertyAsync( Guid id )
    {
        Property? property = await _propertyRepository.GetPropertyByIdAsync( id );
        if ( property == null )
        {
            return false;
        }

        await _propertyRepository.DeletePropertyAsync( id );
        await _unitOfWork.CommitAsync();

        return true;
    }
}