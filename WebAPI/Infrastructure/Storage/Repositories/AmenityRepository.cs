using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class AmenityRepository : IAmenityRepository
{
    private readonly WebAPIDbContext _context;

    public AmenityRepository(WebAPIDbContext context)
    {
        _context = context;
    }

    public async Task<Amenity?> GetAmenityByNameAsync(string name)
    {
        return await _context.Amenity.FirstOrDefaultAsync(a => a.Name == name);
    }

    public async Task<Amenity> CreateAmenityAsync(Amenity amenity)
    {
        _context.Amenity.Add(amenity);
        await _context.SaveChangesAsync();
        
        return amenity;
    }
}