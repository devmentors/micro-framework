using Micro.Abstractions;

namespace Micro.Samples.OrdersService.Commands;

public record CreateOrder(Guid OrderId, Guid CustomerId) : ICommand;
