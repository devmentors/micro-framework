using Micro.Abstractions;
using Micro.Attributes;

namespace Micro.Samples.OrdersService.Events.External;

[Message("customers", "customer_created", "orders.customer_created")]
public record CustomerCreated(Guid Id, string Name) : IEvent;


