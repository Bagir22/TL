using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class AmenityService : IAmenityService
{
    private readonly IAmenityRepository _amenityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AmenityService( IAmenityRepository amenityRepository, IUnitOfWork unitOfWork )
    {
        _amenityRepository = amenityRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Amenity?> GetAmenityByNameAsync( string name )
    {
        return _amenityRepository.GetAmenityByNameAsync( name );
    }

    public async Task<Amenity> CreateAmenityAsync( Amenity amenity )
    {
        await _amenityRepository.CreateAmenityAsync( amenity );
        await _unitOfWork.CommitAsync();
        return amenity;
    }
}