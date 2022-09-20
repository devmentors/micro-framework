using Micro.Exceptions;

namespace Micro.Samples.OrdersService.Exceptions;

public class CustomerNotFoundException : CustomException
{
    public Guid CustomerId { get; }

    public CustomerNotFoundException(Guid customerId) : base($"Customer with ID: '{customerId}' was not found")
    {
        CustomerId = customerId;
    }
}