using Micro.Abstractions;
using Micro.Attributes;

namespace Micro.Samples.DeliveriesService.Events.External;

[Message("orders", "customer_for_order_created", "deliveries.customer_for_order_created")]
public record CustomerForOrderCreated(Guid Id, string Name) : IEvent;
