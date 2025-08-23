using Domain.Entities;

namespace Domain.Repositories;

public interface IAmenityRepository
{
    Task<Amenity?> GetAmenityByNameAsync( string name );
    Task<Amenity> CreateAmenityAsync( Amenity amenity );
}