using Humanizer;
using Micro.Contexts;
using Micro.Handlers;
using Micro.Serialization;

namespace Micro.Samples.DeliveriesService.Events.External.Handlers;

internal sealed class CustomerForOrderCreatedHandler : IEventHandler<CustomerForOrderCreated>
{
    private readonly IContextProvider _contextProvider;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ILogger<CustomerForOrderCreatedHandler> _logger;

    public CustomerForOrderCreatedHandler(IContextProvider contextProvider, IJsonSerializer jsonSerializer,
        ILogger<CustomerForOrderCreatedHandler> logger)
    {
        _contextProvider = contextProvider;
        _jsonSerializer = jsonSerializer;
        _logger = logger;
    }
    
    public async Task HandleAsync(CustomerForOrderCreated @event, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var messagePayload = _jsonSerializer.Serialize(@event);
        var contextPayload = _jsonSerializer.Serialize(context);
        _logger.LogInformation($"Received '{nameof(CustomerForOrderCreated).Underscore()}'{Environment.NewLine}{messagePayload}{Environment.NewLine}{contextPayload}");
        await Task.CompletedTask;
    }
}