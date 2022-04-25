using Micro.Handlers;
using Micro.Samples.OrdersService.Clients;
using Micro.Samples.OrdersService.DAL;
using Micro.Samples.OrdersService.Entities;
using Micro.Samples.OrdersService.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Micro.Samples.OrdersService.Commands.Handlers;

public sealed class CreateOrderHandler : ICommandHandler<CreateOrder>
{
    private readonly ICustomersApiClient _customersApiClient;
    private readonly OrdersDbContext _dbContext;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(ICustomersApiClient customersApiClient, OrdersDbContext dbContext,
        ILogger<CreateOrderHandler> logger)
    {
        _customersApiClient = customersApiClient;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task HandleAsync(CreateOrder command, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation($"Creating the order with ID: '{command.OrderId}' for customer: '{command.CustomerId}'...");
        var customerDto = await _customersApiClient.GetAsync(command.CustomerId);
        if (customerDto is null)
        {
            throw new CustomerNotFoundException(command.CustomerId);
        }

        var customer = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == command.CustomerId, cancellationToken);
        if (customer is null)
        {
            throw new CustomerNotFoundException(command.CustomerId);
        }
        
        await _dbContext.Orders.AddAsync(new Order {Id = command.OrderId, CustomerId = customer.Id, Amount = 100},
            cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Created the order with ID: '{command.OrderId}' for customer: '{command.CustomerId}'.");
    }
}