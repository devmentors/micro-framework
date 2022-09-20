using Micro.Abstractions;
using Micro.Attributes;

namespace Micro.Samples.CustomersService.Events;

[Message("customers", "customer_created")]
public record CustomerCreated(Guid Id, string Name) : IEvent;


