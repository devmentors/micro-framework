using Micro.Contexts;
using Micro.Handlers;
using Micro.Messaging.Brokers;
using Micro.Samples.CustomersService.DAL;
using Micro.Samples.CustomersService.Entities;
using Micro.Samples.CustomersService.Events;
using Micro.Serialization;

namespace Micro.Samples.CustomersService.Commands.Handlers;

internal sealed class CreateCustomerHandler : ICommandHandler<CreateCustomer>
{
    private readonly IMessageBroker _messageBroker;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IContextProvider _contextProvider;
    private readonly CustomersDbContext _dbContext;
    private readonly ILogger<CreateCustomerHandler> _logger;

    public CreateCustomerHandler(IMessageBroker messageBroker, IJsonSerializer jsonSerializer, 
        IContextProvider contextProvider, CustomersDbContext dbContext, ILogger<CreateCustomerHandler> logger)
    {
        _messageBroker = messageBroker;
        _jsonSerializer = jsonSerializer;
        _contextProvider = contextProvider;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task HandleAsync(CreateCustomer command, CancellationToken cancellationToken = default)
    {
        var context = _contextProvider.Current();
        var requestPayload = _jsonSerializer.Serialize(command);
        var contextPayload = _jsonSerializer.Serialize(context);
        await _dbContext.Customers.AddAsync(new Customer {Id = command.Id, Name = command.Name}, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Created customer{Environment.NewLine}{requestPayload}{Environment.NewLine}{contextPayload}");
        await _messageBroker.SendAsync(new CustomerCreated(command.Id, command.Name), cancellationToken);
    }
}