using Domain.Entities;

namespace Domain.Services;

public interface IAmenityRepository
{
    Task<Amenity?> GetAmenityByNameAsync(string name);
    Task<Amenity> CreateAmenityAsync(Amenity amenity);
}