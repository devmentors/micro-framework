using Micro.API.AsyncApi;
using Micro.Samples.CustomersService.Events;
using Saunter.Attributes;

namespace Micro.Samples.CustomersService;

internal abstract class AsyncApi : IAsyncApi
{
    [Channel(nameof(customer_created), BindingsRef = "customers")]
    [SubscribeOperation(typeof(CustomerCreated), Summary = "Informs about created customer.", OperationId = nameof(customer_created))]
    internal abstract void customer_created();
}