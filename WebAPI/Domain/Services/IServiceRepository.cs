using Domain.Entities;

namespace Domain.Services;

public interface IServiceRepository
{
    Task<Service?> GetServiceByNameAsync(string name);
    Task<Service> CreateServiceAsync(Service service);
}