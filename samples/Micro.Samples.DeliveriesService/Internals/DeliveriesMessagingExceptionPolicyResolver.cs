using Micro.Abstractions;
using Micro.Messaging.Exceptions;
using Micro.Samples.DeliveriesService.Events;
using Micro.Samples.DeliveriesService.Events.External;

namespace Micro.Samples.DeliveriesService.Internals;

internal sealed class DeliveriesMessagingExceptionPolicyResolver : IMessagingExceptionPolicyResolver
{
    public MessageExceptionPolicy? Resolve(IMessage message, Exception exception)
        => message switch
        {
            CustomerForOrderCreated _ => new MessageExceptionPolicy(false, true,
                broker => broker.SendAsync(new DeliveryFailed(Guid.NewGuid()))),
            _ => null
        };
}