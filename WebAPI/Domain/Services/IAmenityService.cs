using Domain.Entities;

namespace Domain.Services;

public interface IAmenityService
{
    Task<Amenity?> GetAmenityByNameAsync( string name );
    Task<Amenity> CreateAmenityAsync( Amenity amenity );
}