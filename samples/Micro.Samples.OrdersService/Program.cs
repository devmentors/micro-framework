using Micro.DAL.Postgres;
using Micro.Framework;
using Micro.Handlers;
using Micro.Messaging;
using Micro.Messaging.RabbitMQ;
using Micro.Samples.OrdersService.Clients;
using Micro.Samples.OrdersService.Commands;
using Micro.Samples.OrdersService.DAL;
using Micro.Samples.OrdersService.Events.External;
using Micro.Transactions;
using Micro.Transactions.Inbox;
using Micro.Transactions.Outbox;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services
    .AddSingleton<ICustomersApiClient, CustomersApiClient>()
    .AddPostgres<OrdersDbContext>(builder.Configuration)
    .AddInbox<OrdersDbContext>(builder.Configuration)
    .AddOutbox<OrdersDbContext>(builder.Configuration)
    .AddTransactionalDecorators()
    .AddOutboxInstantSenderDecorators()
    .AddMessagingErrorHandlingDecorators();

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo);

app.MapGet("/ping", () => "pong");

app.MapPost("/orders", async (CreateOrder command, IDispatcher dispatcher) =>
{
    await dispatcher.SendAsync(command with {OrderId = Guid.NewGuid()});
    return Results.NoContent();
});

app.Subscribe()
    .Event<CustomerCreated>();
    
app.UseMicroFramework().Run();