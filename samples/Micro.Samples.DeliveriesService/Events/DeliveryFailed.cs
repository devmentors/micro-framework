using Micro.Abstractions;
using Micro.Attributes;

namespace Micro.Samples.DeliveriesService.Events;

[Message("deliveries", "delivery_failed")]
public record DeliveryFailed(Guid DeliveryId) : IEvent;
