using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ServiceService( IServiceRepository serviceRepository, IUnitOfWork unitOfWork )
    {
        _serviceRepository = serviceRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<Service?> GetServiceByNameAsync( string name )
    {
        return _serviceRepository.GetServiceByNameAsync( name );
    }

    public async Task<Service> CreateServiceAsync( Service service )
    {
        await _serviceRepository.CreateServiceAsync( service );
        await _unitOfWork.CommitAsync();

        return service;
    }
}