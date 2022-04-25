using Micro.API.AsyncApi;
using Micro.Samples.OrdersService.Events;
using Saunter.Attributes;

namespace Micro.Samples.OrdersService;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(customer_for_order_created), BindingsRef = "orders")]
    [SubscribeOperation(typeof(CustomerForOrderCreated), Summary = "Informs about created customer for order.", OperationId = nameof(customer_for_order_created))]
    internal abstract void customer_for_order_created();
}