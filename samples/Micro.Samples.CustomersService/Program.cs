using Micro.DAL.Postgres;
using Micro.Framework;
using Micro.Handlers;
using Micro.Messaging.RabbitMQ;
using Micro.Samples.CustomersService.Commands;
using Micro.Samples.CustomersService.DAL;
using Micro.Samples.CustomersService.DTO;
using Micro.Transactions;
using Micro.Transactions.Inbox;
using Micro.Transactions.Outbox;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services
    .AddPostgres<CustomersDbContext>(builder.Configuration)
    .AddInbox<CustomersDbContext>(builder.Configuration)
    .AddOutbox<CustomersDbContext>(builder.Configuration)
    .AddRabbitMQ(builder.Configuration)
    .AddTransactionalDecorators()
    .AddOutboxInstantSenderDecorators()
    .AddMessagingErrorHandlingDecorators();

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo);

app.MapGet("/ping", () => "pong");

app.MapGet("/customers", async (CustomersDbContext dbContext) =>
    Results.Ok(await dbContext.Customers.Select(x => new CustomerDto(x.Id, x.Name)).ToListAsync()));

app.MapGet("/customers/{id:guid}", async (Guid id, CustomersDbContext dbContext) =>
{
    var customer = await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
    return customer is null ? Results.NotFound() : Results.Ok(new CustomerDto(customer.Id, customer.Name));
});

app.MapPost("/customers", async (CreateCustomer command, IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(command with {Id = Guid.NewGuid()});
    return Results.NoContent();
});

app.UseMicroFramework().Run();