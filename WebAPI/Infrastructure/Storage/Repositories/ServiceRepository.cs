using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly WebAPIDbContext _context;

    public ServiceRepository(WebAPIDbContext context)
    {
        _context = context;
    }

    public async Task<Service?> GetServiceByNameAsync(string name)
    {
        return await _context.Service.FirstOrDefaultAsync(s => s.Name == name);
    }

    public async Task<Service> CreateServiceAsync(Service service)
    {
        _context.Service.Add(service);
        await _context.SaveChangesAsync();
        
        return service;
    }
}