using Micro.HTTP;
using Micro.Samples.OrdersService.Clients.DTO;
using Microsoft.Extensions.Options;

namespace Micro.Samples.OrdersService.Clients;

internal sealed class CustomersApiClient : ICustomersApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly string _url;
    private readonly string _clientName;

    public CustomersApiClient(IHttpClientFactory factory, IOptions<HttpClientOptions> options)
    {
        _factory = factory;
        _clientName = options.Value.Name;
        _url = options.Value.Services["customers"];
    }

    public Task<CustomerDto?> GetAsync(Guid id)
        => _factory.CreateClient(_clientName).GetFromJsonAsync<CustomerDto>($"{_url}/customers/{id}");
}