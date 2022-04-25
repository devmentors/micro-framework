using Micro.Abstractions;

namespace Micro.Samples.CustomersService.Commands;

public record CreateCustomer(Guid Id, string Name) : ICommand;
