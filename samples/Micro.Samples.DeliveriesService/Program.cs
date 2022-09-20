using Micro.Framework;
using Micro.Messaging;
using Micro.Messaging.Exceptions;
using Micro.Messaging.RabbitMQ;
using Micro.Samples.DeliveriesService.Events.External;
using Micro.Samples.DeliveriesService.Internals;

var builder = WebApplication
    .CreateBuilder(args)
    .AddMicroFramework();

builder.Services
    .AddSingleton<IMessagingExceptionPolicyResolver, DeliveriesMessagingExceptionPolicyResolver>()
    .AddMessagingErrorHandlingDecorators();

var app = builder.Build();

app.MapGet("/", (AppInfo appInfo) => appInfo);

app.MapGet("/ping", () => "pong");

app.Subscribe()
    .Event<CustomerForOrderCreated>();

app.UseMicroFramework().Run();