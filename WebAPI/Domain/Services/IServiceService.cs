using Domain.Entities;

namespace Domain.Services;

public interface IServiceService
{
    Task<Service?> GetServiceByNameAsync( string name );
    Task<Service> CreateServiceAsync( Service service );
}