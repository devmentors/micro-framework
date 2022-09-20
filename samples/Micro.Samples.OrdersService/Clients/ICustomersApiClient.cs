using Micro.Samples.OrdersService.Clients.DTO;

namespace Micro.Samples.OrdersService.Clients;

public interface ICustomersApiClient
{
    Task<CustomerDto?> GetAsync(Guid id);
}