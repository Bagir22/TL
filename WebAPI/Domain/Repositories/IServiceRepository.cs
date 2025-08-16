using Domain.Entities;

namespace Domain.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetServiceByNameAsync( string name );
    Task<Service> CreateServiceAsync( Service service );
}