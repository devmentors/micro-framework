using Micro.Abstractions;
using Micro.Attributes;

namespace Micro.Samples.OrdersService.Events;

[Message("orders", "customer_for_order_created")]
public record CustomerForOrderCreated(Guid Id, string Name) : IEvent;